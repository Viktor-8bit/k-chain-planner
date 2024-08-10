using Core.Common;

namespace Core.Interfaces;

public interface IChainRepository
{
    Task<IEnumerable<Chain>?> GetChains();
    Task<Chain?> GetChainById(int chainId);
    Task<Chain> CreateChain(Chain chain);
    Task DeleteChain(int chainId);
    
    
    Task UpdatePentestObj(int chainId, string pentestObj);
    Task AddTag(int chainId, int tagId);
    Task RemoveTag(int chainId, int tagId);
}