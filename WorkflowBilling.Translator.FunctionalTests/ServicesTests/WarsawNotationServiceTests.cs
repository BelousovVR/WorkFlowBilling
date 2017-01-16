using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFlowBilling.Compiler.Services;
using WorkFlowBilling.TestFramework.Common;
using WorkFlowBilling.Compiler.Impl.Services;
using WorkFlowBilling.Translator.Impl;
using WorkFlowBilling.Compiler.Exceptions;
using WorkflowBilling.Translator.FunctionalTests.Common;

namespace WorkflowBilling.Translator.FunctionalTests.ServicesTests
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    public class WarsawNotationServiceTests : BaseFunctionalTest
    {

        /// <summary>
        /// 
        /// </summary>
        private static List<StringConvertTestData> PostfixConvertationData = new List<StringConvertTestData>()
        {
            // Тесты на простые арифметические операторы
            new StringConvertTestData("123.12", "123.12"),
            new StringConvertTestData("5 - 3", "5 3 -"),
            new StringConvertTestData("5 / 3", "5 3 /"),
            new StringConvertTestData("5 + 3", "5 3 +"),
            new StringConvertTestData("5 * 3", "5 3 *"),
            new StringConvertTestData("5 * 3.44", "5 3.44 *"),
            new StringConvertTestData("5 - 3 + 4", "5 3 - 4 +"),
            new StringConvertTestData("4 - 2 * 2", "4 2 2 * -"),
            new StringConvertTestData("(4-2) * 2", "4 2 - 2 *"),
            new StringConvertTestData("10 + 5 * 2", "10 5 2 * +"),
            new StringConvertTestData("(4+5)*7-(3/2+15)", "4 5 + 7 * 3 2 / 15 + -"),
            new StringConvertTestData("{x} + 5 * 2", "{x} 5 2 * +"),
            new StringConvertTestData("(0-3) - (0-3)", "0 3 - 0 3 - -"),
            new StringConvertTestData("-3 + -(8*2)", "0 3 - 0 8 2 * - +"),
            new StringConvertTestData("3 + -3", "3 0 3 - +"),
            new StringConvertTestData("-3 * -3", "0 3 - 0 3 - *"),
            new StringConvertTestData("(8 + 2 * 5) / (1 + 3 * 2 - 4)", "8 2 5 * + 1 3 2 * + 4 - /"),
            new StringConvertTestData("-3 - -3", "0 3 - 0 3 - -"),
            new StringConvertTestData("(-3) - (-3)", "0 3 - 0 3 - -"),
            new StringConvertTestData("--3", "3 --"),
            new StringConvertTestData("++3", "3 ++"),
            new StringConvertTestData("++3 - 5 ", "3 ++ 5 -"),
            new StringConvertTestData("++3 * 5 ", "3 ++ 5 *"),
            new StringConvertTestData("++3 / 5 ", "3 ++ 5 /"),
            new StringConvertTestData("--3 - 5 ", "3 -- 5 -"),
            new StringConvertTestData("--3 * 5 ", "3 -- 5 *"),
            new StringConvertTestData("--3 / 5 ", "3 -- 5 /"),

            // TODO: Тесты на переменные
            new StringConvertTestData("{Variable}", "{Variable}"),

            //TODO: дописать Тесты на логические операторы
            new StringConvertTestData("5 != 3 ", "5 3 !="),
            new StringConvertTestData("5 < 3 ", "5 3 <"),
            new StringConvertTestData("5 > 3 ", "5 3 >"),
            new StringConvertTestData("5 >= 3 ", "5 3 >="),
            new StringConvertTestData("5 <= 3 ", "5 3 <="),
            new StringConvertTestData("2 > 0 || 3 < 5", "2 0 > 3 5 < ||"),
            new StringConvertTestData("2 > 0 && 3 < 5", "2 0 > 3 5 < &&"),
            new StringConvertTestData("!(2 > 0) && 3 < 5", "2 0 > ! 3 5 < &&"),
            new StringConvertTestData("2 > 2 + 0 && 3 < 5", "2 2 0 + > 3 5 < &&"),

            //TODO: тесты на функции


        };

        /// <summary>
        /// Получить тест-кейсы для преобразования из инфиксной строки в постфиксную
        /// </summary>
        /// <returns></returns>
        private static List<TestCaseData> GetPostfixStringTestCases()
        {
            return PostfixConvertationData.Select(t =>
            {
                var inputString = t.InputString;
                var outputString = t.OutputString;

                var testCaseData = new TestCaseData(inputString, outputString);
                testCaseData.SetName($"WarsawNotation_ConvertToPostfix_{inputString}");

                return testCaseData;
            })
           .ToList();
        }

        public IWarsawNotationService WarsawNotationService { get; set; }

        /// <summary>
        /// Перед запуском каждого теста
        /// </summary>
        [SetUp]
        public override void BeforeTestRun()
        {
            base.BeforeTestRun();
        }

        /// <summary>
        /// Регистрация типов для DI
        /// </summary>
        public override void RegisterAssemblies()
        {
            ContainerManager.RegisterAssembliesTypes(typeof(WorkFlowBilling.Translator.Impl.AssemblyRef).Assembly);
        }

        [Test]
        [Category("Functional")]
        public void WarsawNotation_VariableNoCloseChar()
        {
            //GIVEN
            var inputString = "{Variable";

            //WHEN
            TestDelegate action = () => WarsawNotationService.ConvertToPostfixString(inputString);

            //THEN
            Assert.Throws<StringConvertationException>(action);
        }

        [Test, TestCaseSource(nameof(GetPostfixStringTestCases))]
        [Category("Functional")]
        public void WarsawNotation_ConvertToPostfix(string inputString, string awaitedOutputString )
        {
            //WHEN
            var postfixString = WarsawNotationService.ConvertToPostfixString(inputString);

            //THEN
            Assert.AreEqual(awaitedOutputString, postfixString);
        }
    }
}
