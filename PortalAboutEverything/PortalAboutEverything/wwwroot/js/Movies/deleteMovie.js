$(document).ready(function () {

	const debounceDeleteMovie = debounce(deleteMovie, 500);

	$('.deleteMovie').click(function () {
		debounceDeleteMovie.call();
	});

	function deleteMovie() {
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
				}
			});
	}
});
