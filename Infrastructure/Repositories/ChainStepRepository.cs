

using Core.Common;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ChainStepRepository(ApplicationContext _DbContext): IChainStepRepository
{
    public async Task<ChainStep?> GetChainStepById(int chainStepId)
    {
        var chainStep = _DbContext.ChainSteps.FirstOrDefault(cs => cs.Id == chainStepId);
        return chainStep;
    }

    public async Task<IEnumerable<ChainStep>?> GetChainsStepsByFatherChain(int fatherChainId)
    {
        var fatherChain = _DbContext.Chains.FirstOrDefault(fc => fc.Id == fatherChainId);
        if (fatherChain == null) return null;
        List<ChainStep>? chainSteps = _DbContext.ChainSteps
            .Where(cs => cs.FatherChain.Id == fatherChain.Id)
            .ToList();
        return chainSteps;
    }

    public async Task<ChainStep?> AddChainStep(int fatherChainId, ChainStep chainStep)
    {
        var fatherChain = _DbContext.Chains.FirstOrDefault(fc => fc.Id == fatherChainId);
        if (fatherChain == null) return null;
        
        chainStep.FatherChain = fatherChain;
        _DbContext.ChainSteps.Add(chainStep);
        await _DbContext.SaveChangesAsync();
        return chainStep;
    }

    public async Task UpdateChainStep(ChainStep chainStep)
    {
        var chainStepToUpdate = _DbContext.ChainSteps.FirstOrDefault(cs => cs.Id == chainStep.Id);
        if (chainStepToUpdate == null) return;

        chainStepToUpdate.ChangeDescription(chainStep.Description);
        chainStepToUpdate.ChangeTitle(chainStep.Title);
        chainStepToUpdate.ChangeEndDate(chainStep.End);
        chainStepToUpdate.ChangeStartDate(chainStep.Start);
        
        await _DbContext.SaveChangesAsync();
    }

    public async Task DeleteChainStep(int chainStepId, int fatherChainId)
    {
        var chainStep = _DbContext.ChainSteps.FirstOrDefault(cs => cs.Id == chainStepId);
        if (chainStep == null) return;
        _DbContext.ChainSteps.Remove(chainStep);

        var fatherChain = _DbContext.Chains.FirstOrDefault(fc => fc.Id == fatherChainId);
        if (fatherChain != null) fatherChain.DecreaseChainStepLastId();
        
        await _DbContext.SaveChangesAsync();
    }
}