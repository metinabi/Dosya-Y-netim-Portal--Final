namespace uyg05_IdentityApp.Models
{
    public class FileManage
    {
        public int Id { get; set; }
        public string Name { get; set;}
        public string Description { get; set;}
        public string Link { get; set;}
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
