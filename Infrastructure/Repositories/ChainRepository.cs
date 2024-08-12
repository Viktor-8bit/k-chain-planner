
using Core.Common;
using Infrastructure.Data;
using Core.Interfaces;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ChainRepository(ApplicationContext _DbContext): IChainRepository
{
    public async Task<IEnumerable<Chain>?> GetChains() => 
        _DbContext.Chains
            .Include(c => c.Tags)
            .ToList<Chain>();

    public async Task<Chain?> GetChainById(int chainId)
    {
        return await _DbContext.Chains
            .Include(c => c.Tags)
            .FirstOrDefaultAsync(c => c.Id == chainId);
    }

    public async Task<Chain> CreateChain(Chain chain)
    {
        _DbContext.Chains.Add(chain);
        await _DbContext.SaveChangesAsync();
        return chain;
    }

    public async Task<Result<Chain?>> UpdatePentestObj(int chainId, string pentestObj)
    {
        var chainToUpdate = _DbContext.Chains.FirstOrDefault(c => c.Id == chainId);
        if (chainToUpdate != null)
        {
            var result = chainToUpdate.ChangePentestObj(pentestObj);
            if (result.IsFailure) return Result.Failure<Chain?>(result.Error);
            await _DbContext.SaveChangesAsync();
            return Result.Success<Chain?>(chainToUpdate);
        } else return Result.Failure<Chain?>("Цепочка не найдена");
    }

    public async Task<Result<Chain?>> AddTag(int chainId, int tagId)
    
    {
        var chainToUpdate = _DbContext.Chains
            .Include(c => c.Tags)
            .FirstOrDefault(c => c.Id == chainId);
        
        if (chainToUpdate == null) 
            return Result.Failure<Chain?>("Цепочка не найдена");
        
        var tagToAdd = _DbContext.Tags.FirstOrDefault(tg => tg.Id == tagId);
        
        if (tagToAdd == null) 
            return Result.Failure<Chain?>("Тег не найден");
        
        var result = chainToUpdate.AddTag(tagToAdd);
        if (result.IsFailure) return Result.Failure<Chain?>(result.Error);
        
        await _DbContext.SaveChangesAsync();
        return Result.Success<Chain?>(chainToUpdate);
    }

    public async Task<Result<Chain?>> RemoveTag(int chainId, int tagId)
    {
        var chainToUpdate = _DbContext.Chains
            .Include(c => c.Tags)
            .FirstOrDefault(c => c.Id == chainId);
        if (chainToUpdate == null) return Result.Failure<Chain?>("Цепочка не найдена");
        
        var tagToRemove = _DbContext.Tags.FirstOrDefault(tg => tg.Id == tagId);
        if (tagToRemove == null) return Result.Failure<Chain?>("Тег не найден");
        
        chainToUpdate.RemoveTag(tagToRemove);
        await _DbContext.SaveChangesAsync();
        return Result.Success<Chain?>(chainToUpdate);
    }

    public async Task DeleteChain(int chainId)
    {
        var chainToDelete = _DbContext.Chains.FirstOrDefault(c => c.Id == chainId);
        var chainsSteps = _DbContext.ChainSteps.Where(cs => cs.FatherChain.Id == chainId).ToList();
        if (chainToDelete != null)
        {
            _DbContext.ChainSteps.RemoveRange(chainsSteps);
            _DbContext.Chains.Remove(chainToDelete);
            await _DbContext.SaveChangesAsync();
        }
    }
}