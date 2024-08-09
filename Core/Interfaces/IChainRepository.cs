using Core.Common;

namespace Core.Interfaces;

public interface IChainRepository
{
    Task<IEnumerable<Chain>?> GetChains();
    Task<Chain?> GetChainById(int chainId);
    Task<Chain> AddChain(Chain chain);
    Task UpdateChain(Chain chain);
    Task DeleteChain(int chainId);
}