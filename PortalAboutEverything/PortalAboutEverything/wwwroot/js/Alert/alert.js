$(document).ready(function () {
    const hub = new signalR.HubConnectionBuilder()
        .withUrl("/hubs/alert")
        .build();

    const alertTemplate = $(`<div class="alert"></div>`);

    hub.on('AlertWasCreatedAsync', cretaeAlert);

    function cretaeAlert(alertId, text) {
        const alertContainer = $('.alert-container');
        alertContainer.show();
        const alert = alertTemplate.clone();
        alert.text(text);
        alertContainer.prepend(alert);

        setTimeout(() => {
            alert.remove();
            if (alertContainer.children().length == 0) {
                alertContainer.hide();
            }
        }, 5 * 1000);

        $.get(`/api/alert/userWasNottified?alertId=${alertId}`);
    }

    hub.start();

    $.get('/api/alert/getAlertWhatIMiss')
        .then(function (alerts) {
            alerts.forEach(function (alert) {
                cretaeAlert(alert.id, alert.text);
            });
        });
});