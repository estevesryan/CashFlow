using System.ComponentModel.DataAnnotations;
using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Update;

public class UpdateExpenseUseCase : IUpdateExpenseUseCase
{

    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IExpensesUpdateOnlyRepository _expensesRepository;

    public UpdateExpenseUseCase(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IExpensesUpdateOnlyRepository expensesRepository
            )
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _expensesRepository = expensesRepository;
    }
    
    public async Task Execute(int id, RequestExpenseJson request)
    {
        Validate(request);

        var expense = await _expensesRepository.GetById(id);
        
        if(expense is null) throw new NotFoundException("Expense not found");
        
        _mapper.Map(request, expense);
        
        _expensesRepository.Update(expense);
        
        await _unitOfWork.Commit();
    }

    private static void Validate(RequestExpenseJson request)
    {
        var validator = new ExpenseValidator();
        
        var result = validator.Validate(request);
        
        if(result.IsValid is false)
        {
            throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());
        }
    }
}