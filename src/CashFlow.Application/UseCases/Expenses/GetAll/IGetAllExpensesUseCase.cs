using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.UseCases.Expenses.GetAll;

public interface IGetAllExpensesUseCase
{
    public Task<ResponseExpensesJson> Execute();
}