using System.ComponentModel.DataAnnotations;

namespace Kick_X.Models;
public class Admin
{
    [Key]
    public int admin_id { get; set;}

    [Required(ErrorMessage = "*")]
    [MaxLength(20, ErrorMessage = "Too long*")]
    public string? admin_name { get; set;}

    [MaxLength(50, ErrorMessage = "Too long*")]
    [Required(ErrorMessage = "*")]
    public string? admin_email { get; set;}

    [Required(ErrorMessage = "*")]
    [MinLength(3, ErrorMessage = "Too short*")]
    [MaxLength(20, ErrorMessage = "Too long*")]
    public string? admin_password { get; set;}
}