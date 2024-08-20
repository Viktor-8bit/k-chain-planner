

using CSharpFunctionalExtensions;
namespace Core.Common;

public class ChainStep
{

    public ChainStep(string title, string description, Chain fatherChain, DateOnly? start, DateOnly? end)
    {
        Title = title;
        Description = description;
        StepId = fatherChain.StepChainLastId;
        FatherChain = fatherChain;
        Start = start;
        End = end;
    }
    private ChainStep() { }

    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public int StepId { get; set; }
    public DateOnly? Start { get; private set; }     // начало этапа
    public DateOnly? End { get; private set; }       // конец этапа
    public Chain FatherChain { get; set; }

    public static Result<ChainStep> CreateChainStep(string title, string description, Chain fatherChain, DateOnly? start, DateOnly? end)
    {
        if (string.IsNullOrEmpty(title)) return Result.Failure<ChainStep>("Имя не должно быть пустым");
        if (string.IsNullOrEmpty(description)) return Result.Failure<ChainStep>("Описание не должно быть пустым");
        
        if (start.HasValue && end.HasValue) 
            if (start.Value > end.Value) return Result.Failure<ChainStep>("Дата начала должна быть ранее даты конца");

        var newChainStep = new ChainStep(title, description, fatherChain, start, end);
        return Result.Success(newChainStep);
    }

    public Result<ChainStep> ChangeStartDate(DateOnly? start)
    {
        if (start.HasValue && End.HasValue) 
            if (start.Value > End.Value) return Result.Failure<ChainStep>("Дата начала должна быть ранее даты конца");
        
        Start = start;
        return Result.Success(this);
    }
    
    public Result<ChainStep> ChangeEndDate(DateOnly? end)
    {
        if (Start.HasValue && end.HasValue) 
            if (Start.Value > end.Value) return Result.Failure<ChainStep>("Дата начала должна быть ранее даты конца");
        
        End = end;
        return Result.Success(this);
    }

    public Result<ChainStep> ChaingeDate(DateOnly? start, DateOnly? end)
    {
        if (start.HasValue && end.HasValue) 
            if (start.Value > end.Value) return Result.Failure<ChainStep>("Дата начала должна быть ранее даты конца");
        Start = start;
        End = end;
        return Result.Success(this);
    }
    
    
    public Result<ChainStep> ChangeDescription(string description)
    {
        if (string.IsNullOrEmpty(description)) return Result.Failure<ChainStep>("Описание не должно быть пустым");
        Description = description;
        return Result.Success(this);
    }

    public Result<ChainStep> ChangeTitle(string title)
    {
        if (string.IsNullOrEmpty(title)) return Result.Failure<ChainStep>("Имя не должно быть пустым");
        Title = title;
        return Result.Success(this);
    }
}