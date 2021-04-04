using System;
using System.Collections.Generic;

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
    public DateTime DateTirage { get; set; }

    public Tirage(int boule1, int boule2, int boule3, int boule4, int boule5, int etoile1, int etoile2, DateTime dateTirage)
    {
      Boule1 = boule1;
      Boule2 = boule2;
      Boule3 = boule3;
      Boule4 = boule4;
      Boule5 = boule5;
      Etoile1 = etoile1;
      Etoile2 = etoile2;
      DateTirage = dateTirage;
    }

    public Tirage(int boule1, int boule2, int boule3, int boule4, int boule5, int etoile1, int etoile2)
    {
      Boule1 = boule1;
      Boule2 = boule2;
      Boule3 = boule3;
      Boule4 = boule4;
      Boule5 = boule5;
      Etoile1 = etoile1;
      Etoile2 = etoile2;
      DateTirage = new DateTime(1, 1, 1);
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
      DateTirage = new DateTime(1, 1, 1);
    }

    public int PoidsBoulesTirage(Tirage tirage)
    {
      return tirage.Boule1 + tirage.Boule2 + tirage.Boule3 + tirage.Boule4 + tirage.Boule5;
    }

    public int PoidsEtoilesTirage(Tirage tirage)
    {
      return tirage.Etoile1 + tirage.Etoile2;
    }

    public static int[] RepartitionDizaineTirage(Tirage tirage)
    {
      int[] result = new int[5] { 0, 0, 0, 0, 0 };
      foreach (var number in new int[] { tirage.Boule1, tirage.Boule2, tirage.Boule3, tirage.Boule4, tirage.Boule5 })
      {
        switch (number)
        {
          case int _ when number <= 10:
            result[0]++;
            break;
          case int _ when number > 10 && number <= 20:
            result[1]++;
            break;
          case int _ when number > 20 && number <= 30:
            result[2]++;
            break;
          case int _ when number > 30 && number <= 40:
            result[3]++;
            break;
          case int _ when number > 40:
            result[4]++;
            break;
        }
      }

      return result;
    }

    public static string NombreParLigne(int[] tableau)
    {
      string result = string.Empty;
      for (int i = 0; i < tableau.Length; i++)
      {
        result += tableau[i] + " + ";
      }

      return result.TrimEnd().TrimEnd('+').TrimEnd();
    }

    public static int[] RepartitionParColonne(Tirage tirage)
    {
      int[] result = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
      foreach (var number in new int[] { tirage.Boule1, tirage.Boule2, tirage.Boule3, tirage.Boule4, tirage.Boule5 })
      {
        switch (number)
        {
          case int _ when number.ToString().EndsWith("1"):
            result[0]++;
            break;
          case int _ when number.ToString().EndsWith("2"):
            result[1]++;
            break;
          case int _ when number.ToString().EndsWith("3"):
            result[2]++;
            break;
          case int _ when number.ToString().EndsWith("4"):
            result[3]++;
            break;
          case int _ when number.ToString().EndsWith("5"):
            result[4]++;
            break;
          case int _ when number.ToString().EndsWith("6"):
            result[5]++;
            break;
          case int _ when number.ToString().EndsWith("7"):
            result[6]++;
            break;
          case int _ when number.ToString().EndsWith("8"):
            result[7]++;
            break;
          case int _ when number.ToString().EndsWith("9"):
            result[8]++;
            break;
          case int _ when number.ToString().EndsWith("0"):
            result[9]++;
            break;
        }
      }

      return result;
    }

    public static string NombreParColonne(int[] tableau)
    {
      string result = string.Empty;
      for (int i = 0; i < tableau.Length; i++)
      {
        result += tableau[i] + " + ";
      }

      return result.TrimEnd().TrimEnd('+').TrimEnd();
    }

    public static int Dizaine(int nombre)
    {
      int result = 0;
      result = Math.Abs(nombre / 10);
      if (nombre == 50)
      {
        result = 4;
      }

      return result;
    }

    public int NombreDeBoulesQuiSeSuivent()
    {
      int result = 0;
      if (Boule1 == Boule2 + 1)
      {
        result++;
      }

      if (Boule2 == Boule3 + 1)
      {
        result++;
      }

      if (Boule3 == Boule4 + 1)
      {
        result++;
      }

      if (Boule4 == Boule5 + 1)
      {
        result++;
      }

      return result;
    }

    public int NombreDEtoilesQuiSeSuivent()
    {
      return Etoile1 == Etoile2 + 1 ? 1 : 0;
    }

    public int PoidsDesEtoiles()
    {
      return Etoile1 + Etoile2;
    }

    public int PoidsDesBoules()
    {
      return Boule1 + Boule2 + Boule3 + Boule4 + Boule5;
    }

    public void AssigneBoules(List<int> listDeNumeros)
    {
      Boule1 = listDeNumeros[0];
      Boule2 = listDeNumeros[1];
      Boule3 = listDeNumeros[2];
      Boule4 = listDeNumeros[3];
      Boule5 = listDeNumeros[4];
    }

    public void AssigneEtoiles(List<int> listDeNumeros)
    {
      Etoile1 = listDeNumeros[0];
      Etoile2 = listDeNumeros[1];
    }

    public override string ToString()
    {
      int[] boules = new int[] { Boule1, Boule2, Boule3, Boule4, Boule5 };
      Array.Sort(boules);

      return $"{boules[0]}-{boules[1]}-{boules[2]}-{boules[3]}-{boules[4]}";
    }

    internal string FirstFourToString()
    {
      int[] boules = new int[] { Boule1, Boule2, Boule3, Boule4, Boule5 };
      Array.Sort(boules);

      return $"{boules[0]}-{boules[1]}-{boules[2]}-{boules[3]}";
    }

    internal string LastFourToString()
    {
      int[] boules = new int[] { Boule1, Boule2, Boule3, Boule4, Boule5 };
      Array.Sort(boules);

      return $"{boules[1]}-{boules[2]}-{boules[3]}-{boules[4]}";
    }

    public string[] NumberOfBallsFound(Tirage tirageRecherche)
    {
      string[] result = new string[6];
      int nombreDeBoulesTrouves = 0;
      int curseur = 1;
      if (tirageRecherche.Boule1 == Boule1)
      {
        nombreDeBoulesTrouves++;
        result[curseur] = Boule1.ToString();
        curseur++;
      }
      else if (tirageRecherche.Boule1 == Boule2)
      {
        nombreDeBoulesTrouves++;
        result[curseur] = Boule2.ToString();
        curseur++;
      }
      else if (tirageRecherche.Boule1 == Boule3)
      {
        nombreDeBoulesTrouves++;
        result[curseur] = Boule3.ToString();
        curseur++;
      }
      else if (tirageRecherche.Boule1 == Boule4)
      {
        nombreDeBoulesTrouves++;
        result[curseur] = Boule4.ToString();
        curseur++;
      }


      result[0] = nombreDeBoulesTrouves.ToString();
      return result;
    }

    public int PoidsBoules()
    {
      return Boule1 + Boule2 + Boule3 + Boule4 + Boule5;
    }

    public int PoidsEtoiles()
    {
      return Etoile1 + Etoile2;
    }
  }
}
