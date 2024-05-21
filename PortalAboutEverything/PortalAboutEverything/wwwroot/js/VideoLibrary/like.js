function changeVideoLikeState(id, isLiked) {
    let protocol = window.location.protocol;
    let host = location.host;
    let likeEndpoint = protocol + '//' + host + '/VideoLibrary/UpdateVideoLikeState/' + id + '?isLiked=' + isLiked;
    
    fetch(likeEndpoint).then(_ => console.log("Like request sent!"));
}

function reloadPage() {
    location.reload(true);
}