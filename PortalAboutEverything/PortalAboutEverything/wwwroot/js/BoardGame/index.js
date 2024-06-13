document.addEventListener("DOMContentLoaded", function () {
  const boardGames = document.querySelectorAll(".board-game-item");

  boardGames.forEach((game) => {
    game.addEventListener("click", () => {
      const isCurrentGame = game.classList.contains("active");

      boardGames.forEach((game) => { game.classList.remove("active") });

      if (!isCurrentGame) {
        game.classList.add("active");
      }

    });
  });
});