using Newtonsoft.Json;

namespace Nibbles.Engine
{

    public class TopScoreStore : ITopScoreStore
    {
        private const string _scoreFilePath = @"\scores\";
        private const string _scoreFileName = "top-scores.json";

        public TopScoreStore()
        {
            InitializeDirectory(FullDirectoryPath);
        }

        public IEnumerable<TopScore> GetAllScores()
        {
            return GetScoresFromFile()
                .OrderByDescending(score => score.Score);
        }

        public IEnumerable<TopScore> GetScores(Guid gameId)
        {
            return GetAllScores()
                .Where(score => score.GameId == gameId);
        }

        public void SaveScore(TopScore score)
        {
            var allScores = GetAllScores().ToList();

            var gameScores = allScores
                .Where(s => s.GameId == score.GameId)
                .ToList();

            var scoreTooLow = gameScores.Any() 
                && !gameScores.Any(s => s.Score < score.Score);

            if (scoreTooLow)
            {
                return;
            }
            if(gameScores.Count == 10)
            {
                gameScores.RemoveAt(gameScores.Count - 1);
            }
            gameScores.Add(score);
            allScores.RemoveAll(s => s.GameId == score.GameId);
            allScores.AddRange(gameScores);
            SaveScores(allScores);
        }

        private static void InitializeDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private static string FullDirectoryPath =>            
            $"{Directory.GetCurrentDirectory()}{_scoreFilePath}";

        private static string FullFilePath =>
             $"{FullDirectoryPath}{_scoreFileName}";

        private static IEnumerable<TopScore> GetScoresFromFile()
        {
            if (!File.Exists(FullFilePath)) return Enumerable.Empty<TopScore>();

            using var stream = File.Open(FullFilePath, FileMode.Open, FileAccess.Read);
            using var reader = new StreamReader(stream);
            {
                var content = reader.ReadToEnd();
                var scores = JsonConvert.DeserializeObject<IEnumerable<TopScore>>(content, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    Formatting = Formatting.Indented,
                });

                if (scores is null) return Enumerable.Empty<TopScore>();

                return scores;
            }
        }
        private void SaveScores(IEnumerable<TopScore> scores)
        {
            File.WriteAllText(FullFilePath, JsonConvert.SerializeObject(scores, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            }));
        }
    }
}
