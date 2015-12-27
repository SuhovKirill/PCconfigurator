using PCConfigurator.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCConfigurator.Model.Impl
{
    class PcAssembly : IPcAssembly
    {
        #region Ctor

        public PcAssembly(string name, IList<IItem> items, int id)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("name");
            if (items == null) throw new ArgumentNullException("items");
            if (id < 0) throw new ArgumentOutOfRangeException("id");

            _id = id;
            _items = items;
            _name = name;
        }

        #endregion

        #region Public Properties

        int? _id;
        public int ID
        {
            get
            {
                return _id.Value;
            }
        }

        IList<IItem> _items = new List<IItem>();
        public IList<IItem> Items
        {
            get
            {
                return _items;
            }
            set
            {
                if (_items == null) throw new ArgumentNullException("value");
                _items = value;
            }
        }

        string _name;
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("value");
                _name = value;
            }
        }

        #endregion
    }
}
