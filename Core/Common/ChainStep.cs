

using CSharpFunctionalExtensions;
namespace Core.Common;

public class ChainStep(string title, string description, Chain fatherChain, DateOnly? start, DateOnly? _end)
{
    public string Title { get; private set; } = title;
    public string Description { get; private set; } = description;
    public int StepId { get; private set; } = fatherChain.StepChainLastId;
    
    public DateOnly? Start { get; private set; } = start;  // начало этапа
    public DateOnly? End { get; private set; } = _end;     // конец этапа

    public Chain FatherChain { get; private set; } = fatherChain;

    public static Result<ChainStep> CreateChainStep(string title, string description, Chain fatherChain, DateOnly? start, DateOnly? _end)
    {
        if (string.IsNullOrEmpty(title)) return Result.Failure<ChainStep>("Имя не должно быть пустым");
        if (string.IsNullOrEmpty(description)) return Result.Failure<ChainStep>("Описание не должно быть пустым");
        
        if (start.HasValue && _end.HasValue) 
            if (start.Value > _end.Value) return Result.Failure<ChainStep>("Дата начала должна быть ранее даты конца");

        var newChainStep = new ChainStep(title, description, fatherChain, start, _end);
        fatherChain.IncreaseChainStepLastId();
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