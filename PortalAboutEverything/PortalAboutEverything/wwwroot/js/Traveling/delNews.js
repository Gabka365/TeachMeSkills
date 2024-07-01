$(document).ready(function () {
    const baseApiUrl = `https://localhost:7032`;
    const btnDel = $('.traveling-news-btn-del');

    if (btnDel.length > 0) {
        btnDel.click(del);
    }
    function del() {
        const newsId = btnDel.data('news-id');
        const url = baseApiUrl + "/news/del";
        const newsText = $('.traveling-news-text').text();
        const body = {
            newsId: newsId,
            text: newsText
        };
        const promise = $.ajax({
            url: url,
            method: "DELETE",
            data: body,
        });
        
        promise.done(function (response) {
            console.log('News deleted');
            location.reload();
        });

        promise.fail(function (xhr, status, error) {
            console.error(error);
        })
    }
});


