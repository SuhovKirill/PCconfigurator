using PCConfigurator.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCConfigurator.ViewModel
{
    class CellListener
    {
        #region Fields

        private IItemCategory _category;

        #endregion

        #region Ctor

        public CellListener(IItemCategory category)
        {
            if (category == null) throw new ArgumentNullException("category");
            _category = category;
        }

        #endregion

        #region Public Properties

        private IItem _item;
        public IItem Item
        {
            get { return _item; }
            set { _item = value; }
        }


        public string NameCategory
        {
            get
            {
                return _category.Name;
            }
        }

        public int IdCategory
        {
            get
            {
                return _category.ID;
            }
        }

        #endregion
    }
}
