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

  buttonToFavorite.addEventListener("click", () => {

    if (isAdd) {
      AddOrRemoveOnServer(`/api/BoardGame/AddFavoriteBoardGameForUser?GameId=${gameId}`)
    } else {
      AddOrRemoveOnServer(`/api/BoardGame/RemoveFavoriteBoardGameForUser?GameId=${gameId}`)
    }
  });

  function AddOrRemoveOnServer(url) {
    $.get(url)
      .done(() => {
        buttonToFavorite.textContent = textForButtonAfterClick;
        hub.invoke("ChangeFavorites");
        isAdd = !isAdd;
        textForButtonAfterClick = !isAdd
          ? textForAdd
          : textForRemove
      })
      .fail(() => {
        alert("Ошибка сервера");
      });
  }

});