namespace Nibbles.Engine
{
    public interface IHighScoreStore
    {
        void SaveScore(TopScore score);
        IEnumerable<TopScore> GetScores(Guid gameId);
    }
}
