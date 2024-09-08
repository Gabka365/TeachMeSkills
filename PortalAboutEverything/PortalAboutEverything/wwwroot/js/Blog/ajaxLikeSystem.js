
$(document).ready(function (event) {

    $('.post .post-footer .interaction .like-cover').click(function () {

        
        const elem = $(this);
        const currentPost = $(this).closest('.post');
        const like = currentPost.find('.like-cover');
        const postId = like.val();
        const rateCount = currentPost.find('.rate-count');

        currentPost.attr('disabled', true);

        if (rateCount.hasClass(like)) {
            rateCount.removeClass(like);
        }



        const url = `/api/Blog/GetLikes?id=${postId}`;
        $.get(url)
            .done(function (response) {

                let img = elem.find('img');
                if (img.attr('src') === '/images/Blog/png/like.png') {
                    img.attr('src', '/images/Blog/png/like_pressed.png');
                    setTimeout(function () {
                        img.attr('src', '/images/Blog/png/like.png');
                    }, 3 * 1000);
                }

                const likeCount = response;


                rateCount.addClass('like');
                $('.rate-count.like').text(likeCount);
                $('.rate-count.like').show(1000);
                setTimeout(function () {}, 1000);
                $('.rate-count.like').hide(1000);
                currentPost.attr('disabled', false);


            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                console.error("Ошибка при выполнении запроса:", textStatus, errorThrown);
                currentPost.attr('disabled', false);
            });
    });


    $('.post .post-footer .interaction .dislike-cover').click(function () {

        const elem = $(this);
        const currentPost = $(this).closest('.post');
        const dislike = currentPost.find('.dislike-cover');
        const postId = dislike.val();
        const rateCount = currentPost.find('.rate-count');

        currentPost.attr('disabled', true);

        if (rateCount.hasClass(dislike)) {
            rateCount.removeClass(dislike);
        }

        const url = `/api/Blog/GetDislikes?id=${postId}`;
        $.get(url)
            .done(function (response) {

                let img = elem.find('img');
                if (img.attr('src') === '/images/Blog/png/dislike.png') {
                    img.attr('src', '/images/Blog/png/dislike_pressed.png');
                    setTimeout(function () {
                        img.attr('src', '/images/Blog/png/dislike.png');
                    }, 3 * 1000);
                }

                const dislikeCount = response;

                rateCount.addClass('dislike');
                $('.rate-count.dislike').text(dislikeCount);
                $('.rate-count.dislike').show(1000);
                setTimeout(function () { }, 1000);
                $('.rate-count.dislike').hide(1000);
                currentPost.attr('disabled', false);
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                console.error("Ошибка при выполнении запроса:", textStatus, errorThrown);
                currentPost.attr('disabled', false);
            });
    });

});