
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

    public async Task<Chain> AddChain(Chain chain)
    {
        _DbContext.Chains.Add(chain);
        await _DbContext.SaveChangesAsync();
        return chain;
    }

    public async Task UpdateChain(Chain chain)
    {
        var chainToUpdate = _DbContext.Chains.FirstOrDefault(c => c.Id == chain.Id);
        if (chainToUpdate != null)
        {
            chainToUpdate.ChangePentestObj(chain.PentestObj);
            await _DbContext.SaveChangesAsync();
        }
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