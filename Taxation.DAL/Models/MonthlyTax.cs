using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Taxation.DAL.Models
{
    [Table("MonthlyTax")]
    public class MonthlyTax
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required Municipality Municipality { get; set; }
        public decimal Tax { get; set; }
        public required DateTimeOffset StartDate { get; set; }
        public required DateTimeOffset EndDate { get; set; }
    }
}
