using System;
using System.Collections.Generic;

namespace WpfApp1
{
    public partial class Order
    {
        public int Id { get; set; }
        public int UresId { get; set; }
        public decimal DateCreate { get; set; }

        public virtual DishInOrder Ures { get; set; } = null!;
        public virtual User UresNavigation { get; set; } = null!;
    }
}
