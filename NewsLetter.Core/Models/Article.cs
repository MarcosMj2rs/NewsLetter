namespace NewsLetter.Core.Models
{
    public sealed record Article(Guid Id,
                                 string Title,
                                 string url,
                                 string Content,
                                 DateTime PublishDate);

    
}
