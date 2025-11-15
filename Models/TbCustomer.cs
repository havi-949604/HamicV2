using System;
using System.Collections.Generic;

namespace Harmic.Models;

public partial class TbCustomer
{
    public int CustomerId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public DateTime? Birthday { get; set; }

    public string? Avatar { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public int? LocationId { get; set; }

    public DateTime? LastLogin { get; set; }

    public bool IsActive { get; set; }

    public int RoleId { get; set; }

    public virtual TbRole Role { get; set; } = null!;

    public virtual ICollection<TbCart> TbCarts { get; set; } = new List<TbCart>();

    public virtual ICollection<TbOrder> TbOrders { get; set; } = new List<TbOrder>();

    public virtual ICollection<TbProductreview> TbProductreviews { get; set; } = new List<TbProductreview>();

    public virtual ICollection<TbWishlish> TbWishlishes { get; set; } = new List<TbWishlish>();
}
