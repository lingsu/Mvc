using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Site.Models
{
    public class ProductStaticView
    {
        public int Id { get; set; }
        public string ProName { get; set; }
        public string ProUrl { get; set; }
        public string ProPic { get; set; }
        public int CategoryId { get; set; }
    }
}
