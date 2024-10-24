using System;
using System.Collections.Generic;

namespace Elevate.PL;

public partial class tblUser
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? ResetCode { get; set; }

    public DateTime? ResetCodeExpiration { get; set; }
}
