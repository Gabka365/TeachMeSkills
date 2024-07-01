document.addEventListener('DOMContentLoaded', () => {
    const baseApiUrl = `https://localhost:7219/`;
    const reviewField = document.querySelector('.reviewField');
    const submitBtn = document.querySelector('.submitBtn');

    const hubConnection = new signalR.HubConnectionBuilder()
        .withUrl(baseApiUrl + "hubs/goodReview")
        .build();

    hubConnection.on('NotifyAboutNewReview', (goodId, userName, text) => {
        addNewReview(goodId, userName, text);
    });

    hubConnection.start();

    submitBtn.addEventListener('click', () => {
        sendReview();
    });

    reviewField.addEventListener('keypress', (e) => {
        if (e.key === 'Enter') {            
            e.preventDefault();
            sendReview();
        }
    });

    // Шаблон сообщения
    const reviewTemplate = document.createElement('div');
    reviewTemplate.className = 'goodReview';
    reviewTemplate.innerHTML = '<span class="userName"></span> <span class="text"></span>';

    const sendReview = () => {
        const text = reviewField.value;
        const goodId = document.getElementById('goodId').value;

        fetch('/api/Store/AddReview', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ GoodId: +goodId, Text: text }),
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    hubConnection.invoke('AddNewReview', +goodId, text);
                }
            });
        reviewField.value = "";
    }

    const addNewReview = (goodId, userName, text) => {
        const newReviewBlock = reviewTemplate.cloneNode(true);
        newReviewBlock.querySelector('.userName').textContent = userName;
        newReviewBlock.querySelector('.text').textContent = text;
        const blockGoodReviews = document.querySelector(`.blockGoodReviews[data-id="${goodId}"]`);        
        blockGoodReviews.appendChild(newReviewBlock);
    }
});
