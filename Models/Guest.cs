using System;
using System.Collections.Generic;

namespace Hotelier_web.Models;

public partial class Guest
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Citizenship { get; set; } = null!;

    public bool IsFirstVisit { get; set; }

    public string? EMail { get; set; }

    public string? Phone { get; set; }

    public DateTime? DateAdded { get; set; }

    public DateTime? DateUpdated { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
