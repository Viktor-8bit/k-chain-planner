



namespace WebApi.Contracts;

public record ChainStepResponce
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateOnly? Start { get; set; }     
    public DateOnly? End { get; set; } 
    public int FatherChainId { get; set; }
    public int StepId { get; set; }
}