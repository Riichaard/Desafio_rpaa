using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Threading;

namespace SeleniumBot
{
    class ManualTest
    {
        public TestResults RealizarTesteManual()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
            IWebDriver driver = new ChromeDriver(options);

            try
            {
                // Abrindo a página do teste de digitação
                driver.Navigate().GoToUrl("https://10fastfingers.com/typing-test/portuguese");

                // Registra o tempo inicial
                DateTime startTime = DateTime.Now;

                // Registra o tempo final
                DateTime endTime = DateTime.Now;

                // Calcula o tempo decorrido em segundos
                double elapsedTimeInSeconds = (endTime - startTime).TotalSeconds;

                // Obtém os resultados do website
                var results = GetResultsFromWebsite(driver, elapsedTimeInSeconds);

                // Calcula a velocidade média
                results.CalculateAverageSpeed(elapsedTimeInSeconds);

                results.CalculateWordAccuracy();

                results.Tipo = "Humano";

                return results;

                // Exibe os resultados
                Console.WriteLine("Resultados do teste manual:");
                Console.WriteLine($"Palavras por minuto: {results.Palavrasporminuto}");
                Console.WriteLine($"Keystrokes: {results.Keystrokes}");
                Console.WriteLine($"Precisão da palavra: {results.WordAccuracy}%");

                // Retorna os resultados
                return results;
            }
            finally
            {
                // Fechando o navegador após o teste
                //driver.Quit();
            }
        }

        private TestResults GetResultsFromWebsite(IWebDriver driver, double elapsedTimeInSeconds)
        {
            // Localize os elementos na página usando XPath
            Thread.Sleep(65000); // Espera de 65 segundos
            IWebElement palavrasporminutoElement = driver.FindElement(By.XPath("//*[@id=\'wpm\']/strong"));
            IWebElement correctKeystrokesElement = driver.FindElement(By.XPath("//*[@id=\'keystrokes\']/td[2]/small/span[1]"));
            IWebElement wrongKeystrokesElement = driver.FindElement(By.XPath("//*[@id=\'keystrokes\']/td[2]/small/span[2]"));
            IWebElement accuracyElement = driver.FindElement(By.XPath("//*[@id=\'accuracy\']/td[2]/strong"));
            IWebElement correctWordsElement = driver.FindElement(By.XPath("//*[@id=\'correct\']/td[2]/strong"));
            IWebElement wrongWordsElement = driver.FindElement(By.XPath("//*[@id=\'wrong\']/td[2]/strong"));

            // Obter os valores dos elementos
            string palavrasporminuto = palavrasporminutoElement.Text;
            int correctKeystrokes = int.Parse(correctKeystrokesElement.Text);
            int wrongKeystrokes = int.Parse(wrongKeystrokesElement.Text);
            int correctWords = int.Parse(correctWordsElement.Text);
            int wrongWords = int.Parse(wrongWordsElement.Text);

            // Obter a precisão da palavra
            string accuracyText = accuracyElement.Text.Replace("%", "");
            double accuracy = double.Parse(accuracyText);

            // Crie e retorne os resultados do teste
            return new TestResults
            {
                Palavrasporminuto = palavrasporminuto,
                Keystrokes = correctKeystrokes + wrongKeystrokes,
                CorrectKeystrokes = correctKeystrokes,
                WrongKeystrokes = wrongKeystrokes,
                Accuracy = accuracy,
                CorrectWords = correctWords,
                WrongWords = wrongWords,
                ElapsedTimeInSeconds = elapsedTimeInSeconds
            };
        }
    }
}
