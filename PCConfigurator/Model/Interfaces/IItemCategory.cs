using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCConfigurator.Model.Interfaces
{
    interface IItemCategory
    {
        /// <summary>
        /// Айдишник. Неизменяемый.
        /// </summary>
        int ID { get; }

        /// <summary>
        /// Наименование категории.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Описание категории
        /// </summary>
        string Description { get; set; }
    }
}
