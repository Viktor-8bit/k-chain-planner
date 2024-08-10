
using System.Data.Entity;
using Core.Common;
using Infrastructure.Data;
using Core.Interfaces;

namespace Infrastructure.Repositories;

public class TagRepository(ApplicationContext _DbContext): ITagRepository
{
    public async Task<IEnumerable<Tag>?> GetTags() =>
        _DbContext.Tags.ToList<Tag>();


    public async Task<Tag?> GetTagById(int tagId)
    {
        return _DbContext.Tags.FirstOrDefault(c => c.Id == tagId);
    }

    public async Task CreateTag(Tag tag)
    {
        var checkTag = _DbContext.Tags
            .SingleOrDefault(tg => tg.InstrumentName == tag.InstrumentName);
  
        if (checkTag != null) return;
        
        _DbContext.Tags.Add(tag);
        await _DbContext.SaveChangesAsync();
    }

    public async Task DeleteTag(int tagId)
    {
        var tagToDelete = _DbContext.Tags
            .SingleOrDefault(tg => tg.Id == tagId);
        if (tagToDelete == null) return;
        _DbContext.Tags.Remove(tagToDelete);
        await _DbContext.SaveChangesAsync();
    }
}