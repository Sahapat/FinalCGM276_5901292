var app = require('express')();
var server = require('http').Server(app);
var io = require('socket.io')(server);

var defualtLobby = 'main';
var lobbys = ['lobby1','lobby2','lobby3','lobby4','lobby5'];
var lobbyHost = ['none','none','none','none','none'];
var playerInlobbys = [0,0,0,0,0];

server.listen(3000, function () {
    console.log("Server runing at port 3000");
})

io.on('connection', function (socket) {
    socket.emit('connect');

    socket.on('login',function(data)
    {
        socket.username = data.name;
        socket.lobby = defualtLobby;
        socket.emit('connected');
        console.log(data.name + " has connected");
    })
    socket.on('hosting',function()
    {
        var lobbyIndex = checkEmptyLobby();
        if(lobbyIndex == -1)
        {
            socket.emit('not hostable');
            console.log("lobby is full");
        }
        else
        {
            socket.leave(socket.lobby);
            socket.join(lobbys[lobbyIndex]);
            playerInlobbys[lobbyIndex] = 1;
            lobbyHost[lobbyIndex] = socket.username;
            socket.emit('hostable');
            console.log(socket.username+" host at "+lobbys[lobbyIndex]);
        }
    })
    socket.on('lobby list',function()
    {
        var resLobbyList = 
        {
            hostName:lobbyHost,
            lobbyCap:playerInlobbys
        }
        socket.emit('update lobby list',resLobbyList);
    })
})
function checkEmptyLobby()
{
    for(var i =0;i<5;i++)
    {
        if(playerInlobbys[i] == 0)
        {
            return i;
        }
    }
    return -1;
}