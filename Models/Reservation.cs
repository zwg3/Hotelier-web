using System;
using System.Collections.Generic;

namespace Hotelier_web.Models;

public partial class Reservation
{
    public int Id { get; set; }

    public DateOnly DateArrival { get; set; }

    public DateOnly DateDeparture { get; set; }

    public decimal TotalCost { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string? Commentary { get; set; }

    public bool IsCancelled { get; set; }

    public int GuestCount { get; set; }

    public int CreatedBy { get; set; }

    public int LastUpdateBy { get; set; }

    public DateTime? DateAdded { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User LastUpdateByNavigation { get; set; } = null!;

    public virtual ICollection<Guest> Guests { get; set; } = new List<Guest>();
}
