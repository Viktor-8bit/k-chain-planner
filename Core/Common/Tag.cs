


using System.Diagnostics.CodeAnalysis;
using Core.Enums;
using CSharpFunctionalExtensions;

namespace Core.Common;

public class Tag(TagColor tagColor, string instrumentName)
{
    public TagColor TagColor { get; set; } = tagColor;
    public string InstrumentName { get; set; } = instrumentName;
    
    public static Result<Tag> CreateTag(TagColor tagColor, string instrumentName)
    {
        if (string.IsNullOrEmpty(instrumentName))
            return Result.Failure<Tag>("Имя инструмента не может быть пустым");
        var tag = new Tag(tagColor, instrumentName);
        return Result.Success(tag);
    }

    public Result<Tag> ChangeInstrumentName(string instrumentName)
    {
        if (string.IsNullOrEmpty(instrumentName))
            return Result.Failure<Tag>("инструмента не может быть пустым");
        InstrumentName = instrumentName;
        return Result.Success(this);
    }

    public Tag ChangeColor(TagColor color)
    {
        TagColor = tagColor;
        return this;
    }
}