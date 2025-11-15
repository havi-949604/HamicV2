using System;
using System.Collections.Generic;

namespace Harmic.Models;

public partial class TbRolePermission
{
    public int RolePermissionId { get; set; }

    public int? RoleId { get; set; }

    public int? PermissionId { get; set; }

    public virtual TbPermission? Permission { get; set; }

    public virtual TbRole? Role { get; set; }
}

