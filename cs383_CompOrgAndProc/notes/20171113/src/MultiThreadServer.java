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
		ServerSocket ssock = new ServerSocket(30000);
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
			boolean waiting = true;

			while ((strin = instream.readLine()) != null) {
				System.out.println(strin);
				if(waiting){
					// AWAIT
					if(strin.equals("Hello")){
						outstream.write("Welcome");
						waiting = false;
						outstream.flush();
					}else{
						outstream.write("Not Welcomed");
						outstream.flush();
					}
				}else {
					// ECHO
					if(strin.equals("bye")){
						outstream.write(strin);
						waiting = true;
						outstream.flush();
						break;
					}else{
						outstream.write(strin);
						outstream.flush();
					}
				}
			}
			csocket.close();
		} catch (IOException e) {
			System.out.println(e);
		}
	}

}
