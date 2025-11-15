using System;
using System.Collections.Generic;

namespace Harmic.Models;

public partial class TbAdminmenu
{
    public int MenuId { get; set; }

    public string? Title { get; set; }

    public string? Alias { get; set; }

    public string? Icon { get; set; }

    public int? ParentId { get; set; }

    public int? Positon { get; set; }

    public string? Description { get; set; }

    public bool IsActive { get; set; }
}
