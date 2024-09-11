$(document).ready(function () {

	$('.menu div').show(500);
	$('.ava').show(500);
	$('.text-about-blog-author').show(500);
	$('.interactive-section').show(500); 

	$('.interactive-section .post').click(function () {
		const currentBlock = $(this).closest('.post');
		const isCurrentBlockActive = currentBlock.hasClass('.active');

		$('.post').removeClass('active');
		$('.post-footer').hide(500);

		if (!isCurrentBlockActive) {
			currentBlock.addClass('active');
			currentBlock.find('.post-footer').show(500);
		}
		
	});


	$('.like-cover').click(function () {

		$(this).removeClass(.like)


		let img = $(this).find('img');

		if (img.attr('src') === '/images/Blog/png/like.png') {
			img.attr('src', '/images/Blog/png/like_pressed.png');

			setTimeout(function () {
				img.attr('src', '/images/Blog/png/like.png');
			}, 2 * 1000);
		}
	});

});