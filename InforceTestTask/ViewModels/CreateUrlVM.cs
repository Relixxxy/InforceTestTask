using System.ComponentModel.DataAnnotations;

namespace InforceTestTask.ViewModels;

public class CreateUrlVM
{
    [Required(ErrorMessage = "Please enter a URL")]
    [RegularExpression(@"^https?:\/\/[\w\-]+(\.[\w\-]+)+[/#?]?.*$", ErrorMessage = "Please enter a valid URL")]
    public string OriginalUrl { get; set; } = null!;
}
