namespace MoviesReviewsApi.Model
{
	public class ReviewModel
	{
        public int ReviewId { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        public int MovieId { get; set; }
    }
}
