document.addEventListener("DOMContentLoaded", function () {
  const hub = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/boardGame")
    .build();
  hub.start();

  const buttonToFavorite = document.querySelector(".favorite-button");
  const gameId = document.querySelector(".game-id").value;
  const textForAdd = document.querySelector(".text-for-add").value;
  const textForRemove = document.querySelector(".text-for-remove").value;

  let isAdd = document.querySelector(".is-add").value == 1;
  let textForButtonAfterClick = !isAdd
    ? textForAdd
    : textForRemove;
  let buttonIsClickable = true;

  buttonToFavorite.addEventListener("click", () => {

    if (buttonIsClickable) {
      buttonIsClickable = false;
      if (isAdd) {
        addOrRemoveOnServer(`/api/BoardGame/AddFavoriteBoardGameForUser?GameId=${gameId}`);
      } else {
        addOrRemoveOnServer(`/api/BoardGame/RemoveFavoriteBoardGameForUser?GameId=${gameId}`);
      }
    } else {
      return;
    }

    // if (isAdd && buttonIsClickable) {
    //   buttonIsClickable = false;
    //   addOrRemoveOnServer(`/api/BoardGame/AddFavoriteBoardGameForUser?GameId=${gameId}`);
    // } else if (!isAdd && buttonIsClickable) {
    //   buttonIsClickable = false;
    //   addOrRemoveOnServer(`/api/BoardGame/RemoveFavoriteBoardGameForUser?GameId=${gameId}`);
    // } else {
    //   return;
    // }
  });

  async function addOrRemoveOnServer(url) {
    await $.get(url)
      .done(() => {
        buttonToFavorite.textContent = textForButtonAfterClick;
        hub.invoke("ChangeFavorites");
        isAdd = !isAdd;
        textForButtonAfterClick = !isAdd
          ? textForAdd
          : textForRemove
      })
      .fail(() => {
        window.location.href = "/Auth/Login";
      });
    buttonIsClickable = true;
  }

});