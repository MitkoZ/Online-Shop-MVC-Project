using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoryRepository : BaseRepository<Category>
    {
        public CategoryRepository(OnlineShopEntities context):base(context)
        {
            this.Context = context;
        }

        public override void Save(Category item)
        {
            throw new NotImplementedException();
        }
    }
}
