


using Core.Enums;

namespace WebApi.Contracts;

public record TagResponce
{
    public int Id { get; set; }
    public TagColor TagColor { get; set; }
    public string InstrumentName { get; set; } = "";
    public string InstrumentUrl { get; set; } = "";
}