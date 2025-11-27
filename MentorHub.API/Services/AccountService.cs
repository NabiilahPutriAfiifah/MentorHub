using MentorHub.API.DTOs.Accounts;
using MentorHub.API.Models;
using MentorHub.API.Repositories;
using MentorHub.API.Repositories.Interfaces;
using MentorHub.API.Services.Interfaces;

namespace MentorHub.API.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AccountService(IAccountRepository accountRepository, IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateEmployeeAsync(AccountRequest request, CancellationToken cancellationToken)
    {
        var account = new Accounts
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            Password = request.Password,
            // RoleId = request.RoleId,
        };

        var employee = new Employees
        {
            Id = account.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Bio = request.Bio,
            Experience = request.Experience,
            Position = request.Position,
            // MentorId = request.MentorId,
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
            throw new Exception("Account Id not found");
        }

        var employee = await _employeeRepository.GetByIdAsync(id, cancellationToken);

        if (employee is null)
        {
            throw new Exception("Account Id not found");
        }

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _accountRepository.DeleteAsync(account);
            await _employeeRepository.DeleteAsync(employee);
        }, cancellationToken);
    }

    public async Task<IEnumerable<AccountResponse>> GetAllEmployeesAsync(CancellationToken cancellationToken)
    {
        var getAllAccounts = await _accountRepository.GetAllAsync(cancellationToken);
        if (!getAllAccounts.Any())
        {
            throw new Exception("No employees found.");
        }

        var getAllEmployees = await _employeeRepository.GetAllAsync(cancellationToken);
        if (!getAllEmployees.Any())
        {   
            throw new Exception("No employees found.");
        }

        var accountsMap = getAllAccounts.Join(
            getAllEmployees, 
            p => p.Id,
            s => s.Id,
            (p, s) =>
            new AccountResponse(
            p.Id,
            p.Username,
            p.RoleId,
            s.FirstName,
            s.LastName,
            s.Email,
            s.Bio,
            s.Experience,
            s.Position,
            s.MentorId));
        return accountsMap;
    }
    
    public async Task UpdateEmployeeAsync(Guid id, AccountRequest request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByIdAsync(id, cancellationToken);

        if (account is null)
        {
            throw new Exception("Account Id not found");
        }

        var employee = await _employeeRepository.GetByIdAsync(id, cancellationToken);

        if (employee is null)
        {
            throw new Exception("Account Id not found");
        }

        account.Username = request.Username;
        account.Password = request.Password;
        employee.FirstName = request.FirstName;
        employee.LastName = request.LastName;
        employee.Email = request.Email;
        employee.Bio = request.Bio;
        employee.Experience = request.Experience;
        employee.Position = request.Position;

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _accountRepository.UpdateAsync(account);
            await _employeeRepository.UpdateAsync(employee);
        }, cancellationToken);
    }

    public async Task<AccountResponse?> GetEmployeeByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var getAccount = await _accountRepository.GetByIdAsync(id, cancellationToken);
        if (getAccount is null)
        {
            throw new Exception("Account Id not found"); 
        }

        var getEmployee = await _employeeRepository.GetByIdAsync(id, cancellationToken);
        if (getEmployee is null)
        {
            throw new Exception("Employee data not found for this Account Id"); 
        }

        var accountResponse = new AccountResponse(
            getAccount.Id,
            getAccount.Username,
            getAccount.RoleId,
            getEmployee.FirstName,
            getEmployee.LastName,
            getEmployee.Email,
            getEmployee.Bio,
            getEmployee.Experience,
            getEmployee.Position,
            getEmployee.MentorId);

        return accountResponse;
    }
}
