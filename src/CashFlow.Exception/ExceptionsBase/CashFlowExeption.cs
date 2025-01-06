namespace CashFlow.Exception.ExceptionsBase;

public abstract class CashFlowExeption(string message) : SystemException(message)
{ 
    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}