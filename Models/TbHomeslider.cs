using System;
using System.Collections.Generic;

namespace Harmic.Models;

public partial class TbHomeslider
{
    public int HomeSliderId { get; set; }

    public string? Title { get; set; }

    public string? AboveImg { get; set; }

    public string? BelowImg { get; set; }

    public string? MainImg { get; set; }

    public int? Position { get; set; }

    public string? SmallTxt { get; set; }

    public string? BigTxt { get; set; }

    public string? TextBtn { get; set; }

    public string? UrlBtn { get; set; }

    public bool IsActive { get; set; }
}
