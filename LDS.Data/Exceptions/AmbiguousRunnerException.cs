namespace LDS.Data.Exceptions;

public class AmbiguousRunnerException : Exception
{
    public string RunnerName { get; set; }
    
    public AmbiguousRunnerException()
    {
    }

    public AmbiguousRunnerException(string message)
        : base(message)
    {
    }

    public AmbiguousRunnerException(string message, Exception inner)
        : base(message, inner)
    {
    }
}