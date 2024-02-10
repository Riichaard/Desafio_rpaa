using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;

namespace SeleniumBot
{
    class AutomationWeb
    {
        public TestResults TesteWeb()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
            IWebDriver driver = new ChromeDriver(options);

            try
            {
                // Abrindo a página do teste de digitação
                driver.Navigate().GoToUrl("https://10fastfingers.com/typing-test/portuguese");

                // Esperando até que o botão "Deny" esteja visível na tela
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                IWebElement denyCookiesButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("CybotCookiebotDialogBodyButtonDecline")));

                // Clicando no botão "Deny"
                denyCookiesButton.Click();

                // Localizando o campo de entrada de texto
                IWebElement inputField = wait.Until(ExpectedConditions.ElementExists(By.Id("inputfield")));

                // Esperando até que o contêiner de palavras apareça na tela
                IWebElement wordsContainer = wait.Until(ExpectedConditions.ElementExists(By.Id("row1")));

                // Coletando todas as palavras dentro do container
                IList<IWebElement> words = wordsContainer.FindElements(By.TagName("span"));

                // Registra o tempo inicial
                DateTime startTime = DateTime.Now;

                // Digitando cada palavra seguida da tecla de espaço
                foreach (IWebElement word in words)
                {
                    // Digitando a palavra no campo de entrada
                    inputField.SendKeys(word.Text);
                    inputField.SendKeys(Keys.Space);

                    // Esperando até que uma nova palavra apareça no contêiner
                    wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.Id("row1"), word.Text));
                }

                // Registra o tempo final
                DateTime endTime = DateTime.Now;

                // Calcula o tempo decorrido em segundos
                double elapsedTimeInSeconds = (endTime - startTime).TotalSeconds;

                // Obtém os resultados do website
                var results = GetResultsFromWebsite(driver, elapsedTimeInSeconds);

                // Calcula a velocidade média
                results.CalculateAverageSpeed(elapsedTimeInSeconds);

                results.CalculateWordAccuracy();

                results.Tipo = "Robo";

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
            Thread.Sleep(15000); // Espera de 15 segundos
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
