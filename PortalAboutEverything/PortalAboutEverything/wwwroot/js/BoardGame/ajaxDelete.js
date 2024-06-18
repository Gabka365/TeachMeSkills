document.addEventListener("DOMContentLoaded", function () {
  const deleteButtons = document.querySelectorAll(".delete-link");

  deleteButtons.forEach((deleteButton) => {

    deleteButton.addEventListener("click", () => {

      const deletedGame = deleteButton.closest(".board-game-item");
      const deletedId = deletedGame
        .querySelector(".board-game-id")
        .value;

      const url = `api/BoardGame/Delete?id=${deletedId}`;
      $.get(url)
        .done(() => {
          deletedGame.remove();
        })

    });

  });
});