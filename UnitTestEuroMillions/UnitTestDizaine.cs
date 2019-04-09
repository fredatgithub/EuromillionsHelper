using Microsoft.VisualStudio.TestTools.UnitTesting;
using EuroMillionsHelper.Model;

namespace UnitTestEuroMillions
{
  [TestClass]
  public class UnitTestDizaine
  {
    [TestMethod]
    public void TestMethod_Dizaine_1()
    {
      int source = 1;
      int expected = 0;
      int result = Tirage.Dizaine(source);
      Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void TestMethod_Dizaine_2()
    {
      int source = 2;
      int expected = 0;
      int result = Tirage.Dizaine(source);
      Assert.AreEqual(expected, result);
    }
  }
}
