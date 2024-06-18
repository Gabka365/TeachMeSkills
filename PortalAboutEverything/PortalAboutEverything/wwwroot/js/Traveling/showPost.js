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
        const postID = $(this).data('post-id'); 
        console.log('Post ID:', postID); 

        const url = `/api/Traveling/DeletePost?postId=${postID}`;
        $.ajax({
            url: url,
            type: 'DELETE',
            success: function (result) {
                console.log('Post deleted successfully');
                location.reload();
            },
            error: function (xhr, status, error) {
                console.error('Error deleting post:', error);
            }
        });
    });
});
