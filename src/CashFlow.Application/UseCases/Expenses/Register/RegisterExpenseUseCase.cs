using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase(IExpensesRepository expensesRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRegisterExpenseUseCase
{
    private readonly IExpensesRepository _expensesRepository = expensesRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseRegisterExpenseJson> Execute(RequestRegisterExpenseJson request)
    {
        Validate(request);

        var expense = _mapper.Map<Expense>(request);
        
        await _expensesRepository.Add(expense);
        await _unitOfWork.Commit();

        return _mapper.Map<ResponseRegisterExpenseJson>(expense);
    }

    private static void Validate(RequestRegisterExpenseJson request)
    {
        var validator = new RegisterExpenseValidator();
        
        var result = validator.Validate(request);

        if (result.IsValid) return;
        
        var errorMessages = result.Errors.Select((error) => error.ErrorMessage).ToList();

        throw new ErrorOnValidationException(errorMessages);
    }
}