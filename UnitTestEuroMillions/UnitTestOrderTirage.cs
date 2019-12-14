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

    public static bool AssertTirageAreEqual(Tirage tirage1, Tirage tirage2)
    {
      return tirage1.Boule1 == tirage2.Boule1 &&
             tirage1.Boule2 == tirage2.Boule2 &&
             tirage1.Boule3 == tirage2.Boule3 &&
             tirage1.Boule4 == tirage2.Boule4 &&
             tirage1.Boule5 == tirage2.Boule5 &&
             tirage1.Etoile1 == tirage2.Etoile1 &&
             tirage1.Etoile2 == tirage2.Etoile2;
    }
  }
}
