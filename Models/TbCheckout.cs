using System;
using System.Collections.Generic;

namespace Harmic.Models;

public partial class TbCheckout
{
    public int Id { get; set; }

    public string? OrderId { get; set; }

    public string? OrderInfo { get; set; }

    public string? FullName { get; set; }

    public int? Amount { get; set; }

    public DateTime? DatePaid { get; set; }

    public string? Method { get; set; }
}
