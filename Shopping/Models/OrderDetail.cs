using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.Models;

[Table("OrderDetail")]
public partial class OrderDetail
{
    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime ModifiedOn { get; set; }

    public string ModifiedBy { get; set; } = null!;
}
