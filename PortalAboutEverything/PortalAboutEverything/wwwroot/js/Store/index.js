import { storage } from "./store.js";

const storeTemplate = {
    theme: document.getElementById("changeTheme"),
    switchInput: document.querySelector(".switchInput"),
    goodNames: document.querySelectorAll(".salor"),
    lastSelectedGood: null,
    deleteBtns: document.querySelectorAll(".deleteGoodLink"),
    likeBtns: document.querySelectorAll(".likedIcon"),

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

    addToFavouriteHandler(event) {
        event.preventDefault();

        const likeBtn = event.target;
        const likedParent = likeBtn.closest(".linkToReview");
        const id = likedParent.getAttribute("data-id");

        fetch('/api/Store/AddToFavourite', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ id: id })
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    if (data.isLiked) {
                        likeBtn.src = '/images/store/header/likedIcon.svg';
                    } else {
                        likeBtn.src = '/images/store/header/unLikedIcon.svg';
                    }
                } else {
                    alert(data.message);
                }
            })
            .catch(error => {
                alert('Произошла ошибка при выполнении запроса: ' + error);
            });

    },

    deleteGoodHandler(event) {
        event.preventDefault();
        const deleteBtn = event.target;
        const parent = deleteBtn.closest('.goodBlock');
        const id = deleteBtn.getAttribute("data-id");
        fetch(`/api/Store/DeleteGood?id=${id}`, {
            method: 'DELETE'
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    parent.remove();
                    alert(data.message);
                } else {
                    alert(data.message);
                }
            })
            .catch(error => {
                alert('Произошла ошибка при выполнении запроса: ' + error);
            });
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

        this.likeBtns.forEach(btn => {
            btn.addEventListener("click", this.addToFavouriteHandler.bind(this));
        });

        this.deleteBtns.forEach(btn => {
            btn.addEventListener("click", this.deleteGoodHandler.bind(this));
        });
    }
}
storeTemplate.init();