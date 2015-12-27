using PCConfigurator.Model.Impl;
using PCConfigurator.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PCConfigurator.Model.Database
{
    class FakeConnection : IConnectionDb
    {
        #region Ctor

        /// <summary>
        /// Конструктор дарован нам чтобы богам молиться и хвалу им вознасить. 
        /// А вообще, чтобы руками сначала все не добавлять бахну сразу немного по дефолту элементов
        /// + так в задании есть.
        /// </summary>
        public FakeConnection()
        {
            AddNewCategory("Процессоры", string.Empty);
            AddNewItem("i3", 1, string.Empty);
            AddNewItem("i5", 1, string.Empty);
            AddNewItem("i7", 1, string.Empty);

            AddNewCategory("ОЗУ", string.Empty);
            AddNewItem("4gb DDR 2", 2, string.Empty);
            AddNewItem("8gb DDR 3", 2, string.Empty);
            AddNewItem("32gb DDR 4", 2, string.Empty);

            AddNewCategory("Материнская плата", string.Empty);
            AddNewItem("Старая плата", 3, string.Empty);
            AddNewItem("Современная", 3, string.Empty);
            AddNewItem("Дорогая", 3, string.Empty);
        }

        #endregion

        #region Public Properties

        IList<IItemCategory> _categories = new List<IItemCategory>();
        public IList<IItemCategory> Categories
        {
            get
            {
                return _categories;
            }
        }

        IList<IItem> _items = new List<IItem>();
        public IList<IItem> Items
        {
            get
            {
                return _items;
            }
        }

        IList<IPcAssembly> _assemblies = new List<IPcAssembly>();
        public IList<IPcAssembly> Assemblies
        {
            get
            {
                return _assemblies;
            }
        }

        #endregion

        #region Public Methods

        public bool AddNewCategory(string nameCategory, string description)
        {
            if (nameCategory == null) throw new ArgumentNullException("nameCategory");
            if (string.IsNullOrWhiteSpace(nameCategory)) throw new ArgumentException("nameCategory");

            description = description == null ? string.Empty : description;

            var sameCat = _categories.FirstOrDefault(cat => cat.Name == nameCategory);
            if (sameCat != null) return false;

            int newId = 1;
            for(;;++newId)
                if (_categories.FirstOrDefault(cat => cat.ID == newId) == null) break;

            Categories.Add(new ItemCategory(nameCategory, description, newId));

            RaisePropertyChanged("Categories");

            return true;
        }

        public bool RemoveCategory(int id)
        {
            var catFind = _categories.FirstOrDefault(cat => cat.ID == id);
            if (catFind == null) return false;

            _categories.Remove(catFind);


            while (Items.FirstOrDefault(item => item.CategoryID == id) != null)
                RemoveItem(Items.FirstOrDefault(item => item.CategoryID == id).ID);

            RaisePropertyChanged("Categories");
            return true;
        }

        public bool UpdateCategory(IItemCategory itemCategory)
        {
            throw new NotImplementedException();
        }

        public bool AddNewItem(string name, int idCategory, string description)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("name");

            if (Categories.FirstOrDefault(cat => cat.ID == idCategory) == null)
                throw new ArgumentException("No category with such id");

            int newId = 1;
            for (; ; ++newId)
                if (_items.FirstOrDefault(item => item.ID == newId) == null) break;

            _items.Add(new Item(name, description, idCategory, newId));

            RaisePropertyChanged("Items");

            return true;
        }

        public bool UpdateItem(IItem item)
        {
            throw new NotImplementedException();
        }

        public bool RemoveItem(int id)
        {
            var z = Items.FirstOrDefault(item => item.ID == id);
            if (z == null) return false;

            Items.Remove(z);
            RaisePropertyChanged("Items");
            return true;
        }
       
        public IPcAssembly AddNewAssembly(string name, IList<IItem> items)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (items == null) throw new ArgumentNullException("items");

            int newId = 1;
            while (Assemblies.FirstOrDefault(ass => ass.ID == newId) != null)
                ++newId;

            IPcAssembly pc = new PcAssembly(name, items, newId);
            Assemblies.Add(pc);
            return pc;
        }

        public bool RemoveAssembly(int id)
        {
            if (Assemblies.FirstOrDefault(ass => ass.ID == id) == null) return false;

            Assemblies.Remove(Assemblies.FirstOrDefault(ass => ass.ID == id));

            return true;
        }

        public bool UpdateAssembly(IPcAssembly assembly)
        {
            return true;
        }
        
        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (propertyName == null) throw new ArgumentNullException("propertyName");
            if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentException("propertyName");

            PropertyChangedEventHandler copyHandler = PropertyChanged;
            if (copyHandler != null)
                copyHandler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
