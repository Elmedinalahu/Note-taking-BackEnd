using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Notetaking.Models.DomainModels
{
    public class Note
    { 
        [Key]
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime DateCreated { get; set; }
        
        [ForeignKey("UserId")]
        public User? User { get; set; } 
    }
}
