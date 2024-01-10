using System.ComponentModel.DataAnnotations.Schema;
using uyg05_IdentityApp.Models;

namespace uyg05_IdentityApp.ViewModels
{
    public class FileManageModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
        public Category Category { get; set; }
    }
}
