namespace Notetaking.Models.DTO
{
    public class Note
    {        
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
