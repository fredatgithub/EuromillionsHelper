﻿using System;

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
          case int _ when number < 10:
            result[0]++;
            break;
          case int _ when number >= 10 && number <= 19:
            result[1]++;
            break;
          case int _ when number >= 20 && number <= 29:
            result[2]++;
            break;
          case int _ when number >= 30 && number <= 39:
            result[3]++;
            break;
          case int _ when number >= 40:
            result[4]++;
            break;
        }
      }

      return result;
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
  }
}
