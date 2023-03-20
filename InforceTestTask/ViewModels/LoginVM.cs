using System.ComponentModel.DataAnnotations;

namespace InforceTestTask.ViewModels;

public class LoginVM
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Password { get; set; } = null!;
    public bool RememberMe { get; set; }
}
