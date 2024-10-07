using System;
using System.Collections.Generic;

namespace Hotelier_web.Models;

public partial class User
{
    public int Id { get; set; }

    public int? RoleId { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string EMail { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateTime? DateAdded { get; set; }

    public DateTime? LastLogin { get; set; }

    public virtual ICollection<Reservation> ReservationCreatedByNavigations { get; set; } = new List<Reservation>();

    public virtual ICollection<Reservation> ReservationLastUpdateByNavigations { get; set; } = new List<Reservation>();

    public virtual Role? Role { get; set; }
}
