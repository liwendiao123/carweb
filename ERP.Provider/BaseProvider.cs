using UCMS.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Provider
{
    public class BaseProvider
    {
        private UCMSContext db;
        public UCMSContext DB
        {
            get
            {
                if (db == null)
                {
                    db = new UCMSContext();
                }
                return db;
            }
        }
    }
}
