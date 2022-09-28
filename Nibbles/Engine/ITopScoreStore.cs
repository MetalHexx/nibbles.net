namespace Nibbles.Engine
{
    public interface ITopScoreStore
    {
        void SaveScore(TopScore score);
        IEnumerable<TopScore> GetScores(Guid gameId);
    }
}
