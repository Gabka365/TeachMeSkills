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

});