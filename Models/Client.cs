using System;
using System.Collections.Generic;

namespace Diplom2.Models;

public partial class Client
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public DateTime Registrationdate { get; set; }

    public string Status { get; set; } = null!;
}
