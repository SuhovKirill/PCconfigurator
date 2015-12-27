using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls;
using PCConfigurator.View.Windows;
using System;

namespace PCConfigurator.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Ctor

        public MainViewModel()
        {

        }

        #endregion

        #region Commands

        private RelayCommand<MetroWindow> _butItems;
        public RelayCommand<MetroWindow> ButItems
        {
            get
            {
                if (_butItems == null)
                    _butItems = new RelayCommand<MetroWindow>(butItemsExecute);

                return _butItems;
            }
        }

        private void butItemsExecute(MetroWindow currentWindow)
        {
            if (currentWindow == null) throw new ArgumentNullException("currentWindow");

            ItemsWindow win = new ItemsWindow();
            win.Show();
            currentWindow.Close();
        }

        private RelayCommand<MetroWindow> _butNewAssembly;
        public RelayCommand<MetroWindow> ButNewAssembly
        {
            get
            {
                if (_butNewAssembly == null)
                    _butNewAssembly = new RelayCommand<MetroWindow>(butNewAssemblyExecute);

                return _butNewAssembly;
            }
        }

        private void butNewAssemblyExecute(MetroWindow currentWindow)
        {
            if (currentWindow == null) throw new ArgumentNullException("currentWindow");
            EditorAssembly win = new EditorAssembly();
            win.Show();
            currentWindow.Close();
        }

        private RelayCommand<MetroWindow> _butCategories;
        public RelayCommand<MetroWindow> ButCategories
        {
            get
            {
                if (_butCategories == null)
                    _butCategories = new RelayCommand<MetroWindow>(butCategoriesExecute);

                return _butCategories;
            }
        }

        private void butCategoriesExecute(MetroWindow currentWindow)
        {
            if (currentWindow == null) throw new ArgumentNullException("currentWindow");
            CategoriesWindow win = new CategoriesWindow();
            win.Show();
            currentWindow.Close();
        }

        private RelayCommand<MetroWindow> _butAssemblies;
        public RelayCommand<MetroWindow> ButAssemblies
        {
            get
            {
                if (_butAssemblies == null)
                {
                    _butAssemblies = new RelayCommand<MetroWindow>((MetroWindow win) =>
                    {
                        if (win == null) throw new ArgumentNullException("win");

                        AssembliesWindow newWin = new AssembliesWindow();
                        newWin.Show();
                        win.Close();
                    });
                }
                return _butAssemblies;
            }
        }
        
        #endregion

        #region Public Properties

        #endregion
    }
}