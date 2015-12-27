using PCConfigurator.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCConfigurator.Model.Database
{
    interface IConnectionDb : INotifyPropertyChanged
    {
        #region Public Methods

        bool AddNewCategory(string nameCategory, string description);
        bool UpdateCategory(IItemCategory itemCategory);
        bool RemoveCategory(int id);

        bool AddNewItem(string name, int idCategory, string description);
        bool UpdateItem(IItem item);
        bool RemoveItem(int id);

        IPcAssembly AddNewAssembly(string name, IList<IItem> items);
        bool RemoveAssembly(int id);
        bool UpdateAssembly(IPcAssembly assembly);

        #endregion

        #region Public Properties

        IList<IItemCategory> Categories { get; }
        IList<IItem> Items { get; }
        IList<IPcAssembly> Assemblies { get; }

        #endregion
    }
}
