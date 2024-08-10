

using Core.Common;


namespace WebApi.Contracts;

public record ChainResponce
{
    public int Id { get; set; }
    public string PentestObj { get; set; }
    public List<Tag> Tags { get; set; }
}