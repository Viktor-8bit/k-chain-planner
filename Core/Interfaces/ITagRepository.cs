using Core.Common;

namespace Core.Interfaces;

public interface ITagRepository
{
    
    public Task<IEnumerable<Tag>?> GetTags(); 
    public Task<Tag?> GetTagById(int tagId);
    public Task CreateTag(Tag tag);
    
    public Task DeleteTag(int tagId);
}