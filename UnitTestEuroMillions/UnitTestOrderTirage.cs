using Microsoft.VisualStudio.TestTools.UnitTesting;
using EuroMillionsHelper;
using EuroMillionsHelper.Model;

namespace UnitTestEuroMillions
{
  [TestClass]
  public class UnitTestOrderTirage
  {
    [TestMethod]
    public void TestMethod_OrderTirage()
    {
      Tirage source = new Tirage(5, 4, 3, 2, 1, 1, 1);
      Tirage expected = new Tirage(1, 2, 3, 4, 5, 1, 1);
      Tirage result = FormMain.OrderTirage(source);
      Assert.IsTrue(AssertTirageAreEqual(result, expected));
    }

    public static bool AssertTirageAreEqual(Tirage t1Tirage, Tirage t2Tirage)
    {
      return t1Tirage.Boule1 == t2Tirage.Boule1 &&
             t1Tirage.Boule2 == t2Tirage.Boule2 &&
             t1Tirage.Boule3 == t2Tirage.Boule3 &&
             t1Tirage.Boule4 == t2Tirage.Boule4 &&
             t1Tirage.Boule5 == t2Tirage.Boule5 &&
             t1Tirage.Etoile1 == t2Tirage.Etoile1 &&
             t1Tirage.Etoile2 == t2Tirage.Etoile2;
    }
  }
}
