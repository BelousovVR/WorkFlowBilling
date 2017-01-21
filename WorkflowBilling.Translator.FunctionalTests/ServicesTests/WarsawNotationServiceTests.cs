using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WorkFlowBilling.Compiler.Services;
using WorkFlowBilling.TestFramework.Common;
using WorkFlowBilling.Compiler.Exceptions;
using WorkflowBilling.Translator.FunctionalTests.Common;

namespace WorkflowBilling.Translator.FunctionalTests.ServicesTests
{
    /// <summary>
    /// Тесты на сервис обратной польской записи
    /// </summary>
    [TestFixture]
    public class WarsawNotationServiceTests : BaseFunctionalTest
    {

        /// <summary>
        /// Исходные данные об инфиксной записи выражения и постфиксном ожидаемом выражении
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
            new StringConvertTestData("{x} + 5 * 2", "{x} 5 2 * +"),

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

            //TODO: дописать тесты на функции
            new StringConvertTestData("Max(1,2) ", "1 2 Max"),
            new StringConvertTestData("Max(1 + 2 , 2 * 3) ", "1 2 + 2 3 * Max"),
            new StringConvertTestData("Max( 3 * (2-5) , -3) ", "3 2 5 - * 0 3 - Max"),
            new StringConvertTestData("Max(Max(1,3) , Max(2,4))", "1 3 Max 2 4 Max Max"),
            new StringConvertTestData("5 + Max(1,2) ", "5 1 2 Max +"),
            new StringConvertTestData("Max(1,2) - 5 ", "1 2 Max 5 -"),


            // TODO: дописать тесты на тернарный оператор
            new StringConvertTestData("2 > 3 ? 4 : 5", "2 3 > 4 5 ?:"),
            new StringConvertTestData("2 > 3 ? 4 + 5 : 6 + 7", "2 3 > 4 5 + 6 7 + ?:"),
            new StringConvertTestData("2 > 3 ? Max(1,0) + 5 : 6 + 7", "2 3 > 1 0 Max 5 + 6 7 + ?:"),
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
