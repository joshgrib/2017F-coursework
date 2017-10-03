**Announcements**
* Midterm 10/17 during regular class time
    * Covering all topics we cover in class up to the midterm
* No class next week - Monday schedule

# Public Key Cryptography
**Priciples of modern security**
* Security definitions
* Precise assumptions
* Formal proofs

## Motivation
* Secret key cryptography requires a secure channel to share the key initially, which isn't always the case
    * Additionally to use shared secret keys and not repeat it requires too many keys, and all the keys need to be protected to maintain security

## Public key Cryptography
* User generates two keys, one which is publicly known, and one that is private and only known to the user
* Public key is used to encrypt messages to the user, private key is used by the user to decrypt messages
* Assumes that there is a secure way to distribute public keys
* **Must** be CPA secure because public keys are public, so anyone can encrypt any message
