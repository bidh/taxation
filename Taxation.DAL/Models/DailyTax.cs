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

        [ForeignKey("MunicipalityId")]
        public Municipality Municipality { get; set; }
        public int MunicipalityId { get; set; }
        public decimal Tax { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
