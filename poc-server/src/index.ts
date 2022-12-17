import express from "express";
import { Server, Socket } from "socket.io";
import http from "http";
import { registerClientEvents } from "./client-events";
import { instrument } from "@socket.io/admin-ui";

const app = express();
const server = http.createServer(app);
const io = new Server(server, { cors: { origin: ["http://localhost:4200", "https://admin.socket.io/"] } });

const connection = (socket: Socket) => {
	registerClientEvents(io, socket);
};

instrument(io, { auth: false });

io.on("connection", connection);

io.on("connection_error", (...params) => {
	console.log(params);
});

server.listen(3000, () => {
	console.log("Listening now");
});
