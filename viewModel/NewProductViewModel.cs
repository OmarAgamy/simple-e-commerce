using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhaseOne.Models;

namespace PhaseOne.viewModel
{
    public class NewProductViewModel
    {
        public Product Product { get; set; }

        public IEnumerable<Category> Categories { get; set; }
    }
}