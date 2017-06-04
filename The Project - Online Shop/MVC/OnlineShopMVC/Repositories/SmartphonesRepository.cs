using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class SmartphonesRepository : BaseRepository<Smartphone>
    {
        public SmartphonesRepository(OnlineShopEntities context) : base(context)
        {
        }

        public override void Save(Smartphone smartphone)
        {
            if (smartphone.ID == 0)
            {
                base.Create(smartphone);
            }
            else
            {
                base.Update(smartphone, item => item.ID == smartphone.ID);
            }
        }
    }
}
