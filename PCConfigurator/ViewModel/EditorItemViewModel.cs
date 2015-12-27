using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls;
using PCConfigurator.Model.Database;
using PCConfigurator.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCConfigurator.ViewModel
{
    class EditorItemViewModel : ViewModelBase
    {
        #region Fields

        IConnectionDb _connection;

        #endregion

        #region Ctor

        public EditorItemViewModel(IConnectionDb connection)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            _connection = connection;
        }

        #endregion

        #region Public Properties

        IItemCategory _selectedCategory;
        public IItemCategory SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                _selectedCategory = value;
                RaisePropertyChanged("SelectedCategory");
            }
        }


        private ObservableCollection<IItemCategory> _cats;
        public ObservableCollection<IItemCategory> Cats
        {
            get
            {
                if (_cats == null)
                {
                    _cats = new ObservableCollection<IItemCategory>();
                    foreach (var z in _connection.Categories)
                        _cats.Add(z);
                }
                return _cats;
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        #endregion

        #region Commands

        private RelayCommand<MetroWindow> _butSave;
        public RelayCommand<MetroWindow> ButSave
        {
            get
            {
                if (_butSave == null)
                {
                    _butSave = new RelayCommand<MetroWindow>(butSaveExecute);
                }
                return _butSave;
            }
        }

        private void butSaveExecute(MetroWindow currentWindow)
        {
            if (currentWindow == null) throw new ArgumentNullException("currentWindow");

            if (!IsValid())
                return;

            _connection.AddNewItem(Name, _selectedCategory.ID, Description);
            currentWindow.Close();
        }

        #endregion

        #region Private Methods

        private bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(Name))
                return false;

            if (_selectedCategory == null)
                return false;

            if (_connection.Items.FirstOrDefault(item => item.Name == Name) != null)
                return false;

            return true;
        }

        #endregion
    }
}
