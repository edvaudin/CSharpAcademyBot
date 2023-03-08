using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSharpAcademyBot.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public long DiscordId { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    public UserReputation Reputation { get; set; }
}
