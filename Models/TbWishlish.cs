using System;
using System.Collections.Generic;

namespace Harmic.Models;

public partial class TbWishlish
{
    public int WishlishId { get; set; }

    public int AccountId { get; set; }

    public int ProductId { get; set; }

    public virtual TbCustomer Account { get; set; } = null!;

    public virtual TbProduct Product { get; set; } = null!;
}
