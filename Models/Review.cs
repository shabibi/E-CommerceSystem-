using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceSystem.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        [Required]
        [Range(1,5)]
        public int Rating { get; set; }

        public string Comment { get; set; } = null;

        public DateTime ReviewDate { get; set; }

        [ForeignKey("User")]
        public int UID { get; set; }
        public User user { get; set; }

        [ForeignKey("Product")]
        public int PID { get; set; }
        public Product product { get; set; }
    }
}
