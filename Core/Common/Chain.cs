
using CSharpFunctionalExtensions;


namespace Core.Common;

public class Chain
{
    public Chain(string pentestObj, User creator)
    {
        Creator = creator;
        PentestObj = pentestObj;
    }
    
    public Chain() {}
    
    public int Id { get; private set; }
    public string PentestObj { get; private set; }
    
    public User Creator { get; private set; }

    public List<Tag> Tags { get; private set; } = new List<Tag>();
    public int StepChainLastId { get; private set; } = 1;
    
    public static Result<Chain> CreateChain(string pentestObj, User creator)
    {
        if (string.IsNullOrEmpty(pentestObj)) return Result.Failure<Chain>("Имя не должно быть пустым");
        var newChain = new Chain(pentestObj, creator);
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