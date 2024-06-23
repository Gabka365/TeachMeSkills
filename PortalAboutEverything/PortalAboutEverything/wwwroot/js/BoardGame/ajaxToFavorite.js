document.addEventListener("DOMContentLoaded", function () {
  const buttonToFavorite = document.querySelector(".favorite-button");
  const gameId = document.querySelector(".game-id").value;
  const textForAdd = document.querySelector(".text-for-add").value;
  const textForRemove = document.querySelector(".text-for-remove").value;

  let isAdd = document.querySelector(".is-add").value == 1;
  let textForButtonAfterClick = isAdd
    ? textForAdd
    : textForRemove;

  buttonToFavorite.addEventListener("click", () => {

    if (isAdd) {
      AddOrRemoveOnServer(`/api/BoardGame/AddFavoriteBoardGameForUser?GameId=${gameId}`);
      textForButtonAfterClick = textForRemove;
    } else {
      AddOrRemoveOnServer(`/api/BoardGame/RemoveFavoriteBoardGameForUser?GameId=${gameId}`);
      textForButtonAfterClick = textForAdd;
    }
  });

  function AddOrRemoveOnServer(url) {
    $.get(url)
      .done(() => {
        buttonToFavorite.textContent = textForButtonAfterClick;
        isAdd = !isAdd;
      })
      .fail(() => {
        alert("Ошибка сервера");
      });
  }

});