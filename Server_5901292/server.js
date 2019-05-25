var app = require('express')();
var server = require('http').Server(app);
var io = require('socket.io')(server);

var defualtLobby = 'main';
var lobbys = ['lobby1', 'lobby2', 'lobby3', 'lobby4', 'lobby5'];
var lobbyHost = ['empty', 'empty', 'empty', 'empty', 'empty'];
var lobbyClient = ['empty', 'empty', 'empty', 'empty', 'empty'];
var lobbyDatas = [false, false, false, false, false, false, false, false, false, false]
var playerInlobbys = [0, 0, 0, 0, 0];

server.listen(3000, function () {
    console.log("Server runing at port 3000");
})

io.on('connection', function (socket) {
    socket.emit('connect');

    socket.on('login', function (data) {
        socket.username = data.name;
        socket.lobby = defualtLobby;
        socket.emit('connected');
        console.log(data.name + " has connected");
    })
    socket.on('hosting', function () {
        var lobbyIndex = checkEmptyLobby();
        if (lobbyIndex == -1) {
            socket.emit('not hostable');
            console.log("lobby is full");
        }
        else {
            socket.leave(socket.lobby);
            socket.join(lobbys[lobbyIndex]);
            playerInlobbys[lobbyIndex] = 1;
            lobbyHost[lobbyIndex] = socket.username;
            var resLobby =
            {
                hostName: lobbyHost[lobbyIndex],
                indexLobby: lobbyIndex,
                isReady: lobbyDatas[getLobbyDataByIndex(lobbyIndex, true)]
            }
            socket.emit('hostable', resLobby);
            var resLobbyList =
            {
                hostNames: lobbyHost,
                lobbyCap: playerInlobbys
            }
            socket.broadcast.emit('update lobby list', resLobbyList);
            console.log(socket.username + " host at " + lobbys[lobbyIndex]);
        }
    })
    socket.on('lobby list', function () {
        var resLobbyList =
        {
            hostNames: lobbyHost,
            lobbyCap: playerInlobbys
        }
        socket.emit('update lobby list', resLobbyList);
        console.log("update lobby list");
    })
    socket.on('join lobby', function (data) {
        if (playerInlobbys[data] <= 1) {
            var resLobby =
            {
                hostName: lobbyHost[data],
                indexLobby: data,
                isReady: lobbyDatas[getLobbyDataByIndex(data, true)]
            }
            socket.emit('joinable', resLobby);
            socket.leave(socket.lobby);
            socket.join(lobbys[data]);
            playerInlobbys[data] += 1;
            lobbyClient[data] = socket.username;
        }
        else {
            socket.emit('not joinable');
        }
    })
    socket.on('update lobby', function (data) {
        console.log(socket.username + "has join " + socket.lobby);
        var resSync =
        {
            hostName: socket.username,
            indexLobby: data,
            isReady: lobbyDatas[getLobbyDataByIndex(data, false)]
        }
        socket.broadcast.to(lobbys[data]).emit('sync lobby', resSync);
        socket.emit('sync lobby', resSync);
    })
    socket.on('ready press', function (data) {
        console.log(socket.username + " has " + data.isReady);
        var isHost = (socket.username == lobbyHost[data.indexLobby]);
        lobbyDatas[getLobbyDataByIndex(data.indexLobby, isHost)] = data.isReady;

        var temp = 0;
        if (data.isReady) {
            temp = 1;
        }
        else {
            temp = 0;
        }

        var resBool = {
            isHost: isHost,
            isReady: temp
        }

        socket.broadcast.to(lobbys[data.indexLobby]).emit('sync ready press', resBool);

        if (checkReady(data.indexLobby)) {
            var resToClient =
                {
                    hostName: lobbyHost[data.indexLobby],
                    indexLobby: -1,
                    isReady: false
                }
                var resToHost =
                {
                    hostName:lobbyClient[data.indexLobby],
                    indexLobby:-1,
                    isReady:false
                }
            if (isHost) {
                socket.emit('countdown',resToHost);
                socket.broadcast.to(lobbys[data.indexLobby]).emit('countdown',resToClient);
            }
            else
            {
                socket.emit('countdown',resToClient);
                socket.broadcast.to(lobbys[data.indexLobby]).emit('countdown',resToHost);
            }
            socket.emit('countdown');
            socket.broadcast.to(lobbys[data.indexLobby]).emit('countdown');
            setTimeout(function () {
                socket.emit('game start');
                socket.broadcast.to(lobbys[data.indexLobby]).emit('game start');
            }, 6000);
        }
    })
    socket.on('fire',function(data)
    {
        var resToOther = 
        {
            rotationZ:data.rotationZ,
            isHost:data.isHost,
            lobbyIndex:data.lobbyIndex
        }
        console.log(data.lobbyIndex +" firing");
        socket.broadcast.to(lobbys[data.lobbyIndex]).emit('other firing',resToOther);
    })
    socket.on('take damage',function(data)
    {
        var resToOther = 
        {
            currentHealth:data.currentHealth,
            isHost:data.isHost,
            lobbyIndex:data.lobbyIndex
        }
        console.log("take damage "+data.isHost);
        socket.broadcast.to(lobbys[data.lobbyIndex]).emit('other take damage',resToOther);
    })
    socket.on('sent winner',function(data)
    {
        var resToOther =
        {
            name:data.name,
            isHost:data.isHost,
            lobbyIndex:data.lobbyIndex
        }
        console.log("game end");
        socket.emit('game end',resToOther);
        socket.broadcast.to(lobbys[data.lobbyIndex]).emit('game end',resToOther);
    })
})
function checkEmptyLobby() {
    for (var i = 0; i < 5; i++) {
        if (playerInlobbys[i] == 0) {
            return i;
        }
    }
    return -1;
}
function getLobbyDataByIndex(index, isHost) {
    if (isHost) {
        if (index == 0) {
            return 0;
        }
        else {
            return index + index;
        }
    }
    else {
        return index + (index + 1);
    }
}
function checkReady(index) {
    var p1 = lobbyDatas[getLobbyDataByIndex(index, true)];
    var p2 = lobbyDatas[getLobbyDataByIndex(index, false)];

    return p1 && p2;
}