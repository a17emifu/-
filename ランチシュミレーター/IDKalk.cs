using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ランチシュミレーター
{
    class IDKalk
    {
        public int ID { get; set; }

        public IDKalk()
        {
            ID = 0;
        }

        public void RegistreraID()
        {
            ID++;
        }

        public void KallaID(int sistaID)
        {
            ID = sistaID;
        }
    }
}
