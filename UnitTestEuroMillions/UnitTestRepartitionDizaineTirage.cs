using EuroMillionsHelper.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestEuroMillions
{
  [TestClass]
  public class UnitTestRepartitionDizaineTirage
  {
    [TestMethod]
    public void TestMethod_RepartitionDizaineTirage_5_unites()
    {
      Tirage source = new Tirage(1, 2, 3, 4, 5, 0, 0);
      int[] expected = new int[5] { 5, 0, 0, 0, 0};
      int[] result = Tirage.RepartitionDizaineTirage(source);
      Assert.IsTrue(AssertAreEqual(result, expected));
    }

    [TestMethod]
    public void TestMethod_RepartitionDizaineTirage_5_dizaines()
    {
      Tirage source = new Tirage(10, 12, 13, 14, 15, 0, 0);
      int[] expected = new int[5] { 0, 5, 0, 0, 0 };
      int[] result = Tirage.RepartitionDizaineTirage(source);
      Assert.IsTrue(AssertAreEqual(result, expected));
    }

    [TestMethod]
    public void TestMethod_RepartitionDizaineTirage_5_vingtaines()
    {
      Tirage source = new Tirage(21, 22, 23, 24, 25, 0, 0);
      int[] expected = new int[5] { 0, 0, 5, 0, 0 };
      int[] result = Tirage.RepartitionDizaineTirage(source);
      Assert.IsTrue(AssertAreEqual(result, expected));
    }

    [TestMethod]
    public void TestMethod_RepartitionDizaineTirage_5_trentaines()
    {
      Tirage source = new Tirage(31, 32, 33, 34, 35, 0, 0);
      int[] expected = new int[5] { 0, 0, 0, 5, 0 };
      int[] result = Tirage.RepartitionDizaineTirage(source);
      Assert.IsTrue(AssertAreEqual(result, expected));
    }

    [TestMethod]
    public void TestMethod_RepartitionDizaineTirage_5_quarantaines()
    {
      Tirage source = new Tirage(41, 42, 43, 44, 50, 0, 0);
      int[] expected = new int[5] { 0, 0, 0, 0, 5 };
      int[] result = Tirage.RepartitionDizaineTirage(source);
      Assert.IsTrue(AssertAreEqual(result, expected));
    }

    [TestMethod]
    public void TestMethod_RepartitionDizaineTirage_one_of_each()
    {
      Tirage source = new Tirage(1, 10, 20, 34, 45, 0, 0);
      int[] expected = new int[5] { 1, 1, 1, 1, 1 };
      int[] result = Tirage.RepartitionDizaineTirage(source);
      Assert.IsTrue(AssertAreEqual(result, expected));
    }

    public static bool AssertAreEqual(int[] t1, int[] t2)
    {
      bool result = true;
      if (t1.Length != t2.Length)
      {
        return false;
      }

      for (int i = 0; i < t1.Length; i++)
      {
        if (t1[i] != t2[i])
        {
          result = false;
          break;
        }
      }

      return result;
    }
  }
}