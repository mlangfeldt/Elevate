using System;
using System.Collections.Generic;

namespace Elevate.PL;

public partial class tblOrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int CourseId { get; set; }

    public int Quantity { get; set; }

    public double Cost { get; set; }
}
