using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class CommandItem
    {
        [Key]
        public int CommandId;
        [Key]
        public int ProductID;
        public int Quantity { get; set; } 

      }
}