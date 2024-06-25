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
    const review = `<div class="review">
        <div class="name-and-date">
            <div class="name">${reviewData.userName}</div>
            <div class="date">${reviewData.dateOfCreation}</div>
        </div>
        <p class="text">
            ${reviewData.text}
        </p>
        <div class="update-and-delete">
            <a href="/BoardGameReview/Update?id=${reviewData.id}&gameId=${boardGameId}"><img src="/images/BoardGame/edit.svg" /></a>
            <a href="/BoardGameReview/Delete?id=${reviewData.id}&gameId=${boardGameId}"><img src="/images/BoardGame/delete.svg" /></a>
        </div>
    </div>`

    reviewContainer.insertAdjacentHTML("beforeend", review);
  }
});