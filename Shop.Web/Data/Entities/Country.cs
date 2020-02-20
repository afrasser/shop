using System.ComponentModel.DataAnnotations;

namespace Shop.Web.Data.Entities
{
    public class Country : IEntity
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} can contain {1} characters")]
        [Required]
        public string Name { get; set; }
    }
}
