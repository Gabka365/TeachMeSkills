import { storage } from "./service.js";

const theme = document.getElementById("changeTheme");
const inputTheme = document.querySelector(".switchInput");
console.log("theme");

const todosTemplate = {

  changeThemeHandler() {
    const themeName = document.body.getAttribute("data-theme");
    if (themeName !== "dark") {
      document.body.setAttribute("data-theme", "dark");
      storage.setTheme("dark")
    } else {
      document.body.setAttribute("data-theme", "light")
      storage.setTheme("light")
    }
  },

  init() {

    const themeFromStorage = storage.getTheme()
    document.body.setAttribute("data-theme", themeFromStorage);
    inputTheme.checked = themeFromStorage === 'light';

    theme.addEventListener("click", this.changeThemeHandler)

  },
}
todosTemplate.init();