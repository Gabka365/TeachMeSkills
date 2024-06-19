$(document).ready(function () {
	const urlNewMessage = `/chat/AddNewMessage`;
	const enterKeyCode = 13;

	const hub = new signalR.HubConnectionBuilder()
		.withUrl("/hubs/chat")
		.build();

	hub.on('NotifyAboutNewMessage', function(username, text){
		addNewMessage(username, text);
	});

	hub.on('NotifyAboutNewUser', function(username){
		addNewMessage('', `${username} enter to the chat`);
	});

	hub
		.start()
		.then(function(){
			hub.invoke('UserConnectToChat');
		});// open web socket

	$('.new-message-text').on('keyup', function(evt){
		if (evt.keyCode == enterKeyCode){
			sendMessage();
			evt.preventDefault();
		}
	});

	$('.new-message-button').click(sendMessage);

	function sendMessage() {
		const text = $('.new-message-text').val();
		hub.invoke('AddNewMessage', text);
		$('.new-message-text').val('');
	}

	const messageTemplate = $(`
		<div class="message">
			<span class="user-name"></span>
			<span class="text"></span>
		</div>`);

	function addNewMessage(username, text) {
		const newMessageBlock = messageTemplate.clone();
		newMessageBlock.find('.user-name').text(username);
		newMessageBlock.find('.text').text(text);
		$('.messages').append(newMessageBlock);
	}
});