namespace SeleniumBot
{
    class TestResults
    {
        public string Tipo { get; set; }
        public string Palavrasporminuto { get; set; }    
        public int Keystrokes { get; set; }
        public int CorrectKeystrokes { get; set; }
        public int WrongKeystrokes { get; set; }
        public double Accuracy { get; set; }
        public int CorrectWords { get; set; }
        public int WrongWords { get; set; }
        public double AverageSpeed { get; set; } // Velocidade de digitação média em palavras por minuto
        public double WordAccuracy { get; set; } // Precisão por palavra em porcentagem

        public double ElapsedTimeInSeconds { get; set; }

        // Construtor para inicializar as novas propriedades
        public TestResults()
        {
            AverageSpeed = 0;
            WordAccuracy = 0;
        }

        // Método para calcular a velocidade de digitação média
        public void CalculateAverageSpeed(double elapsedTimeInSeconds)
        {
            // Calcula a velocidade de digitação média em palavras por minuto
            AverageSpeed = (CorrectWords + WrongWords) / (elapsedTimeInSeconds / 60);
        }

        // Método para calcular a precisão por palavra
        public void CalculateWordAccuracy()
        {
            // Calcula a precisão por palavra em porcentagem
            if (CorrectWords + WrongWords > 0)
            {
                WordAccuracy = (double)CorrectWords / (CorrectWords + WrongWords) * 100;
            }
        }
    }
}
