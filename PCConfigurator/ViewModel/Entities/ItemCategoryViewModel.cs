using GalaSoft.MvvmLight.Command;
using PCConfigurator.Model.Database;
using PCConfigurator.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCConfigurator.ViewModel.Entities
{
    class ItemCategoryViewModel
    {
        #region Fields

        IConnectionDb _connection;
        IItemCategory _itemCategory;

        #endregion

        #region Ctor

        public ItemCategoryViewModel(IConnectionDb connection, IItemCategory category)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (category == null) throw new ArgumentNullException("category");

            _connection = connection;
            _itemCategory = category;
        }

        #endregion

        #region Public Properties

        public string Name
        {
            get
            {
                return _itemCategory.Name;
            }
            set
            {
                _itemCategory.Name = value;
            }
        }

        #endregion

        #region Commands

        private RelayCommand _butDelete;
        public RelayCommand ButDelete
        {
            get
            {
                if (_butDelete == null)
                    _butDelete = new RelayCommand(() =>
                    {
                        _connection.RemoveCategory(_itemCategory.ID);
                    });

                return _butDelete;
            }
        }

        #endregion
    }
}
