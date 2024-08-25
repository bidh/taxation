using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Taxation.DAL.Models
{
    [Table("DailyTax")]
    public class DailyTax
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required Municipality Municipality { get; set; }
        public decimal Tax { get; set; }
        public required DateTimeOffset Date { get; set; }
    }
}
