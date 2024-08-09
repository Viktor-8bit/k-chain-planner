

using CSharpFunctionalExtensions;
namespace Core.Common;

public class ChainStep
{

    public ChainStep(string title, string description, Chain fatherChain, DateOnly? start, DateOnly? _end)
    {
        Title = title;
        Description = description;
        StepId = fatherChain.StepChainLastId;
        FatherChain = fatherChain;
        Start = start;
        End = _end;
    }
    private ChainStep() { }

    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public int StepId { get; private set; }
    public DateOnly? Start { get; private set; }     // начало этапа
    public DateOnly? End { get; private set; }       // конец этапа
    public Chain FatherChain { get; set; }

    public static Result<ChainStep> CreateChainStep(string title, string description, Chain fatherChain, DateOnly? start, DateOnly? _end)
    {
        if (string.IsNullOrEmpty(title)) return Result.Failure<ChainStep>("Имя не должно быть пустым");
        if (string.IsNullOrEmpty(description)) return Result.Failure<ChainStep>("Описание не должно быть пустым");
        
        if (start.HasValue && _end.HasValue) 
            if (start.Value > _end.Value) return Result.Failure<ChainStep>("Дата начала должна быть ранее даты конца");

        var newChainStep = new ChainStep(title, description, fatherChain, start, _end);
        return Result.Success(newChainStep);
    }

    public Result<ChainStep> ChangeStartDate(DateOnly? start)
    {
        if (start.HasValue && End.HasValue) 
            if (start.Value > End.Value) return Result.Failure<ChainStep>("Дата начала должна быть ранее даты конца");
        
        Start = start;
        return Result.Success(this);
    }
    
    public Result<ChainStep> ChangeEndDate(DateOnly? _end)
    {
        if (Start.HasValue && _end.HasValue) 
            if (Start.Value > _end.Value) return Result.Failure<ChainStep>("Дата начала должна быть ранее даты конца");
        
        End = _end;
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