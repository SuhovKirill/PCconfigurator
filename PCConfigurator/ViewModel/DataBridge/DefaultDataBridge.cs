using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCConfigurator.Model.Interfaces;

namespace PCConfigurator.ViewModel.DataBridge
{
    class DefaultDataBridge : IDataBridge
    {
        #region Public Properties

        private IPcAssembly _currentAssembly;
        public  IPcAssembly currentAssembly
        {
            get
            {
                return _currentAssembly;
            }
            set
            {
                /*
                    Обратите внимание - сделано в Германии!. Шутка
                    Коммент к тому, что тут допускается согласно логике присваивание null
                */
                _currentAssembly = value;
            }
        }

        private IItemCategory _currentCategory;
        public IItemCategory currentCategory
        {
            get
            {
                return _currentCategory;
            }
            set
            {   /*
                    Обратите внимание - сделано в Германии!. Шутка
                    Коммент к тому, что тут допускается согласно логике присваивание null
                */
                _currentCategory = value;
            }
        }

        #endregion
    }
}
