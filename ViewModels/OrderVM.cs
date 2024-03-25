using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class OrderVM
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public string OrderBy { get; set; }
    }
}
