using System;
using System.Collections.Generic;

namespace Shopping.Models;

public partial class ProductsCategory
{
    public int ProductCategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime ModifiedOn { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
