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
        public override void Save(Smartphone item)
        {
            throw new NotImplementedException();
        }
    }
}
