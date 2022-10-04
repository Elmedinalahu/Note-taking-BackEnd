namespace Notetaking.Models.DomainModels
{
    public class Note
    { 
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
