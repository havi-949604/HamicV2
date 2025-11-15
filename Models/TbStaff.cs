using System;
using System.Collections.Generic;

namespace Harmic.Models;

public partial class TbStaff
{
    public int Idstaff { get; set; }

    public string Name { get; set; } = null!;

    public int Idcard { get; set; }

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly? Birth { get; set; }
}
