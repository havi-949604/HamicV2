using System;
using System.Collections.Generic;

namespace Harmic.Models;

public partial class TbOrder
{
    public int OrderId { get; set; }

    public string? Code { get; set; }

    public string? CustomerName { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public int? TotalAmount { get; set; }

    public int? Quanlity { get; set; }

    public int? OrderStatusId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CustomerId { get; set; }

    public virtual TbCustomer? Customer { get; set; }

    public virtual TbOrderstatus? OrderStatus { get; set; }

    public virtual ICollection<TbOrderdetail> TbOrderdetails { get; set; } = new List<TbOrderdetail>();
}
