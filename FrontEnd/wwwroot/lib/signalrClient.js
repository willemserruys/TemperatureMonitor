var connection = new signalR.HubConnectionBuilder()
    .withUrl('http://localhost:5000/temperatureReadingHub')
    .build();
connection.start();

$("#alert").hide(1000);

connection.on('TemperatureReading', (message) => {
    $("#alert").show(1000);
    $("#alert").hide(1000);
});