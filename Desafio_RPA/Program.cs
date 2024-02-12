using System;
using System.Collections.Generic;

namespace SeleniumBot
{
    class Program
    {
        static void Main(string[] args)
        {
            ExibirOpcoes();
        }

        static void ExibirOpcoes()
        {
            Console.WriteLine("Bem-vindo ao programa de teste de digitação!");

            while (true)
            {
                Console.WriteLine("Escolha uma opção:");
                Console.WriteLine("1 - Realizar teste manual");
                Console.WriteLine("2 - Realizar teste automatizado");
                Console.WriteLine("3 - Consultar banco de dados");
                Console.WriteLine("4 - Sair");
                Console.Write("Opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        RealizarTesteManual();
                        break;
                    case "2":
                        RealizarTesteAutomatizado();
                        break;
                    case "3":
                        ConsultarBancoDeDados();
                        break;
                    case "4":
                        Console.WriteLine("Saindo do programa...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Por favor, escolha uma opção válida.");
                        break;
                }

                Console.WriteLine(); // Adiciona uma linha em branco para separar as opções
            }
        }

        static void RealizarTesteManual()
        {
            ManualTest automation = new ManualTest();
            var results = automation.RealizarTesteManual();

            DatabaseManager.SaveResults(results, "Humano");
            Console.WriteLine("Teste manual concluído.");
        }

        static void RealizarTesteAutomatizado()
        {
            RoboTeste automation = new RoboTeste();
            var results = automation.TesteWeb();

            DatabaseManager.SaveResults(results, "Robô");
            Console.WriteLine("Teste automatizado concluído.");
        }

        static void ConsultarBancoDeDados()
        {
            List<TestResults> resultadosHumanos = DatabaseManager.GetResultsFromDatabase("Humano");
            List<TestResults> resultadosRobos = DatabaseManager.GetResultsFromDatabase("Robô");

            if (resultadosHumanos.Count > 0 && resultadosRobos.Count > 0)
            {
                CompararResultados(resultadosHumanos, resultadosRobos);
            }
            else
            {
                Console.WriteLine("Não há resultados suficientes para comparar.");
            }
        }

        static void CompararResultados(List<TestResults> resultadosHumanos, List<TestResults> resultadosRobos)
        {
            if (resultadosHumanos.Count > 0 && resultadosRobos.Count > 0)
            {
                Console.WriteLine("--- Comparação entre Testes Manuais (Humano) e Testes Automatizados (Robô) ---");

                // Calcular médias para cada métrica
                

                double mediaKeystrokesHumano = resultadosHumanos.Average(r => r.Keystrokes);
                double mediaKeystrokesRobo = resultadosRobos.Average(r => r.Keystrokes);

                double mediaCorrectKeystrokesHumano = resultadosHumanos.Average(r => r.CorrectKeystrokes);
                double mediaCorrectKeystrokesRobo = resultadosRobos.Average(r => r.CorrectKeystrokes);

                double mediaWrongKeystrokesHumano = resultadosHumanos.Average(r => r.WrongKeystrokes);
                double mediaWrongKeystrokesRobo = resultadosRobos.Average(r => r.WrongKeystrokes);

                double mediaAccuracyHumano = resultadosHumanos.Average(r => r.Accuracy);
                double mediaAccuracyRobo = resultadosRobos.Average(r => r.Accuracy);

                double mediaCorrectWordsHumano = resultadosHumanos.Average(r => r.CorrectWords);
                double mediaCorrectWordsRobo = resultadosRobos.Average(r => r.CorrectWords);

                double mediaWrongWordsHumano = resultadosHumanos.Average(r => r.WrongWords);
                double mediaWrongWordsRobo = resultadosRobos.Average(r => r.WrongWords);

                // Exibir diferenças percentuais
                
                ExibirDiferencaPercentual("Keystrokes", mediaKeystrokesHumano, mediaKeystrokesRobo);
                ExibirDiferencaPercentual("Correct Keystrokes", mediaCorrectKeystrokesHumano, mediaCorrectKeystrokesRobo);
                ExibirDiferencaPercentual("Wrong Keystrokes", mediaWrongKeystrokesHumano, mediaWrongKeystrokesRobo);
                ExibirDiferencaPercentual("Accuracy", mediaAccuracyHumano, mediaAccuracyRobo);
                ExibirDiferencaPercentual("Correct Words", mediaCorrectWordsHumano, mediaCorrectWordsRobo);
                ExibirDiferencaPercentual("Wrong Words", mediaWrongWordsHumano, mediaWrongWordsRobo);
            }
        }

        static void ExibirDiferencaPercentual(string metrica, double valorHumano, double valorRobo)
        {
            double diferencaPercentual = ((valorRobo - valorHumano) / valorHumano) * 100;

            Console.WriteLine($"Diferença Percentual de {metrica}: {diferencaPercentual}%");
        }

    }
}
