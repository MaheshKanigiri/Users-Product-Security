using System;
using System.Collections.Generic;

namespace Users_Product_Security.Models;

public partial class User
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public int Pid { get; set; }

    public Product PidNavigation { get; set; }
}
