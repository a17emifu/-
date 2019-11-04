using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ランチシュミレーター
{
    [Serializable]
    class LunchList
    {
        public List<Lunch> Lunchlist { get; set; }

        public LunchList()
        {
            Lunchlist = new List<Lunch>();
        }
        public void RegistreraList(Lunch lunch)
        {
            Lunchlist.Add(lunch);
        }
    }
}
