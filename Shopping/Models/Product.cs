using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.Models;

[Table("Product")]
public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int ProductCategoryId { get; set; }

    public string Image { get; set; } = null!;

    public int Price { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime ModifiedOn { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public virtual ProductsCategory ProductCategory { get; set; } = null!;
}
