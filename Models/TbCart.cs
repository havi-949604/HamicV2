using System;
using System.Collections.Generic;

namespace Harmic.Models;

public partial class TbCart
{
    public int IdCart { get; set; }

    public int? IdProduct { get; set; }

    public int? Quantity { get; set; }

    public int? IdCustomer { get; set; }

    public virtual TbCustomer? IdCustomerNavigation { get; set; }

    public virtual TbProduct? IdProductNavigation { get; set; }
}
