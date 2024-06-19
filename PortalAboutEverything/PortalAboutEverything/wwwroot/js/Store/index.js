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

    toggleButtonAndClass(element, isVisible) {
        const parent = element.closest('.goodBlock');
        const button = parent.querySelector(".deleteGoodLink");
        button.style.visibility = isVisible ? 'visible' : 'hidden';
        element.classList.toggle("choosen", isVisible);
    },

    chooseGoodForDeletingHandler(event) {
        const selectedGood = event.target;

        const isAlreadySelected = selectedGood === this.lastSelectedGood;

        this.toggleButtonAndClass(selectedGood, !isAlreadySelected);

        if (isAlreadySelected) {
            this.lastSelectedGood = null;
        } else {
            if (this.lastSelectedGood) {
                this.toggleButtonAndClass(this.lastSelectedGood, false);
            }
            this.lastSelectedGood = selectedGood;
        }
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