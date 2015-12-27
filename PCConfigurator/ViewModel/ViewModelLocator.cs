using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using PCConfigurator.Model.Database;
using PCConfigurator.ViewModel.DataBridge;

namespace PCConfigurator.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    internal class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<CategoriesViewModel>();
            SimpleIoc.Default.Register<AddNewCategoryViewModel>();
            SimpleIoc.Default.Register<ItemsViewModel>();
            SimpleIoc.Default.Register<EditorItemViewModel>();
            SimpleIoc.Default.Register<EditorAssemblyViewModel>();
            SimpleIoc.Default.Register<AssembliesViewModel>();

            SimpleIoc.Default.Register<IConnectionDb, FakeConnection>();
            SimpleIoc.Default.Register<IDataBridge, DefaultDataBridge>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public CategoriesViewModel Categories
        {
            get
            {
                return SimpleIoc.Default.GetInstanceWithoutCaching<CategoriesViewModel>();
            }
        }

        public AddNewCategoryViewModel AddNewCategory
        {
            get
            {
                return SimpleIoc.Default.GetInstanceWithoutCaching<AddNewCategoryViewModel>();
            }
        }

        public ItemsViewModel Items
        {
            get
            {
                return SimpleIoc.Default.GetInstanceWithoutCaching<ItemsViewModel>();
            }
        }

        public EditorItemViewModel EditorItem
        {
            get
            {
                return SimpleIoc.Default.GetInstanceWithoutCaching<EditorItemViewModel>();
            }
        }

        public EditorAssemblyViewModel EditorAssembly
        {
            get
            {
                return SimpleIoc.Default.GetInstanceWithoutCaching<EditorAssemblyViewModel>();
            }
        }

        public AssembliesViewModel Assemblies
        {
            get
            {
                return SimpleIoc.Default.GetInstanceWithoutCaching<AssembliesViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}