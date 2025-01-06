using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Delete;

public class DeleteExpenseUseCase : IDeleteExpenseUseCase
{
    private readonly IExpensesWriteOnlyRepository _expensesRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteExpenseUseCase(
        IExpensesWriteOnlyRepository expensesRepository,
        IUnitOfWork unitOfWork
        )
    {
        _expensesRepository = expensesRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Execute(int id)
    {
        var result = await _expensesRepository.Delete(id);

        if (!result)
        {
            throw new NotFoundException("Expense not found");
        }

        await _unitOfWork.Commit();
    }
}