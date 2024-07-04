$(document).ready(function () {
    const baseApiUrl = `https://localhost:58814/`;

    init();

    function init() {
        const reviewId = $('.reviewId').val();

        $.get(baseApiUrl + `findReviewInfo?movieId=${reviewId}`)
            .done(function (review) {
                $('.comment').text(review.comment);
                $('.rate').val(review.rate);
            })
    }

    $('.sendReview').click(function () {
        const rate = $('input[name="rate"]:checked').val() - 0;
        const comment = $('.comment').val();
        const reviewId = $('[name=ReviewId]').val() - 0;
        const body = {
            rate: rate,
            comment: comment,
            reviewId: reviewId,
        }

        const url = baseApiUrl + 'updateReview';

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
});
