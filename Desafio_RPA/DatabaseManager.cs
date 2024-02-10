using Npgsql;

namespace SeleniumBot
{
    class DatabaseManager
    {
        public static void SaveResults(TestResults results, string tipoTeste)
        {
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=123456789;Database=postgres";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO resultados (tipo,palavrasporminuto,keystrokes, correct_keystrokes,wrong_keystrokes,accuracy, correct_words, wrong_words,Tempo_decorrido_em_segundo,velocidade_media, precisao_por_palavra) " +
                     "VALUES (@Tipo,@palavrasporminuto,@Keystrokes, @CorrectKeystrokes,@WrongKeystrokes,@Accuracy, @CorrectWords, @WrongWords,@Tempo_decorrido_em_segundo ,@VelocidadeMedia, @PrecisaoPorPalavra)";

                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("Tipo", tipoTeste); 
                    cmd.Parameters.AddWithValue("palavrasporminuto", results.Palavrasporminuto);
                    cmd.Parameters.AddWithValue("Keystrokes", results.Keystrokes);
                    cmd.Parameters.AddWithValue("CorrectKeystrokes", results.CorrectKeystrokes);
                    cmd.Parameters.AddWithValue("WrongKeystrokes", results.WrongKeystrokes);
                    cmd.Parameters.AddWithValue("Accuracy", results.Accuracy);
                    cmd.Parameters.AddWithValue("CorrectWords", results.CorrectWords);
                    cmd.Parameters.AddWithValue("WrongWords", results.WrongWords);
                    cmd.Parameters.AddWithValue("Tempo_decorrido_em_segundo", results.ElapsedTimeInSeconds);
                    cmd.Parameters.AddWithValue("VelocidadeMedia", results.AverageSpeed);
                    cmd.Parameters.AddWithValue("PrecisaoPorPalavra", results.WordAccuracy);

                    cmd.ExecuteNonQuery();
                }
            }

        }
        public static List<TestResults> GetResultsFromDatabase()
        {
            List<TestResults> resultsList = new List<TestResults>();

            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=123456789;Database=postgres";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM resultados";

                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TestResults results = new TestResults
                            {
                                Palavrasporminuto = reader["palavrasporminuto"].ToString(),
                                Keystrokes = Convert.ToInt32(reader["keystrokes"]),
                                CorrectKeystrokes = Convert.ToInt32(reader["correct_keystrokes"]),
                                WrongKeystrokes = Convert.ToInt32(reader["wrong_keystrokes"]),
                                Accuracy = Convert.ToDouble(reader["accuracy"]),
                                CorrectWords = Convert.ToInt32(reader["correct_words"]),
                                WrongWords = Convert.ToInt32(reader["wrong_words"]),
                                ElapsedTimeInSeconds = Convert.ToDouble(reader["tempo_decorrido_em_segundo"]),
                                AverageSpeed = Convert.ToDouble(reader["velocidade_media"]),
                                WordAccuracy = Convert.ToDouble(reader["precisao_por_palavra"])
                            };

                            resultsList.Add(results);
                        }
                    }
                }
            }

            return resultsList;
        }
    }
}
