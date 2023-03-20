using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace InforceTestTask.ViewModels;

public class RegisterVM
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Password { get; set; } = null!;
    [Required]
    [DataType(DataType.EmailAddress)]
    [Compare(nameof(Password), ErrorMessage = "Your passwords didn't match")]
    public string ConfirmPassword { get; set; } = null!;
}
