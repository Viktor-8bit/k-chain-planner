using Core.Common;

namespace Core.Interfaces;

public interface ITagRepository
{
    public Task<Tag?> GetTagById(int tagId);
    public Task AddTag(int chainId);
    public Task RemoveTag(int chainId);
}