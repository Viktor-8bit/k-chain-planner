


namespace WebApi.Contracts;

public record ChainStepRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateOnly? Start { get; set; }     
    public DateOnly? End { get; set; } 
}