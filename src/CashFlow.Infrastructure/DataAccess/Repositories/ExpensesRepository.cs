using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories;

internal class ExpensesRepository(CashFlowDbContext dbContext) : IExpensesWriteOnlyRepository, IExpensesReadOnlyRepository, IExpensesUpdateOnlyRepository
{
    public async Task Add(Expense expense)
    {
        await dbContext.Expenses.AddAsync(expense);
    }

    public async Task<bool> Delete(int id)
    {
        var expense = await dbContext.Expenses.FirstOrDefaultAsync(x => x.Id == id);

        if (expense is null) return false;
        
        dbContext.Expenses.Remove(expense);

        return true;
    }

    public async Task<List<Expense>> GetAll()
    {
        return await dbContext.Expenses.AsNoTracking().ToListAsync();
    }

    async Task<Expense?> IExpensesReadOnlyRepository.GetById(int id)
    {
        
        return await dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(expense => expense.Id == id);
    }

    async Task<Expense?> IExpensesUpdateOnlyRepository.GetById(int id)
    {
        return await dbContext.Expenses.FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Update(Expense expense)
    {
        dbContext.Expenses.Update(expense);
    }
}