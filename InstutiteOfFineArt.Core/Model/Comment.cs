using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstutiteOfFineArt.Core.Model
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string Id { get; set; }
        public string Avartar { get; set; }
        public string UserName { get; set; }
        public virtual User User { get; set; }
        public int PostId { get; set; }
        public int Mark { get; set; }
        public Post Post { get; set; }
        [NotMapped]
        public string PassWord { get; set; }
        [StringLength(255)]
        [Required(ErrorMessage = "Comment header is required.")]

        [Display(Name = "Comment")]
        public string CommentText { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
