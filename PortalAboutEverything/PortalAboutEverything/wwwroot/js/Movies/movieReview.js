$(document).ready(function () {
    const baseApiUrl = `https://localhost:58814/`;

    $('.movie .movieImage').click(function () {
        $('.review').remove();
        const movie = $(this).closest('.movie');
        const movieId = movie.find('.movieId').val();
        
        $.get(baseApiUrl + `findReviewsByMovieId?movieId=${movieId}`)
            .done(function (reviews) {
                reviews.forEach((review) => {
                    const timeWithData = review.dateOfCreation.slice(0, 19);
                    const time = timeWithData.slice(-8);
                    const date = review.dateOfCreation.slice(0, 10);
                    const dateOfCreation = date + " " + time;
                    addNewReview(review.movieId, dateOfCreation, review.comment, review.rate, review.id);
                });
            });
    });

    const reviewTemplate = $(`
    <div class="review">
        <div><input class="reviewId" type="hidden" /></div>
        <div class="deleteReview">
            <input class="movieIdDel" type="hidden" />
             <div class="delete">Delete</div>
             <div class="update">Update</div>
        </div>
        <div class="dateReview"></div>
        <div class="comment"></div>
        <div class="rate"></div>
    </div>`);

    function addNewReview(movieId, dateOfCreation, comment, rate, reviewId) {
        const newReviewBlock = reviewTemplate.clone();
        newReviewBlock.find('.reviewId').val(reviewId);
        newReviewBlock.find('.movieIdDel').val(movieId);
        newReviewBlock.find('.dateReview').text(dateOfCreation);
        newReviewBlock.find('.comment').text(comment);
        newReviewBlock.find('.rate').text(`The rate of the movie is ${rate}`);
        $('.review-container').append(newReviewBlock);


        $('.delete').click(function () {
            const review = $(this).closest('.review');
            const reviewId = review.find('.reviewId').val();

            $.get(baseApiUrl + `deleteReview?reviewId=${reviewId}`)
                .done(() => {
                    review.remove();
                });
        });
    }
});
