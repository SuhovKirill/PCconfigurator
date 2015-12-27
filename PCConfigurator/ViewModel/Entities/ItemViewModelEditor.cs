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
    class ItemViewModelEditor
    {
        #region Fields

        private IItem _item;
        private IConnectionDb _connection;

        #endregion

        #region Ctor

        public ItemViewModelEditor(IItem item, IConnectionDb connection)
        {
            if (item == null) throw new ArgumentNullException("item");
            if (string.IsNullOrWhiteSpace(item.Name)) throw new ArgumentException("item");
            if (connection == null) throw new ArgumentNullException("connection");

            _item = item;
            _connection = connection;
        }

        #endregion

        #region Public Properties

        public string Name
        {
            get
            {
                return _item.Name;
            }
            set
            {
                _item.Name = value;
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
                        _connection.RemoveItem(_item.ID);
                    });

                return _butDelete;
            }
        }

        #endregion
    }
}
