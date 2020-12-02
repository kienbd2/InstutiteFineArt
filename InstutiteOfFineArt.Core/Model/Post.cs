using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstutiteOfFineArt.Core.Model
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [Required(ErrorMessage = "Title name is required.")]
        [StringLength(255, MinimumLength = 60, ErrorMessage = "Quotations, Stories should be between 60 characters and 200 characters only")]
        [Display(Name = "Post title")]
        public string Title { get; set; }

        [StringLength(1024, MinimumLength = 200, ErrorMessage = "Quotations, Stories should be between 200 characters and 1000 characters only")]
        [Column(TypeName = "ntext")]
        [Display(Name = "Content")]
        [Required(ErrorMessage = "Quotations, Stories is required.")]
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
        public virtual IList<Comment> Comments { get; set; }
    }
}
