


using Core.Enums;
using CSharpFunctionalExtensions;

namespace Core.Common;

public class Tag
{

    public Tag(int id, TagColor tagColor, string instrumentName, string instrumentUrl)
    {
        Id = id;
        TagColor = tagColor;
        InstrumentName = instrumentName;
        InstrumentUrl = instrumentUrl;
    }
    
    public Tag(TagColor tagColor, string instrumentName, string instrumentUrl)
    {
        TagColor = tagColor;
        InstrumentName = instrumentName;
        InstrumentUrl = instrumentUrl;
    }


    private Tag() {}
    
    public int Id { get; private set; }
    public TagColor TagColor { get; set; }
    public string InstrumentName { get; private set; }
    public string InstrumentUrl { get; private set; }
    public static Result<Tag> CreateTag(TagColor tagColor, string instrumentName, string instrumentUrl)
    {
        if (string.IsNullOrEmpty(instrumentName))
            return Result.Failure<Tag>("Имя инструмента не может быть пустым");
        var tag = new Tag(tagColor, instrumentName, instrumentUrl);
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
        TagColor = color;
        return this;
    }
}