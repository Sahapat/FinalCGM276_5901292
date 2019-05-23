var app = require('express')();
var server = require('http').Server(app);
var io = require('socket.io')(server);

var usernames = {};

server.listen(3000, function () {
    console.log("Server runing at port 3000");
})

io.on('connection', function (socket) {
    socket.emit('connect');

    socket.on('login',function(data)
    {
        socket.username = data.name;
        usernames[data.name] = data.name;
        socket.emit('connected');
        console.log(data.name + " has connected");
    })

    socket.on('disconnect',function()
    {
        delete usernames[socket.username];
        io.sockets.emit('update user',socket.username);
    })
})