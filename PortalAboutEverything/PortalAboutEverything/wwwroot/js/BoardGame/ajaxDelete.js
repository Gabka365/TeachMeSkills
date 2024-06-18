document.addEventListener("DOMContentLoaded", function () {
  const deleteButtons = document.querySelectorAll(".delete-link");
  if (!deleteButtons.length) {
    return;
  }

  const debounceDeleteBoardGame = debounce(deleteBoardGame, 500);

  deleteButtons.forEach((deleteButton) => {

    deleteButton.addEventListener("click", () => {

      const deletedGame = deleteButton.closest(".board-game-item");
      const deletedId = deletedGame
        .querySelector(".board-game-id")
        .value;

      debounceDeleteBoardGame.call(null, deletedId, deletedGame)

    });

  });

  function deleteBoardGame(deletedId, deletedGame) {
    const url = `/api/BoardGame/Delete?id=${deletedId}`;
    $.get(url)
      .done((isSuccessful) => {
        if (isSuccessful) {
          deletedGame.remove();
          updateTop();
        } else {
          alert("Ошибка удаления");
        }
      })
      .fail(() => {
        alert("Ошибка сервера");
      });
  }

  function updateTop() {

    const topContainer = document.querySelector(".top-board-games");
    topContainer.innerHTML = "";

    const url = `/api/BoardGame/GetTop3`;
    $.get(url)
      .done((top3FavoriteBoardGames) => {

        for (let i = 0; i < top3FavoriteBoardGames.length; i++) {
          const favoriteBoardGame = top3FavoriteBoardGames[i];
          const topGame =
            `<li class="board-game-item top-board-game-item">
              <input class="top-board-game-id" type="hidden" value="${favoriteBoardGame.id}" />
              <p>${i + 1}</p>
              <a class="board-game-title text-dark top-board-game-title" href="/BoardGame/BoardGame?Id=${favoriteBoardGame.id}">${favoriteBoardGame.title}</a>
              <p class="like-count">${favoriteBoardGame.countOfUserWhoLikeIt}</p>
            </li>`;

          topContainer.insertAdjacentHTML("beforeend", topGame);
        }
      })
  }

});