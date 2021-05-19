using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class AssetFile
    {
        public Guid Id { get; set; }

        public Guid AssetId { get; set; }

        public virtual Asset Asset { get; set; }

        public string Name { get; set; }
    }
}
