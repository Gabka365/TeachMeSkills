$(document).ready(function () {

    $('.u-btn').hide();

    $('.u-image').click(function () {
        if ($(this).hasClass('active')) {
            $(this).removeClass('active');
        } else {
            $('.u-image').removeClass('active');
            $('.u-btn').closest('.custom-expanded').find('.u-btn').hide();
            $(this).addClass('active');
            $(this).closest('.custom-expanded').find('.u-btn').show();
        }
    });

    $('.u-image').on('mouseover', function () {
        $(this).css('cursor', 'pointer');
    });

    $(document).on('click', function (event) {
        if (!$(event.target).closest('.u-image').length) {
            $('.u-btn').hide();
            $('.u-image').removeClass('active');
        }
    });


    $('.del-post').click(function () {
        const postElement = $(this).closest('.post_wrapper');
        const postID = $(this).data('post-id');

        const url = `/api/Traveling/DeletePost?postId=${postID}`;
        $.ajax({
            url: url,
            type: 'DELETE',
            success: function (result) {
                console.log('Post deleted successfully');
                postElement.remove();
            },
            error: function (xhr, status, error) {
                console.error('Error deleting post:', error);
            }
        });
    });

    $('.like-button').click(function () {
        const button = $(this);
        const countLikesElement = $(this).closest('.post_wrapper').find('.countLikes');
        const postId = button.data('post-id')


        const url = `/api/Traveling/LikePost?postId=${postId}`;

        const promise = $.ajax({
            url: url
        });

        promise.done(function (response) {

            const likesCount = response;
            console.log('New Likes Count:', likesCount);
            countLikesElement.text(likesCount);
        });

        promise.fail(function (xhr, status, error) {
            console.error(error);
        });
    });

});

$(document).ready(function () {
    const urlNewMessage = `/CommentTraveling/AddNewComment`;
    const enterKeyCode = 13;  

    const hub = new signalR.HubConnectionBuilder()
        .withUrl("/hubs/CommentTraveling")
        .build();

    hub.on('NotifyAboutNewComment', function (travelingCreateComment) {
        addNewComment(travelingCreateComment);
    });


    hub.start(); 

    $('.new-comments-text').on('keyup', function (evt) {
        if (evt.keyCode == enterKeyCode) {
            sendMessage();
            evt.preventDefault();
        }
    });

    $('.new-comments-button').click(sendMessage);

    function sendMessage() {
        const button = $(this);
        const text = button.prev('.new-comments-text').val();
        const trevelingId = button.data('post-id')

        const travelingCreateComment = {
            Text: text,
            TravelingId: trevelingId
        };

        hub.invoke('AddNewComment', travelingCreateComment);
        $('.new-comments-text').val('');
    }

    const commentTemplate = $(`		
         <div class="comment-block">
             <p class="text"></p>
         </div>`);

    function addNewComment(travelingCreateComment) {
        const newCommentBlock = commentTemplate.clone();
        const id = travelingCreateComment.travelingId
        const text = travelingCreateComment.text        
        newCommentBlock.find('.text').text(text);
        $(`.post-${id}`).append(newCommentBlock);
    }
});



