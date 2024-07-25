namespace ParserCombinators.Server.DataLayer;

public sealed class NewsEntity
{
    public int ID { get; set; }

    public string? Text { get; set; }

    public string? Filter { get; set; }
}