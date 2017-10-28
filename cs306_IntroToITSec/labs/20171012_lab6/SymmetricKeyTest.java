import java.security.Key;
import java.security.SecureRandom;

import javax.crypto.Cipher;
import javax.crypto.KeyGenerator;
import javax.crypto.spec.IvParameterSpec;
//
// encrypt and decrypt using the DES private key algorithm
public class SymmetricKeyTest {

	public static void main (String[] args) throws Exception {

		// error checking
		if (args.length < 5) {
			System.err.println("Usage: java PrivateExample <algorithm> <key size> <mode of operation> <iterations> <message>");
			System.exit(1);
		}
        String arg_algorithm = args[0];
        String arg_key_size = args[1];
        String arg_mode = args[2];
        String arg_iters = args[3];
        String arg_message = args[4];

		byte[] plainText = arg_message.getBytes("UTF8");

		// get a cipher object and print the provider
		Cipher cipher = Cipher.getInstance(arg_algorithm+"/"+arg_mode+"/PKCS5Padding");
		System.out.println(cipher.getProvider().getInfo() );

		// generate IV for CBC mode
		SecureRandom rand = new SecureRandom();
		System.out.println("Generating IV using "+rand.getAlgorithm());
		byte[] iv = new byte[cipher.getBlockSize()];
		rand.nextBytes(iv);
		IvParameterSpec ivp = new IvParameterSpec(iv);

		// get a private key
		System.out.println( "Generating "+ arg_algorithm +" key ("+arg_key_size+" bits)" );
		KeyGenerator keyGen = KeyGenerator.getInstance(arg_algorithm);
		keyGen.init(Integer.parseInt(arg_key_size));
		Key key = keyGen.generateKey();

		long avgEnc=0, avgDec=0, start=0;
		byte[] cipherText = null, newPlainText=null;
		int iterations = Integer.parseInt(arg_iters);

		for(int i = 0;i<iterations;i++){
			start = System.nanoTime();

			// encrypt using the key and the plaintext
			if(arg_mode.equalsIgnoreCase("ecb")) cipher.init(Cipher.ENCRYPT_MODE, key);
			else cipher.init(Cipher.ENCRYPT_MODE, key, ivp);
			cipherText = cipher.doFinal(plainText);

			avgEnc+=(System.nanoTime()-start);
			start = System.nanoTime();

			// decrypt the ciphertext using the same key
			if(arg_mode.equalsIgnoreCase("ecb")) cipher.init(Cipher.DECRYPT_MODE, key);
			else cipher.init(Cipher.DECRYPT_MODE, key, ivp);
			newPlainText = cipher.doFinal(cipherText);

			avgDec+=(System.nanoTime()-start);
		}

		System.out.println("\nSimulation run " + iterations + " times");
		System.out.println("Average encryption time: " + (avgEnc / (iterations*10e3))+" microseconds");
		System.out.println("Average decryption time: " + (avgDec / (iterations*10e3))+" microseconds");

		System.out.println("\nInput message: " +arg_message);
		System.out.println("Encrypted message: " + new String(cipherText, "UTF8"));
		System.out.println("Decrypted message: " + new String(newPlainText, "UTF8"));

	}
}
