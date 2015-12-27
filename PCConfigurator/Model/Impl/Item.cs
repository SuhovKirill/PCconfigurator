using PCConfigurator.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PCConfigurator.Model.Impl
{
    class Item : IItem
    {
        #region Ctor

        public Item(string name, string description, int catId, int id)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("name");
            if (catId < 0) throw new ArgumentException("catId");
            if (id < 0) throw new ArgumentException("id");

            description = description == null ? string.Empty : description;

            Name = name;
            Description = description;
            CategoryID = catId;
            _id = id;
        }

        #endregion

        #region Public Properties

        int? _catId;
        public int CategoryID
        {
            get { return _catId.Value; }
            set { _catId = value; }
        }

        string _description;
        public string Description
        {
            get { return _description; }
            set{ _description = value; }
        }

        int? _id;
        public int ID
        {
            get
            {
                return _id.Value;
            }
        }

        public BitmapSource Image
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        IList<int> _incompatibleItems = new List<int>();
        public IList<int> IncompatibleItems
        {
            get
            {
                return _incompatibleItems;
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                _name = value;
            }
        }

        #endregion

        #region Public Properties

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
