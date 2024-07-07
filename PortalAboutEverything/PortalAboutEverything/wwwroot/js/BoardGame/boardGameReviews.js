document.addEventListener("DOMContentLoaded", function () {
  const baseApiUrl = `https://localhost:7289/`;
  const boardGameId = document.querySelector(".game-id").value;
  const reviewContainer = document.querySelector(".reviews-container");
  const absenceOfReviewsText = document.querySelector(".absence-of-reviews-text").value;

  init();

  function init() {
    reviewContainer.innerHTML = "";
    $.get(baseApiUrl + `getAll?gameId=${boardGameId}`)
      .done(function (reviews) {
        if (reviews.length === 0) {
          reviewContainer.insertAdjacentHTML("beforeend", `<p>${absenceOfReviewsText}</p>`);
        } else {
          reviews.forEach(review => {
            addReview(review);
          });
        }
      });
  }

  function addReview(reviewData) {
    const dateOfCreationInStringFormat = new Date(reviewData.dateOfCreation).toLocaleString('en-GB', {
      day: '2-digit',
      month: '2-digit',
      year: 'numeric',
      hour: '2-digit',
      minute: '2-digit',
      hour12: false
    })
      .replace(',', '')
      .replaceAll('/', '.');

    const review = `<div class="review" id="review-${reviewData.id}">
        <div class="name-and-date">
            <div class="name">${reviewData.userName}</div>
            <div class="date">${dateOfCreationInStringFormat}</div>
        </div>
        <p class="text">
            ${reviewData.text}
        </p>
        <div class="update-and-delete">
            <a class="update-button" href="/BoardGameReview/Update?id=${reviewData.id}&gameId=${boardGameId}"><img src="/images/BoardGame/edit.svg" /></a>
            <a class="delete-button" ><img src="/images/BoardGame/delete.svg" /></a>
        </div>
    </div>`

    reviewContainer.insertAdjacentHTML("beforeend", review);

    const reviewForDelete = reviewContainer.querySelector(`#review-${reviewData.id}`);
    const deleteButton = reviewForDelete.querySelector(".delete-button");
    deleteButton.addEventListener("click", () => {
      $.get(baseApiUrl + `delete?id=${reviewData.id}`)
        .done(() => {
          reviewForDelete.remove();
          if(reviewContainer.querySelector(".review")){
            return;
          }
          reviewContainer.insertAdjacentHTML("beforeend", `<p>${absenceOfReviewsText}</p>`);
        });
    });
  }
});