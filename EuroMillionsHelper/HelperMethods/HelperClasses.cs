using System.Collections.Generic;
using System.Security.Cryptography;

namespace EuroMillionsHelper.HelperMethods
{
  public static class HelperClasses
  {
    public static int GenerateRndNumberUsingCrypto(int min, int max)
    {
      if (max > 255 || min < 0)
      {
        return 0;
      }

      if (max == min)
      {
        return min;
      }

      int result;
      var crypto = new RNGCryptoServiceProvider();
      byte[] randomNumber = new byte[1];
      do
      {
        crypto.GetBytes(randomNumber);
        result = randomNumber[0];
      } while (result < min || result > max);

      return result;
    }

    public static List<int> GenerateSeveralRandomNumbers(int min, int max, int numberOfNumbers)
    {
      var result = new List<int>();
      if (numberOfNumbers < 1)
      {
        return result;
      }

      for (int i = 0; i < numberOfNumbers; i++)
      {
        int tmpNumber = GenerateRndNumberUsingCrypto(min, max);
        if (!result.Contains(tmpNumber))
        {
          result.Add(tmpNumber);
        }
      }

      return result;
    }
  }
}
