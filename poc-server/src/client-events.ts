import { Server, Socket } from "socket.io";

interface Update {
	username: string;
	timestamp: Date;
	type: "status" | "message";
}

interface Status extends Update {
	type: "status";
	status: "online" | "offline";
	to: "self" | "others";
}

interface Message extends Update {
	message: string;
	type: "message";
	room?: string;
	to: "self" | "others";
}

export const registerClientEvents = (io: Server, socket: Socket) => {
	const username = socket.handshake.auth.name;
	console.log(socket.handshake);
	console.log(`${username} joined the chatroom`);

	let status: Status = {
		status: "online",
		username,
		timestamp: new Date(),
		type: "status",
		to: "others",
	};

	socket.broadcast.emit("status", status);

	socket.on("send-message", (message: string, success: (message: Message) => void) => {
		const messageObj: Message = {
			message,
			username,
			timestamp: new Date(),
			type: "message",
			to: "others",
		};
		success(messageObj);
		socket.broadcast.emit("recieve-message", messageObj);

		console.log(`${username}: ${message}`);
	});

	socket.on("join-room", (roomName: string) => {
		socket.join(roomName);
		socket.in(roomName).emit("status", `${username} joined ${roomName}`);
	});

	socket.on("disconnect", (params) => {
		const status: Status = {
			status: "offline",
			username,
			timestamp: new Date(),
			type: "status",
			to: "others",
		};

		io.emit("status", status);
		console.log(`${username} left the chat`);
	});
};
