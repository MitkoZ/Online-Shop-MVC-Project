using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PCsRepository : BaseRepository<PC>
    {
        public PCsRepository(OnlineShopEntities context) : base(context)
        {
        }

        public override void Save(PC pc)
        {
            if (pc.ID == 0)
            {
                base.Create(pc);
            }
            else
            {
                base.Update(pc, item => item.ID == pc.ID);
            }
        }
    }
}
