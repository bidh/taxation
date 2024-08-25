using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Taxation.DAL.Models
{
    [Table("Yearlytax")]
    public class Yearlytax
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required Municipality Municipality { get; set; }
        public required DateTimeOffset StartDate { get; set; }
        public required DateTimeOffset EndDate { get; set; }
        public required decimal Tax { get; set; }
    }
}
