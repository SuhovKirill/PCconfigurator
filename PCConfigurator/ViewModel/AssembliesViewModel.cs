using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls;
using PCConfigurator.Model.Database;
using PCConfigurator.Model.Interfaces;
using PCConfigurator.View.Windows;
using PCConfigurator.ViewModel.DataBridge;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PCConfigurator.ViewModel
{
    class AssembliesViewModel : ViewModelBase
    {
        #region Fields

        private IConnectionDb _connection;
        private IDataBridge _bridge;

        #endregion

        #region Ctor

        public AssembliesViewModel(IConnectionDb connection, IDataBridge bridge)
        {
            if (connection == null) throw new ArgumentNullException("conection");
            if (bridge == null) throw new ArgumentNullException("bridge");

            _connection = connection;
            _bridge = bridge;
        }

        #endregion

        #region Public Properties

        private ObservableCollection<IPcAssembly> _assemblies;
        public ObservableCollection<IPcAssembly> Assemblies
        {
            get
            {
                if (_assemblies == null)
                {
                    _assemblies = new ObservableCollection<IPcAssembly>();
                    foreach (var z in _connection.Assemblies)
                        _assemblies.Add(z);
                }
                return _assemblies;
            }
        }

        private IPcAssembly _currentAssembly;
        public IPcAssembly CurrentAssembly
        {
            get
            {
                return _currentAssembly;
            }
            set
            {
                _currentAssembly = value;
            }
        }

        #endregion

        #region Commands

        private RelayCommand<MetroWindow> _butEdit;
        public RelayCommand<MetroWindow> ButEdit
        {
            get
            {
                if (_butEdit == null)
                {
                    _butEdit = new RelayCommand<MetroWindow>((MetroWindow curWindow) =>
                   {
                       if (CurrentAssembly == null)
                       {
                           MessageBox.Show("Сборка не выбрана.");
                           return;
                       }
                       _bridge.currentAssembly = CurrentAssembly;
                       EditorAssembly newWin = new EditorAssembly();
                       newWin.Show();
                       curWindow.Close();
                   });
                }
                return _butEdit;
            }
        }

        private RelayCommand<MetroWindow> _butBack;
        public RelayCommand<MetroWindow> ButBack
        {
            get
            {
                if (_butBack == null)
                {
                    _butBack = new RelayCommand<MetroWindow>((MetroWindow currentWindow) =>
                    {
                        if (currentWindow == null) throw new ArgumentNullException("currentWindow");

                        MainWindow win = new MainWindow();
                        win.Show();
                        currentWindow.Close();
                    });
                }
                return _butBack;
            }
        }

        private RelayCommand _butDelete;
        public RelayCommand ButDelete
        {
            get
            {
                if (_butDelete == null)
                {
                    _butDelete = new RelayCommand(() =>
                    {
                        if (CurrentAssembly == null)
                        {
                            MessageBox.Show("Не выбран элемент");
                            return;
                        }

                        _connection.RemoveAssembly(CurrentAssembly.ID);
                        Assemblies.Remove(CurrentAssembly);
                    });
                }
                return _butDelete;
            }
        }

        #endregion

    }
}
