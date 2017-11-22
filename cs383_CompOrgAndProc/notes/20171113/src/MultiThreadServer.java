import java.io.*;
//import java.net.*;
//import java.util.Date;
import java.net.ServerSocket;
import java.net.Socket;

public class MultiThreadServer implements Runnable {
	Socket csocket;

	MultiThreadServer(Socket csocket) {
		this.csocket = csocket;
	}

	public static void main(String args[]) throws Exception {
		ServerSocket ssock = new ServerSocket(8080);
		System.out.println("Listening");
		try {
			while (true) {
				Socket sock = ssock.accept();
				System.out.println("Connected");
				new Thread(new MultiThreadServer(sock)).start();
			}
		} finally {
			ssock.close();
		}
	}

	public void run() {
		try {

			BufferedReader instream = new BufferedReader(new InputStreamReader(csocket.getInputStream()));
			BufferedWriter outstream = new BufferedWriter(new OutputStreamWriter(csocket.getOutputStream()));

			// String strin = instream.readLine();
			String strin;
			while ((strin = instream.readLine()) != null) {
				if (strin.equals("Hi")) {
					outstream.write("Hi");

				} else {

					System.out.println(strin);
				}

				if (strin.equals("Bye."))
					break;
			}

			// csocket.close();
		} catch (IOException e) {
			System.out.println(e);
		}
	}

}
