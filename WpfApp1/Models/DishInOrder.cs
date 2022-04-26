using System;
using System.Collections.Generic;

namespace WpfApp1
{
    public partial class DishInOrder
    {
        public DishInOrder()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public int OrderId { get; set; }
        public int DishId { get; set; }
        public int DishCount { get; set; }

        public virtual Dish Dish { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
