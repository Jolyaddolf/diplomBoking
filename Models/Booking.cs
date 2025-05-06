using System;
using System.Collections.Generic;

namespace Diplom2.Models;

public partial class Booking
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public int RoomId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;

    public virtual Room Room { get; set; } = null!;
}
