using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetAll;

public class GetAllExpensesUseCase(IExpensesReadOnlyRepository expensesRepository, IMapper mapper) : IGetAllExpensesUseCase
{
    public async Task<ResponseExpensesJson> Execute()
    {
        var expenses = await expensesRepository.GetAll();


        return new ResponseExpensesJson
        {
            Expenses = mapper.Map<List<ResponseShortExpenseJson>>(expenses)
        };
    }
}