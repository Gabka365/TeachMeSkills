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
});
