using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_CommerceSystem.Models
{
    [PrimaryKey(nameof(OID), nameof(PID))]
    public class OrderProducts
    {

        [Range (0, int.MaxValue)]
        public int Quantity { get; set; }

        [JsonIgnore]
        [ForeignKey("Order")]
        public int OID { get; set; }
        public Order Order { get; set; }

        [JsonIgnore]
        [ForeignKey("Product")]
        public int PID { get; set; }
        public Product product { get; set; }
    }
}

