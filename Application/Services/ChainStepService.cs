﻿
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
        facherChain.IncreaseChainStepLastId();
        return await chainStepRepository.AddChainStep(fatherChainId, result.Value);
    }
    
    public async Task DeleteChainStep(int chainStepId, int fatherChainId)
    {
        var fatherChainStep = await chainRepository.GetChainById(chainStepId);
        var chainStep = await chainStepRepository.GetChainStepById(chainStepId);
        
        if(chainStep == null) return;
        if (fatherChainStep != null) fatherChainStep.DecreaseChainStepLastId();
        
        await chainStepRepository.DeleteChainStep(chainStepId, fatherChainId);
    } 
    
    public async Task<Result<ChainStep>> UpdateChainStep(int id, string title, string description, DateOnly? start, DateOnly? end)
    {
        var chainStep = await chainStepRepository.GetChainStepById(id);
        if(chainStep == null) return Result.Failure<ChainStep>("ChainStep не найден");
        
        var result = chainStep.ChangeEndDate(end);
        if (result.IsFailure) return Result.Failure<ChainStep>(result.Error);

        result = chainStep.ChangeStartDate(start);
        if (result.IsFailure) return Result.Failure<ChainStep>(result.Error);

        result = chainStep.ChangeDescription(description);
        if (result.IsFailure) return Result.Failure<ChainStep>(result.Error);

        result = chainStep.ChangeTitle(title);
        if (result.IsFailure) return Result.Failure<ChainStep>(result.Error);

        return Result.Success(chainStep);
    }


    
}