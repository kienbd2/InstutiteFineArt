﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstutiteOfFineArt.Core.Model
{
    public class Awards
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Ranking { get; set; }
        public virtual IList<Post> Posts { get; set; }
        public virtual Competition  Competition { get; set; }
    }
}
