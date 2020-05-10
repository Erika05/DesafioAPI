using NUnit.Framework;
using DesafioAPI.Helpers;
using DesafioAPI.DBSteps;

namespace DesafioAPI.Bases
{
    public class TestBase
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            ProjetoDBSteps.DeletaProjetos();
            TarefaDBSteps.DeleteTodasTarefas();
            ExtentReportHelpers.CreateReport();
        }

        [SetUp]
        public void SetUp()
        {
            ExtentReportHelpers.AddTest();
        }

        [TearDown]
        public void TearDown()
        {
            ExtentReportHelpers.AddTestResult();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            ExtentReportHelpers.GenerateReport();
        }
    }
}
