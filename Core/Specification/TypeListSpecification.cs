using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specification;

public class TypeListSpecification : Specification<Product, string>
{
    public TypeListSpecification()
    {
        AddSelect(x => x.Type);
        ApplyDistinct();
    }
}
