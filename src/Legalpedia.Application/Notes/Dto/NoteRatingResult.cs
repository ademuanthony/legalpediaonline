namespace Legalpedia.Notes.Dto
{
    public class NoteRatingResult
    {
        public NoteRatingResult(int total, int count)
        {
            Count = count;
            if (count > 0)
                AverageRating = (double) total / count;
        }
        
        public double AverageRating { get; set; }
        public int Count { get; set; }
    }
}