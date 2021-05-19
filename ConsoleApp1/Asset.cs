using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Asset 
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<AssetFile> Files { get; set; }
    }
}
