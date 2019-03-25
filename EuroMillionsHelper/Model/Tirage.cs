using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroMillionsHelper.Model
{
  public class Tirage
  {
    public int Boule1 { get; set; }
    public int Boule2 { get; set; }
    public int Boule3 { get; set; }
    public int Boule4 { get; set; }
    public int Boule5 { get; set; }
    public int Etoile1 { get; set; }
    public int Etoile2 { get; set; }

    public Tirage(int b1, int b2, int b3, int b4, int b5, int e1, int e2)
    {
      Boule1 = b1;
      Boule2 = b2;
      Boule3 = b3;
      Boule4 = b4;
      Boule5 = b5;
      Etoile1 = e1;
      Etoile2 = e2;
    }

    public Tirage()
    {
      Boule1 = 0;
      Boule2 = 0;
      Boule3 = 0;
      Boule4 = 0;
      Boule5 = 0;
      Etoile1 = 0;
      Etoile2 = 0;
    }
  }
}
