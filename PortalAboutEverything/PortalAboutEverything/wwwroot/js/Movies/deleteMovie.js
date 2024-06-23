$(document).ready(function () {

    $('.deleteMovie').click(function () {
        const movieToDelete = $('.movie.active');
        const movieInput = movieToDelete.find('.movieIdDel')
        const movieId = movieInput.val();

        const tableStatisticCellByMovieId = $(`.movieTableCellId[value="${movieId}"]`);
        const rowToDelete = tableStatisticCellByMovieId.closest('.movieContent');

        const url = `/api/Movie/DeleteMovie?movieId=${movieId}`;
        $.get(url)
            .done(function (isDeleted) {
                if (isDeleted) {
                    movieToDelete.remove();
                    rowToDelete.remove();
                } else {
                    alert("Не удалось удалить фильм");
                }
            });
    });
});
