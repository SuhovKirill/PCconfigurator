using PCConfigurator.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCConfigurator.ViewModel.DataBridge
{
    interface IDataBridge
    {
        IItemCategory currentCategory { get; set; }
        IPcAssembly currentAssembly { get; set; }
    }
}
