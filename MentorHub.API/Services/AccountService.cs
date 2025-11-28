using MentorHub.API.DTOs.Accounts;
using MentorHub.API.Models;
using MentorHub.API.Repositories;
using MentorHub.API.Repositories.Interfaces;
using MentorHub.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace MentorHub.API.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<Accounts> _passwordHasher;

    public AccountService(IAccountRepository accountRepository, IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork, IPasswordHasher<Accounts> passwordHasher)
    {
        _accountRepository = accountRepository;
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task CreateEmployeeAsync(AccountCreationRequest accountRequest, EmployeeProfileRequest profileRequest, CancellationToken cancellationToken)
    {
        var newId = Guid.NewGuid();

        var hashedPassword = _passwordHasher.HashPassword(null!, accountRequest.Password);

        var account = new Accounts
        {
            Id = newId,
            Username = accountRequest.Username,
            Password = hashedPassword,
            RoleId = accountRequest.RoleId,
        };

        var employee = new Employees
        {
            Id = newId,
            FirstName = profileRequest.FirstName,
            LastName = profileRequest.LastName,
            Email = profileRequest.Email,
            Bio = profileRequest.Bio,
            Experience = profileRequest.Experience,
            Position = profileRequest.Position,
            MentorId = profileRequest.MentorId,
        };

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _accountRepository.CreateAsync(account, cancellationToken);
            await _employeeRepository.CreateAsync(employee, cancellationToken);
        }, cancellationToken);
    }
    
    public async Task DeleteEmployeeAsync(Guid id, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByIdAsync(id, cancellationToken);
        if (account is null)
        {
            throw new NullReferenceException("Account not found.");
        }

        var employee = await _employeeRepository.GetByIdAsync(id, cancellationToken);
        if (employee is null)
        {
            throw new NullReferenceException("Employee profile not found.");
        }

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _employeeRepository.DeleteAsync(employee);
            await _accountRepository.DeleteAsync(account);
        }, cancellationToken);
        
    }
}
