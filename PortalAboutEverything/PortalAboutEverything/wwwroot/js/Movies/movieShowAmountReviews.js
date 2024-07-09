$(document).ready(function () {
    const baseApiUrl = `https://localhost:58814/`;

    init();

    function init() {
        const url = '/api/Movie/FindAllMovieId';

        $.get(url)
            .done(function (moviesId) {
                moviesId.forEach((movieId) => {
                    $.get(baseApiUrl + `getAmountOfReviewsByMovie?movieId=${movieId}`)
                        .done(function (amountReviews) {
                            addCountOfReview(amountReviews, movieId);
                        })
                });
            })
    }

    function addCountOfReview(amountReviews, movieId) {
        const movieIdTag = $(`.movieId[value="${movieId}"]`);
        const movieContent = movieIdTag.closest('.movieContent');
        movieContent.find('.countOfReviews').text(amountReviews);
    }
});
