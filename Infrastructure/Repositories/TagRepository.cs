
using Core.Common;
using Infrastructure.Data;
using Core.Interfaces;

namespace Infrastructure.Repositories;

public class TagRepository(ApplicationContext _DbContext): ITagRepository
{
    public async Task<Tag?> GetTagById(int tagId)
    {
        return _DbContext.Tags.FirstOrDefault(c => c.Id == tagId);
    }

    public async Task AddTag(int chainId, Tag tag)
    {
        var chain = _DbContext.Chains.FirstOrDefault(c => c.Id == chainId);
        if (chain == null) return;
        chain.AddTag(tag);
        await _DbContext.SaveChangesAsync();
    }

    public async Task RemoveTag(int chainId, int tagId)
    {
        var chain = _DbContext.Chains.FirstOrDefault(c => c.Id == chainId);
        if (chain == null) return;

        var tag = _DbContext.Tags.FirstOrDefault(t => t.Id == tagId);
        if (tag == null) return;
        
        chain.RemoveTag(tag);
        await _DbContext.SaveChangesAsync();
    }
}