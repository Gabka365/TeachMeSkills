function updateVideos(){
    let protocol = window.location.protocol;
    let host = location.host;
    let updateVideosEndpoint = host + '/VideoLibrary/UpdateLibrary';
    
    fetch(updateVideosEndpoint).then(_ => console.log("Update request sent!"));
}