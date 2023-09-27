using System.ComponentModel.DataAnnotations.Schema;

namespace WEB_API.Models
{
    public class Game{
        public string Id { get; set; } = new Guid().ToString();
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Images { get; set; }
        public Info? Info { get; set; }
        public SystemRequirements? SystemRequirements { get; set; }
        public virtual int CategoryId { get; set; } 
        public Category Category { get; set; }
    }
}
