$(document).ready(function () {
    const baseApiUrl = `https://localhost:7072/`;
    const enterKeyCode = 13;
    const userName = $('[name=userName]').val();

    const hub = new signalR.HubConnectionBuilder()
        .withUrl(baseApiUrl + "hubs/chat", {
            headers: { authKey: '123' }
        })
        .build();

    init();

    hub.on('NotifyAboutNewMessage', function (username, text) {
        addNewMessage(username, text);
    });

    hub.on('NotifyAboutNewUser', function (username) {
        addNewMessage('', `${username} enter to the chat`);
    });

    hub
        .start()
        .then(function () {
            hub.invoke('UserConnectToChat', userName);
        });// open web socket

    $('.new-message-text').on('keyup', function (evt) {
        if (evt.keyCode == enterKeyCode) {
            sendMessage();
            evt.preventDefault();
        }
    });

    $('.new-message-button').click(sendMessage);

    function init() {
        $.get(baseApiUrl + 'getLastMessages')
            .done(function (messages) {
                messages.forEach((message) => {
                    addNewMessage(message.authorName, message.text);
                });
            })
    }

    function sendMessage() {
        const text = $('.new-message-text').val();
        hub.invoke('AddNewMessage', userName, text);
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