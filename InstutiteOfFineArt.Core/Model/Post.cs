using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstutiteOfFineArt.Core.Model
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [Required(ErrorMessage = "Title name is required.")]
        [StringLength(255)]
        [Display(Name = "Post title")]
        public string Title { get; set; }


        [Column(TypeName = "ntext")]
        [Display(Name = "Content")]
        [StringLength(1024)]
        public string PostContent { get; set; }


        [Display(Name = "Is Published")]
        public bool Published { get; set; }

        [Column("Posted On")]
        [Display(Name = "Posted On")]
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public string Id { get; set; }
        public virtual User User { get; set; }
        public string Images { get; set; }
        public int CompetitionId { get; set; }
        public virtual Competition Competition { get; set; }
        public decimal? Mark { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceCustomer { get; set; }

        public bool? IsPaid { get; set; }
        public bool? IsSold { get; set; }
    }
}
