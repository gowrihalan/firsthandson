using System;
using Core.Entities;

namespace Core.Specification;

public class ProductSpecification : Specification<Product>
{
    public ProductSpecification(ProductSpecPram specPram) : base(x =>
    (string.IsNullOrEmpty(specPram.Search) || x.Name.ToLower().Contains(specPram.Search)) &&
    (!specPram.Brands.Any() || specPram.Brands.Contains(x.Brand)) &&
    (!specPram.Types.Any() || specPram.Types.Contains(x.Type))
    )
    {

        ApplyPaging(specPram.PageSize * (specPram.PageIndex - 1), specPram.PageSize);
        switch (specPram.Sort)
        {
            case "priceAsc":
                AddOrderBy(x => x.Price);
                break;
            case "priceDesc":
                AddOrderByDescending(x => x.Price);
                break;
            default:
                AddOrderBy(x => x.Name);
                break;
        }
    }

}
