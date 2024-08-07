namespace Core.Common;

public class ChainStep
{
    public string Title { get; set; }

    public int StepId { get; set; } = 0;
    
    public required Chain FatherChain { get; set; }
    
    // начало этапа
    public DateOnly? Start { get; set; }
    
    // конец этапа
    public DateOnly? End { get; set; }
}