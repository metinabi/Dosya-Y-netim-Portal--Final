namespace uyg05_IdentityApp.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public List<FileManage> FileManages { get; set;}
    }
}
