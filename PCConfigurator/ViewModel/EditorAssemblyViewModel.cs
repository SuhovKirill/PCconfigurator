using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls;
using PCConfigurator.Model.Database;
using PCConfigurator.Model.Interfaces;
using PCConfigurator.View.Windows;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PCConfigurator.Model.Impl;
using MahApps.Metro.Controls.Dialogs;
using PCConfigurator.ViewModel.DataBridge;
using Microsoft.Win32;
using System.IO;

namespace PCConfigurator.ViewModel
{
    class EditorAssemblyViewModel : ViewModelBase
    {
        #region Fields

        IConnectionDb _connection;
        IPcAssembly _curAssembly;
        IList<CellListener> _cells = new List<CellListener>();

        #endregion

        #region Ctor

        public EditorAssemblyViewModel(IConnectionDb connection, IDataBridge bridge)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (bridge == null) throw new ArgumentNullException("bridge");

            if (bridge.currentAssembly != null)
            {
                _curAssembly = bridge.currentAssembly;
                Name = _curAssembly.Name;
                bridge.currentAssembly = null;
            }

            _connection = connection;
        }

        #endregion

        #region Commands

        private RelayCommand _butSaveToFile;
        public RelayCommand ButSaveToFile
        {
            get
            {
                if (_butSaveToFile == null)
                {
                    _butSaveToFile = new RelayCommand(() => {
                        SaveFileDialog dialog = new SaveFileDialog();
                        dialog.Filter = "txt Files (*.txt)|*.txt";

                        if (dialog.ShowDialog() == true)
                        {
                            string pathFile = dialog.FileName;

                            StringBuilder b = new StringBuilder();
                            b.AppendLine(Name);
                            foreach (var z in _cells)
                            {
                                if (z.Item != null)
                                    b.AppendLine(z.Item.Name);
                            }

                            File.WriteAllText(pathFile, b.ToString());
                        }
                    });
                }
                return _butSaveToFile;
            }
        }

        private RelayCommand<EditorAssembly> _cmdInitializated;
        public RelayCommand<EditorAssembly> CmdInitializated
        {
            get
            {
                if (_cmdInitializated == null)
                {
                    _cmdInitializated = new RelayCommand<EditorAssembly>(cmdInitializatedExecute);
                }
                return _cmdInitializated;
            }
        }

        private void cmdInitializatedExecute(EditorAssembly win)
        {
            if (win == null) throw new ArgumentNullException("win");

            initItemsChoise(win);
            initListeners(win);
            fillExistItems(win);
        }

        private RelayCommand<MetroWindow> _butBack;
        public RelayCommand<MetroWindow> ButBack
        {
            get
            {
                if (_butBack == null)
                    _butBack = new RelayCommand<MetroWindow>((MetroWindow currentWindow) =>
                    {
                        if (currentWindow == null) throw new ArgumentNullException("currentWindow");

                        MainWindow win = new MainWindow();
                        win.Show();
                        currentWindow.Close();
                    });

                return _butBack;
            }
        }

        private RelayCommand _butSave;
        public RelayCommand ButSave
        {
            get
            {
                if (_butSave == null)
                {
                    _butSave = new RelayCommand(butSaveExecute);
                }
                return _butSave;
            }   
        }

        private void butSaveExecute()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                MessageBox.Show("Не надо так с пустым именем сохранять");
                return;
            }

            if (_curAssembly == null)
            {
                IList<IItem> items = new List<IItem>();
                foreach (var z in _cells)
                {
                    if (z.Item != null)
                        items.Add(z.Item);
                }

                _curAssembly = _connection.AddNewAssembly(Name, items);
            }
            else
            {
                _curAssembly.Items.Clear();
                foreach (var z in _cells)
                {
                    if (z.Item != null)
                        _curAssembly.Items.Add(z.Item);
                }

                _connection.UpdateAssembly(_curAssembly);
            }
        }

        #endregion

        #region InitMethods

        private void fillExistItems(EditorAssembly win)
        {
            if (win == null) throw new ArgumentNullException("win");

            if (_curAssembly == null) return;

            foreach(var childObj in win.CellItems.Children)
            {
                Border bigBd = childObj as Border;
                if (bigBd == null) continue;

                CellListener cell = bigBd.DataContext as CellListener;
                if (cell == null) continue;

                foreach (var item in _curAssembly.Items)
                    if (item.CategoryID == cell.IdCategory)
                    {
                        cell.Item = item;

                        StackPanel panel = bigBd.Child as StackPanel;
                        if (panel == null) return;

                        Label nameCurItem = panel.FindChild<Label>("Item");
                        if (nameCurItem == null) return;

                        nameCurItem.Content = cell.Item.Name;
                    }
            }
        }

        private void initItemsChoise(EditorAssembly win)
        {
            if (win == null) throw new ArgumentNullException("win");

            foreach (var cat in _connection.Categories)
            {
                Expander exp = new Expander();
                exp.Header = cat.Name;
                win.ChoiceItems.Children.Add(exp);

                StackPanel panel = new StackPanel();
                exp.Content = panel;

                foreach (var item in _connection.Items)
                {
                    if (item.CategoryID != cat.ID) continue;

                    Label newLabel = new Label();
                    newLabel.Content = item.Name;
                    newLabel.DataContext = item;
                    newLabel.MouseDown += new MouseButtonEventHandler(delegate (object o, MouseButtonEventArgs args)
                    {
                        DragDrop.DoDragDrop(newLabel, newLabel.DataContext, DragDropEffects.Move);
                    });

                    panel.Children.Add(newLabel);
                }

                RaisePropertyChanged("ChoiceItems");
            }
        }

        private void initListeners(EditorAssembly win)
        {
            if (win == null) throw new ArgumentNullException("win");

            foreach (var cat in _connection.Categories)
            {
                Border bigBd = new Border();
                bigBd.BorderThickness = new Thickness(1);
                bigBd.Background = Brushes.SteelBlue;

                bigBd.Width = 220;
                bigBd.Height = 60;

                bigBd.HorizontalAlignment = HorizontalAlignment.Stretch;

                bigBd.AllowDrop = true;

                bigBd.Drop += BigBd_Drop;
                bigBd.MouseDown += BigBd_MouseDown;

                bigBd.DataContext = new CellListener(cat);
                _cells.Add(bigBd.DataContext as CellListener);

                StackPanel panel = new StackPanel();
                panel.Orientation = Orientation.Vertical;

                Label catNameLabel = new Label();
                catNameLabel.Content = cat.Name;
                panel.Children.Add(catNameLabel);

                Label nameCurItem = new Label();
                nameCurItem.Name = "Item";
                nameCurItem.Content = "Не выбран";
                panel.Children.Add(nameCurItem);

                bigBd.Child = panel;

                win.CellItems.Children.Add(bigBd);
            }
        }

        private void BigBd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender == null) throw new ArgumentNullException("sender");
            if (e == null) throw new ArgumentNullException("e");

            if (e.RightButton == MouseButtonState.Pressed)
            {
                Border bd = sender as Border;
                if (bd == null) return;

                CellListener cell = bd.DataContext as CellListener;
                if (cell == null) return;

                StackPanel panel = bd.Child as StackPanel;
                if (panel == null) return;

                Label nameCurItem = panel.FindChild<Label>("Item");
                if (nameCurItem == null) return;

                nameCurItem.Content = "Ничего не выбрано";
                cell.Item = null;
            }
        }

        private void BigBd_Drop(object sender, DragEventArgs e)
        {
            if (sender == null) throw new ArgumentNullException("sender");
            if (e == null) throw new ArgumentNullException("e");

            Border bd = sender as Border;
            if (bd == null) return;

            CellListener cell = bd.DataContext as CellListener;
            if (cell == null) return;

            StackPanel panel = bd.Child as StackPanel;
            if (panel == null) return;

            Label nameCurItem = panel.FindChild<Label>("Item");
            if (nameCurItem == null) return;

            Item item = e.Data.GetData(typeof(Item)) as Item;
            if (item == null) return;

            if (item.CategoryID != cell.IdCategory)
            {
                MessageBox.Show("Слот для другой категории");
                return;
            }

            nameCurItem.Content = item.Name;
            cell.Item = item;
        }

        #endregion

        #region Public Properties

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        #endregion
    }
}
