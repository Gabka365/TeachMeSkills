$(document).ready(function () {
    const baseApiUrl = `https://localhost:7032`;
    const btn = $('.traveling-news-btn');
    const userId = btn.data('user-id');
    const text = $('.traveling-news-text');
    const url = baseApiUrl + "/news/add";

    text.on('input', function () {
        const inputText = text.val();
        btn.prop('disabled', inputText.length < 20);       
    });
   
    btn.click(sendText);

    function sendText() {
        const inputText = text.val();
        const body ={
            userId: userId,
            text: inputText
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


