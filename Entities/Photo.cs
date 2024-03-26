using System.ComponentModel.DataAnnotations.Schema;

namespace dating_backend.Entities
{
    [Table("Photos")] // Override EF to make it a table.
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public int UserId { get; set; } // Convention to create a foreign key for userId
        public User User { get; set; } // Convention to create a foreign key for userId
    }
}