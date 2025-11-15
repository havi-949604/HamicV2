using System;
using System.Collections.Generic;

namespace Harmic.Models;

public partial class TbProductreview
{
    public int ProductReviewId { get; set; }

    public int CustomerId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? Detail { get; set; }

    public int? Star { get; set; }

    public int? ProductId { get; set; }

    public bool IsActive { get; set; }

    public virtual TbCustomer Customer { get; set; } = null!;

    public virtual TbProduct? Product { get; set; }
}
