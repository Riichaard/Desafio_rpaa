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
                        RealizarTesteManual("Humano");
                        break;
                    case "2":
                        RealizarTesteAutomatizado("Robo");
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

        static void RealizarTesteManual(string tipoTeste)
        {
            ManualTest automation = new ManualTest();
            var results = automation.RealizarTesteManual();

            DatabaseManager.SaveResults(results, tipoTeste);
            Console.WriteLine("Teste manual concluído.");
            Console.WriteLine(); // Adiciona uma linha em branco após a mensagem
        }

        static void RealizarTesteAutomatizado(string tipoTeste)
        {
            AutomationWeb automation = new AutomationWeb();
            var results = automation.TesteWeb();

            DatabaseManager.SaveResults(results, tipoTeste);
            Console.WriteLine("Teste automatizado concluído.");
            Console.WriteLine(); // Adiciona uma linha em branco após a mensagem
        }

        static void ConsultarBancoDeDados()
        {
            List<TestResults> resultsList = DatabaseManager.GetResultsFromDatabase();

            if (resultsList.Count > 0)
            {
                Console.WriteLine("Resultados encontrados no banco de dados:");

                foreach (var results in resultsList)
                {
                    Console.WriteLine($"Palavras por minuto: {results.Palavrasporminuto}");
                    Console.WriteLine($"Keystrokes: {results.Keystrokes}");
                    Console.WriteLine($"Precisão da palavra: {results.WordAccuracy}%");
                    Console.WriteLine("--------------------------------------");
                }
            }
            else
            {
                Console.WriteLine("Nenhum resultado encontrado no banco de dados.");
            }

            Console.WriteLine(); // Adiciona uma linha em branco após os resultados
        }
    }
}
