namespace InforceTestTask.ViewModels;

public class ShortUrlVM
{
    public int Id { get; set; }
    public string CreatedBy { get; set; } = null!;
    public string OriginalUrl { get; set; } = null!;
    public string ShortUrl { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
}
