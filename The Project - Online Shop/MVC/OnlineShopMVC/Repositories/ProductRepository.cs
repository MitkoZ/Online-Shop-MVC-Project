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
        public override void Save(Product item)
        {
            throw new NotImplementedException();
        }
    }
}
