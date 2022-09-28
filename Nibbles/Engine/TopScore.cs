namespace Nibbles.Engine
{
    public record TopScore
    {
        public Guid GameId { get; private set; }
        public string Initials { get; private set; }
        public long Score { get; private set; }

        public TopScore(Guid gameId, string initials, long score)
        {
            var invalidInitials = string.IsNullOrWhiteSpace(initials) 
                || initials.Contains(' ') 
                || initials.Length is < 3 or > 3;

            if (invalidInitials)
            {
                throw new ArgumentException("The score must be a non-empty string of length 3");
            }            
            if(gameId == Guid.Empty)
            {
                throw new ArgumentException("Guid must not be empty");
            }
            GameId = gameId;
            Initials = initials;
            Score = score;
        }
    }
}
