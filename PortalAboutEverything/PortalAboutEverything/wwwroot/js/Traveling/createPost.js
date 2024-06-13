
function updateFileName(value) {
    var fileName = value.split('\\').pop();
    var elements = $('.file-name');
    elements.text(fileName ? "Выбран файл: " + fileName : "");
};



