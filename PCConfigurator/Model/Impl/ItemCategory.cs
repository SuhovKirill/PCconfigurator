using PCConfigurator.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCConfigurator.Model.Impl
{
    class ItemCategory : IItemCategory
    {
        #region Ctor

        public ItemCategory(string name, string description, int id)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("name");

            description = description == null ? string.Empty : description;

            Name = name;
            Description = description;
            _id = id;
        }

        #endregion

        #region Public Properties

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
            }
        }

        private int _id;
        public int ID
        {
            get
            {
                return _id;
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        #endregion
    }
}
