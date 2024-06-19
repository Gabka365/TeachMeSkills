import { storage } from "./service.js";

const storeTemplate = {
       theme: document.getElementById("changeTheme"),
       switchInput: document.querySelector(".switchInput"),
       goodNames: document.querySelectorAll(".salor"),
       lastSelectedGood: null,
       deleteBtn: document.querySelectorAll(".deleteGoodLink"),

       changeThemeHandler() {
              const wrapper = document.querySelector(".storeWrapper");
              const currentTheme = wrapper.getAttribute("data-theme");
              const newTheme = currentTheme === "dark" ? "light" : "dark";

              wrapper.setAttribute("data-theme", newTheme);
              storage.setTheme(newTheme);
       },

       chooseGoodForDeletingHandler(event) {
              const selectedGood = event.target;

              if (this.lastSelectedGood) {
                     this.lastSelectedGood.classList.remove("choosen");
                     const parentOfLastSelectedGood = this.lastSelectedGood.closest('.goodBlock');
                     const visibleButton = parentOfLastSelectedGood.querySelector(".deleteGoodLink");
                     visibleButton.style.visibility = 'hidden';
              }

              selectedGood.classList.add("choosen");
              this.lastSelectedGood = selectedGood;

              const parentOfLastSelectedGood = selectedGood.closest('.goodBlock');
              const visibleButton = parentOfLastSelectedGood.querySelector(".deleteGoodLink");
              visibleButton.style.visibility = 'visible';
       },

       init() {
              const themeFromStorage = storage.getTheme();
              const wrapper = document.querySelector(".storeWrapper");
              wrapper.setAttribute("data-theme", themeFromStorage);

              this.switchInput.checked = themeFromStorage === 'light';

              this.theme.addEventListener("click", this.changeThemeHandler.bind(this));

              this.goodNames.forEach(goodName => {
                     goodName.addEventListener("click", this.chooseGoodForDeletingHandler.bind(this));
              });
       },
}
storeTemplate.init();







