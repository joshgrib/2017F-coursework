import java.net.*;
import java.io.*;

public class EchoClient {
	public static void main(String[] args) {
		try {

			int port = 30000;
			Socket sock = new Socket("10.0.2.15", port);

			BufferedReader instream = new BufferedReader(new InputStreamReader(sock.getInputStream()));
			BufferedWriter outstream = new BufferedWriter(new OutputStreamWriter(sock.getOutputStream()));

			String request = "Hello\n";
			System.out.println("Sending Messages to the Server... :" + request);
			System.out.println("Connecting to " + sock.getInetAddress() + " and port " + sock.getPort());
			System.out.println("Local Address :" + sock.getLocalAddress() + " Port :" + sock.getLocalPort());

			// write a message
			outstream.write(request);
			outstream.flush();

			String response;
			System.out.println("The server says: ");
			// read data
			response = instream.readLine();
			System.out.println(response);

			// write your code

			instream.close();
			outstream.close();
			sock.close();
			System.out.println("Connection Closing...");
		} catch (IOException ex) {
			System.out.println("Error during I/O");
			ex.getMessage();
			ex.printStackTrace();
		}
	}
}
