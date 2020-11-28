using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstutiteOfFineArt.Core.Model
{
    public class Competition
    {
        [Key]
        public int CompetitionId { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Departure date is required")]
        [Display(Name = "Start Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy H:mm:ss tt}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public virtual IList<Post> Posts { get; set; }
    }
}
