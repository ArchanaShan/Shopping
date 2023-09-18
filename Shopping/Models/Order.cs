using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.Models;

[Table("Order")]
public partial class Order
{
    public int OrderId { get; set; }

    public string UserId { get; set; } = null!;

    public int TotalAmount { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime ModifiedOn { get; set; }

    public string ModifiedBy { get; set; } = null!;
}
