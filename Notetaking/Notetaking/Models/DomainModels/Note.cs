using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Notetaking.Models.DomainModels
{
    public class Note
    { 
        public Guid Id { get; set; }
        public string? Heading { get; set; }
        public string? Text { get; set; }
        public DateTime DateCreated { get; set; }
        
        [ForeignKey("UserId")]
        public User? User { get; set; } 
    }
}
