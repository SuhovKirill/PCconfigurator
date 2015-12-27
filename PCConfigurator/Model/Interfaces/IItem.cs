using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PCConfigurator.Model.Interfaces
{
    interface IItem
    {
        int CategoryID { get; set; }

        int ID { get; }

        string Name { get; set; }

        string Description { get; set; }

        IList<int> IncompatibleItems { get; }

        BitmapSource Image { get; }
    }
}
