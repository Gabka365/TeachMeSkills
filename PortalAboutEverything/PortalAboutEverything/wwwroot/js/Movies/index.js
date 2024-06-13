$(document).ready(function () {
    const animationTime = 500;

    $('.movie .movieImage').click(function () {
        const movie = $(this)
            .closest('.movie');
        const isCurrentMovieActive = movie.hasClass('active');

        $('.review-container').hide(animationTime);
        $('.deleteMovie').hide(animationTime);
        $('.movie').removeClass('active');

        if (!isCurrentMovieActive) {
            movie
                .find('.review-container')
                .show(animationTime);
            movie
                .find('.deleteMovie')
                .show(animationTime);
            movie.addClass('active');
        }
    });
});
