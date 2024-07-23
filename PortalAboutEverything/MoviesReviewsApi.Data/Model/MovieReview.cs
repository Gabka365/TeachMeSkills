
namespace MoviesReviewsApi.Data.Model
{
    public class MovieReview
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string Comment { get; set; }

        public int MovieId { get; set; }
    }
}
