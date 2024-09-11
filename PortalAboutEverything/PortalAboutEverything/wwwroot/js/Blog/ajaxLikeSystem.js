import { event } from "jquery";

$(document).ready(function () {

    $('.post .post-footer .interaction .like-cover').click(function () {

        let img = $(this).find('img');

        if (img.attr('src') === '/images/Blog/png/like.png') {
            img.attr('src', '/images/Blog/png/like_pressed.png');

            setTimeout(function () {
                img.attr('src', '/images/Blog/png/like.png');
            }, 2 * 1000); 
        }
    });


    $('.dislike-cover').click(function () {
        event.preventDefault();
    });

}