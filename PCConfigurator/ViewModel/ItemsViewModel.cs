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

namespace PCConfigurator.ViewModel
{
    class ItemsViewModel : ViewModelBase
    {
        #region Fields

        IConnectionDb _connection;

        #endregion

        #region Ctor

        public ItemsViewModel(IConnectionDb connection)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            _connection = connection;
            _connection.PropertyChanged += _connection_PropertyChanged;
        }


        #endregion

        #region Public Properties

        private ObservableCollection<ItemViewModelEditor> _items;
        public ObservableCollection<ItemViewModelEditor> Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new ObservableCollection<ItemViewModelEditor>();
                    foreach (var z in _connection.Items)
                        _items.Add(new ItemViewModelEditor(z, _connection));
                }
                return _items;
            }
        }

        #endregion

        #region Commands

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

        private RelayCommand _butNewItem;
        public RelayCommand ButNewItem
        {
            get
            {
                if (_butNewItem == null)
                    _butNewItem = new RelayCommand(() =>
                    {
                        EditorItemWindow win = new EditorItemWindow();
                        win.Show();
                    });

                return _butNewItem;
            }
        }

        #endregion

        #region Events

        private void _connection_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _items = null;
            RaisePropertyChanged("Items");
        }

        #endregion

    }
}
