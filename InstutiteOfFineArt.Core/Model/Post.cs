using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstutiteOfFineArt.Core.Model
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title name is required.")]
        [StringLength(255)]
        [Display(Name = "Post title")]
        public string Title { get; set; }


        [Column(TypeName = "ntext")]
        [Display(Name = "Content")]
        [StringLength(1024)]
        public string PostContent { get; set; }

        [StringLength(255)]
        [Display(Name = "Url")]
        public string UrlSlug { get; set; }

        [Display(Name = "Is Published")]
        public bool Published { get; set; }

        [Column("Posted On")]
        [Display(Name = "Posted On")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PostedOn { get; set; }
        //[UIHint("_DisplayDatetime")]

        public DateTime? Modified { get; set; }


        public int? RateCount { get; set; }
        public int? TotalRate { get; set; }
    }
}
