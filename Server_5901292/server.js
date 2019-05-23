var app = require('express')();
var server = require('http').Server(app);
var io = require('socket.io')(server);

var lobbys = ['lobby1','lobby2','lobby3','lobby4','lobby5'];
var playerInlobbys = [0,0,0,0,0];

server.listen(3000, function () {
    console.log("Server runing at port 3000");
})

io.on('connection', function (socket) {
    socket.emit('connect');

    socket.on('login',function(data)
    {
        socket.username = data.name;
        socket.emit('connected');
        console.log(data.name + " has connected");
    })
    socket.on('hosting',function()
    {
        var lobbyIndex = checkEmptyLobby();
        if(lobbyIndex == -1)
        {
            socket.emit('not hostable');
        }
        else
        {
            socket.emit('hostable');
            console.log(socket.username+" host at "+lobbys[lobbyIndex]);
        }
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