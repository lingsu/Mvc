using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

using Component.Data;
using Core.Models;
using Core.Data.Repositories;

namespace Core.Data.Repositories.Impl
{
    [Export(typeof(IProductRepository))]
    public class ProductRepository:EFRepositoryBase<Product>,IProductRepository
    {
    }
}
