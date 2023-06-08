using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PhaseOne.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter your Product Name")]
        [StringLength(250)]
        public String Name { get; set; }

        public double Price { get; set; }

        public String Image { get; set; }

        public String Description { get; set; }

        public Category Category { get; set; }

        public byte CategoryId { get; set; }
    }
}