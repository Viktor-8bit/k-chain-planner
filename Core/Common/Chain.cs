
using Core.Enums;
namespace Core.Common;

public class Chain
{
    public required string? Title { get; set; }
    
    // тестируемый объект
    public required string PentestObj { get; set; }
    
    // используемые технологии
    public List<Tag>? Tags { get; set; }
    
}