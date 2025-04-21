using System;
using System.Collections.Generic;

namespace Diplom2.Models;

public partial class Room
{
    public int Id { get; set; }

    public string Roomnumber { get; set; } = null!;

    public string Status { get; set; } = null!;
}
