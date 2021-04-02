using System.Collections.Generic;

namespace EuroMillionsHelper.Model
{
  public class Tirages
  {
    public List<Tirage> ListOfTirages { get; set; }

    public Tirages()
    {
      ListOfTirages = new List<Tirage>();
    }

    public Tirages(Tirage tirage)
    {
      ListOfTirages = new List<Tirage>
      {
        tirage
      };
    }

    public void Add(Tirage tirage)
    {
      ListOfTirages.Add(tirage);
    }

    public int PoidsBoules(Tirage tirage)
    {
      return tirage.Boule1 +
        tirage.Boule2 +
        tirage.Boule3 +
        tirage.Boule4 +
        tirage.Boule5;
    }

    public int PoidsEtoiles(Tirage tirage)
    {
      return tirage.Etoile1 + tirage.Etoile2;
    }
  }
}
