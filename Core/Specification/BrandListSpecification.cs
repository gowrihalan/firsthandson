using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specification;

public class BrandListSpecification : Specification<Product, string>
{
    public BrandListSpecification()
    {
        AddSelect(x => x.Brand);
        ApplyDistinct();

    }
}
