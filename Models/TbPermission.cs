using System;
using System.Collections.Generic;

namespace Harmic.Models;

public partial class TbPermission
{
    public int PermissionId { get; set; }

    public string? PermissionName { get; set; }

    public string? Description { get; set; }

    public string? ControllerName { get; set; }

    public string? ActionName { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<TbRolePermission> TbRolePermissions { get; set; } = new List<TbRolePermission>();
}

