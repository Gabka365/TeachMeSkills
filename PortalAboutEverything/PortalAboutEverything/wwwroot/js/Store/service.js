export const storage = {
    setTheme(theme) {
        localStorage.setItem("theme", theme);
    },
    getTheme() {
        return localStorage.getItem("theme");
    },
};