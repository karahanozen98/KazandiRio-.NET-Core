using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KazandiRio.Domain.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Username { get; set; }
        [Required]
        [StringLength(20)]
        public string Password { get; set; }
        [Required]
        [StringLength(20)]
        public string Role { get; set; }
        [Required]
        public float Balance { get; set; }
        [Required]
        public float Rewards { get; set; }
        public int? TokenId { get; set; }
        [ForeignKey("TokenId")]
        public RefreshToken Token { get; set; }

    }
}
