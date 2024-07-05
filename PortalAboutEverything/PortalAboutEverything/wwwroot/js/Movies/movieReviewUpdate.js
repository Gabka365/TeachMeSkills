$(document).ready(function () {
    const baseApiUrl = `https://localhost:58814/`;

    init();

    function init() {
        const reviewId = $('[name=ReviewId]').val();

        $.get(baseApiUrl + `findReviewInfo?reviewId=${reviewId}`)
            .done(function (review) {
                const newReviewBlock = $(`<textarea name="comment" class="comment">${review.comment}</textarea>`);
                $('.comment').append(newReviewBlock);
                $(`input[name="rate"][value=${review.rate}]`).prop('checked', true);
            })
    }

    $('.sendReview').click(function () {
        const rate = $('input[name="rate"]:checked').val() - 0;
        const comment = $('[name=comment]').val();
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
