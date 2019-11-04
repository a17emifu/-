using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ランチシュミレーター
{
    [Serializable]
    class Lunch
    {
        public string MatNamn { get; set; }
        public List<string> Ingredienser { get; set; }
        public int MatID { get; set; }

        public Lunch()
        {
            Ingredienser = new List<string>();
        }

        public void RegistreraLunch(string matnamn, List<string>ingredienser, int id)
        {
            MatNamn = matnamn;
            Ingredienser = ingredienser;
            MatID = id;
        }

    }
}
