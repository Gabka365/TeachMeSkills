$(document).ready(function () {

	const hub = new signalR.HubConnectionBuilder()
		.withUrl("/hubs/movie")
		.build();

	hub.on('NotifyAboutDelitingMovie', function (username, movieName) {
		addNewMessage(username, `deleted ${movieName}`);
	});

	hub.start();

	$('.deleteMovie').click(sendMessage);

	function sendMessage() {
		const movieIdInput = $(this).find('[class=movieIdDel]');
		const movieId = movieIdInput.val()-0;
		hub.invoke('UserDeleteMovie', movieId);
	}

	const messageTemplate = $(`
		<div class="notification">
			<span class="user-name"></span>
			<span class="text"></span>
		</div>`);

	function addNewMessage(username, movieName) {
		const newMessageBlock = messageTemplate.clone();
		newMessageBlock.find('.user-name').text(username);
		newMessageBlock.find('.text').text(movieName);
		$('.notifications-containter').append(newMessageBlock);
	}
});
