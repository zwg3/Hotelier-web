using System;
using System.Collections.Generic;

namespace Hotelier_web.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public DateTime? DateAdded { get; set; }

    public DateTime? DateUpdated { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
