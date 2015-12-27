using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCConfigurator.Model.Interfaces
{
    interface IPcAssembly
    {
        IList<IItem> Items { get; set; }
        string Name { get; set; }
        int ID { get; }
    }
}
