



using Core.Common;
using Core.Interfaces;

namespace Application.Services;

public class TagService(ITagRepository tagRepository)
{
    
    public async Task<IEnumerable<Tag>?> GetTags() => 
        await tagRepository.GetTags();
    
    public async Task CreateTag(Tag tag) =>
        await tagRepository.CreateTag(tag);
    
    public async Task DeleteTag(int tagId) =>
        await tagRepository.DeleteTag(tagId);
}