function goToRandomVideo(isLiked) {
    let protocol = window.location.protocol;
    let host = location.host;
    let randomVideoEndpoint = protocol + '//' + host + '/VideoLibrary/GetRandomVideoId?isLiked=' + isLiked;
    let randomVideoPlayerEndpoint = protocol + '//' + host + '/VideoLibrary/Player/';

    fetch(randomVideoEndpoint)
        .then(response => response.text())
        .then(videoId => window.location.href = randomVideoPlayerEndpoint + videoId);
}

function goToFirstVideo(id) {
    let protocol = window.location.protocol;
    let host = location.host;
    window.location.href = protocol + '//' + host + '/VideoLibrary/Player/' + id;
}

function deleteVideo(id) {
    let protocol = window.location.protocol;
    let host = location.host;
    let deleteVideoEndpoint = protocol + '//' + host + '/VideoLibrary/DeleteVideo/' + id;
    
    fetch(deleteVideoEndpoint).then(_ => console.log("Delete request sent!"));
}