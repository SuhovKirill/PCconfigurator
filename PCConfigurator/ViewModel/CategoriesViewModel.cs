using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls;
using PCConfigurator.Model.Database;
using PCConfigurator.Model.Interfaces;
using PCConfigurator.View.Windows;
using PCConfigurator.ViewModel.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PCConfigurator.ViewModel
{
    class CategoriesViewModel : ViewModelBase
    {
        #region Fields

        IConnectionDb _connection;

        #endregion

        #region Ctor

        public CategoriesViewModel(IConnectionDb connection)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            _connection = connection;
            _connection.PropertyChanged += _connection_PropertyChanged;
        }


        #endregion

        #region Public Properties

        private ObservableCollection<ItemCategoryViewModel> _cats;
        public ObservableCollection<ItemCategoryViewModel> Categories
        {
            get
            {
                if (_cats == null)
                {
                    _cats = new ObservableCollection<ItemCategoryViewModel>();
                    foreach (var z in _connection.Categories)
                        _cats.Add(new ItemCategoryViewModel(_connection, z));

                }
                return _cats;
            }
        }

        #endregion

        #region Commands

        private RelayCommand<Flyout> _showButtons;
        public RelayCommand<Flyout> ShowButtons
        {
            get
            {
                if (_showButtons == null)
                    _showButtons = new RelayCommand<Flyout>(showButtonsExecute);

                return _showButtons;
            }
        }

        private void showButtonsExecute(Flyout flyout)
        {
            if (flyout == null) throw new ArgumentNullException("flyout");
            flyout.IsOpen = !flyout.IsOpen;
        }

        private RelayCommand<MetroWindow> _butBack;
        public RelayCommand<MetroWindow> ButBack
        {
            get
            {
                if (_butBack == null)
                    _butBack = new RelayCommand<MetroWindow>(butBackExecute);

                return _butBack;
            }
        }

        private void butBackExecute(MetroWindow currentWindow)
        {
            if (currentWindow == null) throw new ArgumentNullException("currentWindow");

            MainWindow z = new MainWindow();
            z.Show();
            currentWindow.Close();
        }

        private RelayCommand _butAddNewCategory;
        public RelayCommand ButAddNewCategory
        {
            get
            {
                if (_butAddNewCategory == null)
                    _butAddNewCategory = new RelayCommand(butAddNewCategoryExecute);

                return _butAddNewCategory;
            }
        }

        private void butAddNewCategoryExecute()
        {
            AddNewCategoryWindow win = new AddNewCategoryWindow();
            win.ShowDialog();
        }

        private RelayCommand<int> _butDeleteCategory;
        public RelayCommand<int> ButDeleteCategory
        {
            get
            {
                if (_butDeleteCategory == null)
                {
                    _butDeleteCategory = new RelayCommand<int>(butDeleteCategoryExecute);
                }
                return _butDeleteCategory;
            }
        }

        private void butDeleteCategoryExecute(int id)
        {
            MessageBox.Show("1");
            _connection.RemoveCategory(id);
        }

        #endregion

        #region Events

        private void _connection_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _cats = null;
            RaisePropertyChanged("Categories");
        }

        #endregion
    }
}
