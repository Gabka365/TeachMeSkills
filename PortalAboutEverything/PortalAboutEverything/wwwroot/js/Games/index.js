$(document).ready(function () {
	const animationTime = 500;

	$('.game .cover').click(function () {
		const game = $(this)
			.closest('.game');
		const isCurrentGameActive = game.hasClass('active');

		$('.comment-container').hide(animationTime);
		$('.game').removeClass('active');
		
		if (!isCurrentGameActive) {
			game
				.find('.comment-container')
				.show(animationTime);
			game.addClass('active');
		}
	});
});
