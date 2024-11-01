namespace CashFlow.Exception.ExceptionsBase;

public class ErrorOnValidationException(List<string> errorMessages) : CashFlowExeption
{
    public List<string> Errors { get; set; } = errorMessages;
}