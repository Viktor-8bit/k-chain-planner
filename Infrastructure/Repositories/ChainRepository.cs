
using Core.Common;
using Infrastructure.Data;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ChainRepository(ApplicationContext _DbContext): IChainRepository
{
    public async Task<IEnumerable<Chain>?> GetChains() => 
        _DbContext.Chains.ToList<Chain>();

    public async Task<Chain?> GetChainById(int chainId)
    {
        return await _DbContext.Chains
            .FirstOrDefaultAsync(c => c.Id == chainId);
    }

    public async Task<Chain> CreateChain(Chain chain)
    {
        _DbContext.Chains.Add(chain);
        await _DbContext.SaveChangesAsync();
        return chain;
    }

    public async Task UpdatePentestObj(int chainId, string pentestObj)
    {
        var chainToUpdate = _DbContext.Chains.FirstOrDefault(c => c.Id == chainId);
        if (chainToUpdate != null)
        {
            chainToUpdate.ChangePentestObj(pentestObj);
            await _DbContext.SaveChangesAsync();
        }
    }

    public async Task AddTag(int chainId, int tagId)
    {
        var chainToUpdate = _DbContext.Chains.FirstOrDefault(c => c.Id == chainId);
        if (chainToUpdate == null) return;
        
        var tagToAdd = _DbContext.Tags.FirstOrDefault(tg => tg.Id == tagId);
        if (tagToAdd == null) return;
        
        chainToUpdate.AddTag(tagToAdd);
        await _DbContext.SaveChangesAsync();
    }

    public async Task RemoveTag(int chainId, int tagId)
    {
        
        var chainToUpdate = _DbContext.Chains.FirstOrDefault(c => c.Id == chainId);
        if (chainToUpdate == null) return;
        
        var tagToRemove = _DbContext.Tags.FirstOrDefault(tg => tg.Id == tagId);
        if (tagToRemove == null) return;
        
        chainToUpdate.RemoveTag(tagToRemove);
        await _DbContext.SaveChangesAsync();
        
    }

    public async Task DeleteChain(int chainId)
    {
        var chainToDelete = _DbContext.Chains.FirstOrDefault(c => c.Id == chainId);
        if (chainToDelete != null)
        {
            _DbContext.Chains.Remove(chainToDelete);
            await _DbContext.SaveChangesAsync();
        }
    }
}