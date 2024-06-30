$(document).ready(function () {
    const baseApiUrl = `https://localhost:58814/`;

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

        $.post({
            url: url,
            data: JSON.stringify(body),
            contentType: 'application/json; charset=utf-8'
        })
            .done(function () {
                window.location.href = '/Movie/Index/';
            })
            .fail(function (xhr, status, error) {
                console.error('Error:', error);
            });
    });

    

    const reviewTemplate = $(`
		<div class="review">
        <div class="deleteReview">
            <input class="movieIdDel" type="hidden" />
            �������
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
        newReviewBlock.find('.rate').text(`������ ������ ${rate}`);
        $('.review-container').append(newReviewBlock);
    }
});
