
using Core.Common;
using Core.Interfaces;
using CSharpFunctionalExtensions;

namespace Application.Services;

public class ChainStepService(IChainRepository chainRepository, IChainStepRepository chainStepRepository)
{

    public async Task<IEnumerable<ChainStep>?> GetChainStepsByFatherChain(int fatherChainId)
    {
        var chainSteps = await chainStepRepository.GetChainsStepsByFatherChain(fatherChainId);
        return chainSteps;
    }

    public async Task<Result<ChainStep>> AddChainStep(int fatherChainId, string title, string description, DateOnly? start, DateOnly? end)
    {
        var facherChain = await chainRepository.GetChainById(fatherChainId);
        if (facherChain == null) return Result.Failure<ChainStep>("Chain не найден");

        var result = ChainStep.CreateChainStep(title, description, facherChain, start, end);
        if (result.IsFailure) return Result.Failure<ChainStep>(result.Error);
        return await chainStepRepository.AddChainStep(fatherChainId, result.Value);
    }
    
    public async Task DeleteChainStep(int chainStepId, int fatherChainId) =>
        await chainStepRepository.DeleteChainStep(chainStepId, fatherChainId);
    
    public async Task<Result<ChainStep>> UpdateChainStep(int id, ChainStep chainToUppdate)
    {
        var chainStep = await chainStepRepository.GetChainStepById(id);
        if(chainStep == null) return Result.Failure<ChainStep>("ChainStep не найден");

        var result = chainStep.ChaingeDate(chainToUppdate.Start, chainToUppdate.End);
        if (result.IsFailure) return Result.Failure<ChainStep>(result.Error);

        result = chainStep.ChangeDescription(chainToUppdate.Description);
        if (result.IsFailure) return Result.Failure<ChainStep>(result.Error);

        result = chainStep.ChangeTitle(chainToUppdate.Title);
        if (result.IsFailure) return Result.Failure<ChainStep>(result.Error);
        
        await chainStepRepository.UpdateChainStep(chainStep);
        return Result.Success(chainStep);
    }

    public async Task<ChainStep?> GetChainStepById(int id) => 
        await chainStepRepository.GetChainStepById(id);
}