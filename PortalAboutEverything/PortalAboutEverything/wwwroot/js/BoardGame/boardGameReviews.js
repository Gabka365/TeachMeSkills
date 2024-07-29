document.addEventListener("DOMContentLoaded", function () {
  const baseApiUrl = `https://localhost:7289/`;
  const boardGameId = document.querySelector(".game-id").value;
  const reviewContainer = document.querySelector(".reviews-container");

  const absenceOfReviewsText = document.querySelector(".absence-of-reviews-text").value;
  const uploadingReviewsText = document.querySelector(".uploading-reviews-text").value;

  const currentUserId = document.querySelector(".current-user-id").value - 0;
  const isModerator = document.querySelector(".is-moderator").value;

  init();

  function init() {
    reviewContainer.innerHTML = uploadingReviewsText;
    $.get(baseApiUrl + `getAll?gameId=${boardGameId}`)
      .done(function (reviews) {
        if (reviews.length === 0) {
          reviewContainer.innerHTML = "";
          reviewContainer.insertAdjacentHTML("beforeend", `<p>${absenceOfReviewsText}</p>`);
        } else {
          reviewContainer.innerHTML = "";
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

    let updateButtonHtml = ''
    if (currentUserId === reviewData.userId) {
      updateButtonHtml = `<a class="update-button" href="/BoardGameReview/Update?id=${reviewData.id}&gameId=${boardGameId}"><img src="/images/BoardGame/edit.svg" /></a>`
    }

    let deleteButtonHtml = ''
    if (currentUserId === reviewData.userId || isModerator) {
      deleteButtonHtml = `<a class="delete-button" ><img src="/images/BoardGame/delete.svg" /></a>`
    }

    const review = `<div class="review" id="review-${reviewData.id}">
        <div class="name-and-date">
            <div class="name">${reviewData.userName}</div>
            <div class="date">${dateOfCreationInStringFormat}</div>
        </div>
        <p class="text">
            ${reviewData.text}
        </p>
        <div class="update-and-delete">
            ${updateButtonHtml}
            ${deleteButtonHtml}
        </div>
    </div>`

    reviewContainer.insertAdjacentHTML("beforeend", review);

    let deleteButtonIsClickable = true;
    const reviewForDelete = reviewContainer.querySelector(`#review-${reviewData.id}`);
    const deleteButton = reviewForDelete.querySelector(".delete-button");
    if (deleteButton) {
      deleteButton.addEventListener("click", async function() {
        if (deleteButtonIsClickable) {
          deleteButtonIsClickable = false;
          const url = `/api/BoardGameReview/Delete?id=${reviewData.id}`;
          await $.get(url)
            .done((successfully) => {
              if (successfully) {
                reviewForDelete.remove();
                if (reviewContainer.querySelector(".review")) {
                  return;
                }
                reviewContainer.insertAdjacentHTML("beforeend", `<p>${absenceOfReviewsText}</p>`);
              } else {
                window.location.href = "/Auth/AccessDenied";
              }
            })
            .fail(() => {
              window.location.href = "/Auth/Login";
            });
            deleteButtonIsClickable = true;
        } else {
          return;
        }
      });
    }
  }
});