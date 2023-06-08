using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PhaseOne.Models
{
    public class Category
    {
        public byte Id { get; set; }

        [Required]
        public String Name { get; set; }

        public int No_Of_Products { get; set; }
    }
}