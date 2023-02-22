using System;
using System.Collections.Generic;

namespace Users_Product_Security.Models;

public partial class Product
{
    public int Pid { get; set; }

    public string Pname { get; set; } = null!;

    public virtual ICollection<User> Users { get; } = new List<User>();
}
