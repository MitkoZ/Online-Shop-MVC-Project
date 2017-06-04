using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductRepository : BaseRepository<Product>
    {
        public ProductRepository(OnlineShopEntities context) : base(context)
        {
        }

        public override void Save(Product product)
        {
            if (product.ID == 0)
            {
                base.Create(product);
            }
            else
            {
                base.Update(product, item => item.ID == product.ID);
            }
        }
    }
}
