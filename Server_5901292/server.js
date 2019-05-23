var app = require('express')();
var server = require('http').Server(app);
var io = require('socket.io')(server);

var playerName = [];
var numPlayer = 0;

server.listen(3000, function () {
    console.log("Server runing at port 3000");
})

io.on('connection', function (socket) {
    socket.emit('login require');

    socket.on('login', function (data) {
        if (numPlayer <= 1) {
            playerName.push(data.name);
            numPlayer += 1;

            var resLogin =
            {
                name: data.name,
                characterId: numPlayer-1
            }

            console.log("player: "+data.name+" has connected");
            socket.broadcast.emit('otherPlayerConnected', resLogin);
            socket.emit('connected', resLogin);

            if(numPlayer >= 2)
            {
                socket.broadcast.emit('game start');
                socket.emit('game start');
            }
        }
    })
})