using Core.Common;
using CSharpFunctionalExtensions;

namespace Core.Interfaces;

public interface IChainStepRepository
{
    Task<ChainStep?> GetChainStepById(int chainStepId);
    Task<IEnumerable<ChainStep>?> GetChainsStepsByFatherChain(int fatherChainId);
    Task<ChainStep> AddChainStep(int fatherChainId, ChainStep chainStep);
    Task UpdateChainStep(ChainStep chainStep);
    Task DeleteChainStep(int chainStepId, int fatherChainId);
    Task RecalculateChainStepByFatherChain(int fatherChainId);
}