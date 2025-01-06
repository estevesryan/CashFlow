using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.GetById;

public class GetExpenseByIdUseCase(IExpensesReadOnlyRepository expensesRepository, IMapper mapper) : IGetExpenseByIdUseCase
{
    public async Task<ResponseExpenseJson> Execute(long id)
    {       
        var expense = await expensesRepository.GetById(id);

        if(expense is null) throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        
        return mapper.Map<ResponseExpenseJson>(expense);
    }
    
    
}