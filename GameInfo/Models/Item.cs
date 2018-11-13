using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfo.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string AcquiredFrom { get; set; }

        public string Usage { get; set; }
    }
}
