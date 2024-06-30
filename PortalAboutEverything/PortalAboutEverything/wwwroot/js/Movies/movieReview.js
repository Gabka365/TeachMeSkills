$(document).ready(function () {
    const baseApiUrl = `https://localhost:7087/`;


    $('.sendReview').click(function () {
        const rate = $('input[name="rate"]:checked').val() - 0;
        const comment = $('.comment').val();
        const movieId = $('[name=MovieId]').val() - 0;
        const body = {
            rate: rate,
            comment: comment,
            movieId: movieId
        }
        
        const url = baseApiUrl + 'addReview';

        $.get(url, body)
            .done(function (reviews) {
                reviews.forEach((review) => {
                    addNewReview(review.movieId, review.dateOfCreation, review.comment, review.rate);
                });
            })
            .fail(function (xhr, status, error) {
                console.error('Ошибка при выполнении запроса:', error);
            });
            
    });

    

    const reviewTemplate = $(`
		<div class="review">
        <div class="deleteReview">
            <input class="movieIdDel" type="hidden" />
            Удалить
        </div>
        <div class="dateReview"></div>
        <div class="comment"></div>
        <div class="rate"></div>
    </div>`);

    function addNewReview(movieId, dateOfCreation, comment, rate) {
        const newReviewBlock = reviewTemplate.clone();
        newReviewBlock.find('.movieIdDel').val(movieId);
        newReviewBlock.find('.dateReview').text(dateOfCreation);
        newReviewBlock.find('.comment').text(comment);
        newReviewBlock.find('.rate').text(`Оценка фильма ${rate}`);
        $('.review-container').append(newReviewBlock);
    }
});
