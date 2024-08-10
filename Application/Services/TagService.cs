



using Core.Common;
using Core.Interfaces;
using CSharpFunctionalExtensions;

namespace Application.Services;

public class TagService(ITagRepository tagRepository)
{
    
    public async Task<IEnumerable<Tag>?> GetTags() => 
        await tagRepository.GetTags();

    public async Task<Result<Tag>> CreateTag(Tag tag)
    {
        var result = await tagRepository.CreateTag(tag);
        return result;
    }

    public async Task<Result<Tag>> DeleteTag(int tagId)
    {
        var result = await tagRepository.DeleteTag(tagId);
        if (result.IsFailure)
            return Result.Failure<Tag>(result.Error);
        return Result.Success(result.Value);
    }
}