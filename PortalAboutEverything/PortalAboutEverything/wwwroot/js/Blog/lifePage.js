$(document).ready(function () {
    const baseApiUrl = `https://localhost:7103/`;
    const userName = $('[name=userName]').val();

    const hub = new signalR.HubConnectionBuilder()
        .withUrl(baseApiUrl + "hubs/BlogComment", {
            headers: { authKey: '123' }
        })
        .build();

    init();

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


    function init() {
        $.get(baseApiUrl + 'getAllComments')
            .done(function (comments) {
                comments.forEach((comment) => {
                    addNewComment(comment.name, comment.message, comment.currentTime, comment.postId); //дописать то, что должно вернуться
                });
            })
    }



    const commentTemplate = $(`
        <div class="comment">
            <span class="author-name"></span>
            <span class="date"></span>
            <span class="comment-text"></span>
        </div>
    `);

    function addNewComment(username, text, datetime, postId) {

        const commentsSection = $(`.post-footer input.post-comment[value="${postId}"]`)
            .closest('.post')
            .next('.comments');


        const newComment = commentTemplate.clone();
        newComment.find('.author-name').text(username);
        newComment.find('.date').text(datetime);
        newComment.find('.comment-text').text(text);



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

        hub.invoke("AddNewComment", userName, text, postId)
            .catch(function (err) {
                console.error("Failed to invoke AddNewComment:", err);
            });
    }
});