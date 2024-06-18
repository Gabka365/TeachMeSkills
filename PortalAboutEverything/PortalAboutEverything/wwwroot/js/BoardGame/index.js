document.addEventListener("DOMContentLoaded", function () {

  const boardGames = document.querySelectorAll(".board-game-item");
  hideAllAdminBlocks(boardGames);

  boardGames.forEach((game) => {
    game.addEventListener("click", () => {
      const isCurrentGame = game.classList.contains("active-board-game");

      boardGames.forEach((game) => { game.classList.remove("active-board-game") });

      hideAllAdminBlocks(boardGames);

      if (!isCurrentGame) {
        game.classList.add("active-board-game");
        const adminBlock = game.querySelector(".update-and-delete");
        if (adminBlock) {
          adminBlock.style.display = 'flex';
        }
      }

    });
  });
});

function hideAllAdminBlocks(boardGames) {
  boardGames.forEach((game) => {
    const adminBlock = game.querySelector(".update-and-delete");
    if (adminBlock) {
      adminBlock.style.display = 'none';
    }
  });
}