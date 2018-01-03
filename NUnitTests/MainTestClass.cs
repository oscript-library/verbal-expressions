using NUnit.Framework;
using OnescriptVerbalExpressions;

// Используется NUnit 3.6

namespace NUnitTests
{
	[TestFixture]
	public class MainTestClass
	{

		private EngineHelpWrapper host;

		[OneTimeSetUp]
		public void Initialize()
		{
			host = new EngineHelpWrapper();
			host.StartEngine();
		}

		[Test]
		public void TestAsInternalObjects()
		{
			var verbalExpression = new VerbalExpression();

			verbalExpression.Word();

			Assert.AreEqual(verbalExpression.ToString(), "\\w+");
		}


		[Test]
		public void TestAsExternalObjects()
		{
			host.RunTestScript("NUnitTests.Tests.external.os");
		}
	}
}
