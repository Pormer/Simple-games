namespace Code.Score
{
    public static class ScoreEvent
    {
        public static SetScore SetScore = new SetScore();
    }

    public class SetScore : GameEvent
    {
        public int score;
    }
}