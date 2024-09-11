$(document).ready(function () {
    const baseApiUrl = `https://localhost:58814/`;

    $('.deleteMovie').click(function () {
        const movieToDelete = $('.movie.active');
        const movieInput = movieToDelete.find('.movieIdDel')
        const movieId = movieInput.val();

        const tableStatisticCellByMovieId = $(`.movieId[value="${movieId}"]`);
        const rowToDelete = tableStatisticCellByMovieId.closest('.movieContent');

        const url = `/api/Movie/DeleteMovie?movieId=${movieId}`;
        $.get(url)
            .done(function (isDeleted) {
                if (isDeleted) {
                    movieToDelete.remove();
                    rowToDelete.remove();
                    deleteReview();
                } else {
                    alert("Failed to delete movie");
                }
            });

        function deleteReview() {
            $.post(baseApiUrl + `deleteAllReviewsByMovieId?movieId=${movieId}`)
                .done(function () {
                });
        }
    });
});
