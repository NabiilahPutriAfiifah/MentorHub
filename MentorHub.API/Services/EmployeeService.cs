using MentorHub.API.DTOs.Accounts;
using MentorHub.API.Models;
using MentorHub.API.Repositories;
using MentorHub.API.Repositories.Interfaces;
using MentorHub.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace MentorHub.API.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<Accounts> _passwordHasher;

    public EmployeeService(IAccountRepository accountRepository, IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork, IPasswordHasher<Accounts> passwordHasher)
    {
        _accountRepository = accountRepository;
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<IEnumerable<AccountResponse>> GetAllProfilesAsync(CancellationToken cancellationToken)
    {
        var getAllAccounts = await _accountRepository.GetAllAsync(cancellationToken);
        var getAllEmployees = await _employeeRepository.GetAllAsync(cancellationToken);

        if (!getAllAccounts.Any() || !getAllEmployees.Any())
        {
            return Enumerable.Empty<AccountResponse>();
        }

        var profilesMap = getAllAccounts.Join(
            getAllEmployees, 
            account => account.Id, 
            employee => employee.Id,
            (account, employee) =>
            new AccountResponse(
                account.Id,
                account.Username,
                account.RoleId,
                employee.FirstName,
                employee.LastName,
                employee.Email,
                employee.Bio,
                employee.Experience,
                employee.Position,
                employee.MentorId)
        );
        
        return profilesMap;
    }


    public async Task<AccountResponse?> GetProfileByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var getAccount = await _accountRepository.GetByIdAsync(userId, cancellationToken);
        var getEmployee = await _employeeRepository.GetByIdAsync(userId, cancellationToken);

        if (getAccount is null || getEmployee is null)
        {
            return null;
        }

        return new AccountResponse(
            getAccount.Id,
            getAccount.Username,
            getAccount.RoleId,
            getEmployee.FirstName,
            getEmployee.LastName,
            getEmployee.Email,
            getEmployee.Bio,
            getEmployee.Experience,
            getEmployee.Position,
            getEmployee.MentorId
        );
    }

    public async Task UpdatePasswordAsync(Guid userId, PasswordUpdateRequest request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByIdAsync(userId, cancellationToken);
        if (account is null)
        {
            throw new NullReferenceException("Account not found.");
        }

        var verificationResult = _passwordHasher.VerifyHashedPassword(
            account, 
            account.Password, 
            request.CurrentPassword);

        if (verificationResult == PasswordVerificationResult.Failed)
        {
            throw new ArgumentException("Current password is incorrect.");
        }

        account.Password = _passwordHasher.HashPassword(account, request.NewPassword);

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _accountRepository.UpdateAsync(account);
        }, cancellationToken); 
    }

    public async Task UpdateProfileAsync(Guid userId, ProfileUpdateRequest request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(userId, cancellationToken);
        if (employee is null)
        {
            throw new NullReferenceException("User profile not found."); // Termasuk otorisasi
        }

        employee.FirstName = request.FirstName;
        employee.LastName = request.LastName;
        employee.Email = request.Email;
        employee.Bio = request.Bio;
        employee.Experience = request.Experience;
        employee.Position = request.Position;

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _employeeRepository.UpdateAsync(employee);
        }, cancellationToken);
    }

}
