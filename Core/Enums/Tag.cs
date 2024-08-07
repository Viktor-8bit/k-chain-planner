using System.Diagnostics.CodeAnalysis;

namespace Core.Enums;



public class Tag
{
    public TagColor TagColor { get; set; }  
    public required string InstrumentName { get; set; }
}