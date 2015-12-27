using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls;
using PCConfigurator.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCConfigurator.ViewModel
{
    class AddNewCategoryViewModel : ViewModelBase
    {
        #region Fields

        IConnectionDb _connection;

        #endregion

        #region Ctor

        public AddNewCategoryViewModel(IConnectionDb connection)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            _connection = connection;
        }

        #endregion

        #region Public Properties

        private string _nameNewCategory;
        public string Name
        {
            get { return _nameNewCategory; }
            set { _nameNewCategory = value; }
        }

        private string _descriptionNewCategory;
        public string Description
        {
            get { return _descriptionNewCategory; }
            set { _descriptionNewCategory = value; }
        }

        #endregion

        #region Commands

        private RelayCommand<MetroWindow> _butSave;
        public RelayCommand<MetroWindow>  ButSave
        {
            get
            {
                if (_butSave == null)
                    _butSave = new RelayCommand<MetroWindow>(butSaveExecute);

                return _butSave;
            }
        }

        private void butSaveExecute(MetroWindow window)
        {
            if (window == null) throw new ArgumentNullException("window");

            if (!IsValidNewCategory())
                return;

            _connection.AddNewCategory(Name, Description);

            window.Close();
        }

        #endregion

        #region Private Methods

        private bool IsValidNewCategory()
        {
            if (string.IsNullOrWhiteSpace(Name)) return false;
            return true;
        }

        #endregion
    }
}
