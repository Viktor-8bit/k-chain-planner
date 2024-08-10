
using System.Data.Entity;
using Core.Common;
using Infrastructure.Data;
using Core.Interfaces;
using CSharpFunctionalExtensions;

namespace Infrastructure.Repositories;

public class TagRepository(ApplicationContext _DbContext): ITagRepository
{
    public async Task<IEnumerable<Tag>?> GetTags() =>
        _DbContext.Tags.ToList<Tag>();


    public async Task<Tag?> GetTagById(int tagId)
    {
        return _DbContext.Tags.FirstOrDefault(c => c.Id == tagId);
    }

    public async Task<Result<Tag>> CreateTag(Tag tag)
    {
        var checkTag = _DbContext.Tags
            .SingleOrDefault(tg => tg.InstrumentName.ToLower() == tag.InstrumentName.ToLower());
  
        if (checkTag != null) 
            return Result.Failure<Tag>("Такой тэг уже существует");
        
        _DbContext.Tags.Add(tag);
        await _DbContext.SaveChangesAsync();
        return Result.Success(tag);
    }

    public async Task<Result<Tag>> DeleteTag(int tagId)
    {
        var tagToDelete = _DbContext.Tags
            .SingleOrDefault(tg => tg.Id == tagId);
        if (tagToDelete == null) 
            return Result.Failure<Tag>("Тэг не существует");
        _DbContext.Tags.Remove(tagToDelete);
        await _DbContext.SaveChangesAsync();
        return Result.Success(tagToDelete);
    }
    
}