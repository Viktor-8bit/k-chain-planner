
using CSharpFunctionalExtensions;
using Core.Enums;

namespace Core.Common;

public class Chain
{

    public Chain(string pentestObj, List<Tag> tags)
    {
        PentestObj = pentestObj;
        Tags = tags;
    }
    
    // тестируемый объект
    public string PentestObj { get; private set; }
    
    // используемые технологии
    public List<Tag> Tags { get; private set; }

    // последний id StepChain
    public int StepChainLastId { get; private set; } = 0;
    
    public static Result<Chain> CreateChain(string pentestObj, List<Tag> tags)
    {
        if (string.IsNullOrEmpty(pentestObj)) return Result.Failure<Chain>("Имя не должно быть пустым");
        var newChain = new Chain(pentestObj, tags);
        return Result.Success(newChain);
    }

    public Result<Chain> ChangePentestObj(string pentestObj)
    {
        if (string.IsNullOrEmpty(pentestObj)) return Result.Failure<Chain>("Имя не должно быть пустым");
        PentestObj = pentestObj;
        return Result.Success(this);
    }
    
    public Result<Chain> AddTag(Tag tag)
    {
        if (Tags.Contains(tag)) return Result.Failure<Chain>("Тег уже присутствует в цепочке");
        Tags.Add(tag);
        return Result.Success(this);
    }
    
    public Result<Chain> RemoveTag(Tag tag)
    {
        if (!Tags.Contains(tag)) return Result.Failure<Chain>("Тег не присутствует в цепочке");
        Tags.Remove(tag);
        return Result.Success(this);
    }
    
    public void IncreaseChainStepLastId() => StepChainLastId += 1;
    public void DecreaseChainStepLastId() => StepChainLastId -= 1;
}