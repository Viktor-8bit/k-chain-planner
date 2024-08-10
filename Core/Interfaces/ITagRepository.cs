using Core.Common;
using CSharpFunctionalExtensions;

namespace Core.Interfaces;

public interface ITagRepository
{
    
    public Task<IEnumerable<Tag>?> GetTags(); 
    public Task<Tag?> GetTagById(int tagId);
    public Task<Result<Tag>> CreateTag(Tag tag);
    public Task<Result<Tag>> DeleteTag(int tagId);
}