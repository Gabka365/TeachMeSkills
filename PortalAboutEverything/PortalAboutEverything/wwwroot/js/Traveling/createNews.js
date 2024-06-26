$(document).ready(function () {
    const baseApiUrl = `https://localhost:7032`;
    const btn = $('.traveling-news-btn'); 
    
    btn.click(sendText);

    function sendText() {
        const userId = btn.data('user-id');
        const url = baseApiUrl + "/news/add";
        const text = $('.traveling-news-text-input').val();
        const body ={
            userId: userId,
            text: text
        };

        const promise = $.post(url, body)

        promise.done(function (response) {

            console.log('New News');
            location.reload();
        });

        promise.fail(function (xhr, status, error) {
            console.error(error);
        });
    };
});


