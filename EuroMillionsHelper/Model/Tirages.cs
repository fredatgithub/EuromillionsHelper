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

    public int SmallestWeigh()
    {
      int result = 240; // highest weigh
      foreach (Tirage tirage in ListOfTirages)
      {
        if (tirage.PoidsBoules() < result)
        {
          result = tirage.PoidsBoules();
        }
      }

      return result;
    }

    public int HighestWeigh()
    {
      int result = 15; // lowest weigh
      foreach (Tirage tirage in ListOfTirages)
      {
        if (tirage.PoidsBoules() > result)
        {
          result = tirage.PoidsBoules();
        }
      }

      return result;
    }

    public int MediumWeigh()
    {
      int allWeigh = 0;
      foreach (Tirage tirage in ListOfTirages)
      {
        allWeigh += tirage.PoidsBoules();
      }

      return allWeigh / ListOfTirages.Count;
    }

    public int MedianWeigh()
    {
      int medianWeigh = 0;
      List<int> listOfWeighs = new List<int>();
      foreach (Tirage tirage in ListOfTirages)
      {
        listOfWeighs.Add(tirage.PoidsBoules());
      }

      if (listOfWeighs.Count % 2 == 0)
      {
        medianWeigh = listOfWeighs[listOfWeighs.Count / 2];
      }
      else
      {
        medianWeigh = listOfWeighs[listOfWeighs.Count / 2] + listOfWeighs[listOfWeighs.Count / 2] / 2;
      }

      return medianWeigh;
    }
  }
}
