using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class SalesRepository : BaseRepository<Sale>
    {
        public override void Save(Sale item)
        {
            Context.Sales.Add(item);
            Context.SaveChanges();
        }
    }
}
