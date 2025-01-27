using System.ComponentModel.DataAnnotations;

namespace fanaticServe.Models
{
    public class Abustract_Album
    {
        [Key]
        public Guid Abustract_Album_Id { get; set; }
        public string? Title { get; set; }
        public string? Note { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }
    }
}
