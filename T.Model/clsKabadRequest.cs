using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kabadi.Model
{
    public class clsKabadRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string phone { get; set; }
        public string Message { get; set; }

        public int ItemTypeId { get; set; }
        public string Item { get; set; }
        public string Location { get; set; }
        public int LocationId { get; set; }
    }
}
