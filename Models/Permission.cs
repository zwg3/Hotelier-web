using System;
using System.Collections.Generic;

namespace Hotelier_web.Models;

public partial class Permission
{
    public int Id { get; set; }

    public bool CreateReservations { get; set; }

    public bool ManageReservations { get; set; }

    public bool EditReservations { get; set; }

    public bool DeleteReservations { get; set; }

    public DateTime? DateAdded { get; set; }

    public DateTime? DateUpdated { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
