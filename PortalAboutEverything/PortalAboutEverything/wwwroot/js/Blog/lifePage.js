$(document).ready(function () {


    const hub = new signalR.HubConnectionBuilder()
        .withUrl("/hubs/BlogComment")
        .build();

    hub.on('NotifyAboutNewComment', function (username, text, datetime, postId) {
        addNewComment(username, text, datetime, postId)
    });

    hub
        .start()
        .then(function () {
            console.log("Connected.");
        })
        .catch(function (err) {
            console.error("Connection failed: ", err);
        });


    $('.submit-comment').click(function (event) {
        event.preventDefault(); 
        postComment($(this));
    });


    const commentTemplate = $(`
        <div class="comment">
            <span class="author-name"></span>
            <span class="date"></span>
            <span class="comment-text"></span>
        </div>
    `);

    function addNewComment(username, text, datetime, postId) {
        const newComment = commentTemplate.clone();
        newComment.find('.author-name').text(username);
        newComment.find('.date').text(datetime);
        newComment.find('.comment-text').text(text);

        const commentsSection = $(`.post-footer`).filter(function () {
            return $(this).find('input.post-comment').val() == postId;
        }).next('.comments');

        commentsSection.append(newComment);
    }

    function postComment(currentButton) {
        const text = currentButton
            .closest('form') 
            .find('.textarea-comment')
            .val();

        const postId = currentButton
            .closest('form') 
            .find('.post-comment')
            .val();

        hub.invoke('AddNewComment', text, postId);
    }
});