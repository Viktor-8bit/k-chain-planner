﻿

using Core.Common;

namespace WebApi.Contracts;

public record ChainRequest
{
    public string PentestObj { get; set; }
    
    public int CreatorId { get; set; }
    public List<Tag> Tags { get; set; }
}