# Public-Key Crypto

**Announcements**
* Labs will continue to be the quiz-based format
* HW 1 does out next Friday and will be due 2 weeks after on the Sunday

## Intro to modern cryptography
**Systematic process**
* **Formal definitions**
    * Why? Successful project management, provable security, qualitative analysis/ modular design
* **Precise assumptions**
    * What is our view of the world *exactly*?
    * Adversary - What are the threats, capabilities, and limitations of attacks? (e.g eavesdropping means the attacker can't modify the message)
    * Computation - some problems are hard enough that modern computers can't solve them efficiently
    * Setting - system definitions, communication lines, etc
    * Why does it matter? Provides a basis for proofs of security, a comparison between solutions, and flexibility in design and analysis
    * Example: eavesdropping involves one party using a supposedly secret key to encrypt, then someone can view the ciphertext only, and wants to figure out the plaintext, with no previous ciphertexts or plaintexts.
        * Maybe they have a previous ciphertext-plaintext pair - that opens many new vulnerabilities for the next message sent
        * Maybe they have a collection of pairs for plaintext chosen by the attacker designed to find patterns
* **Provable security**
    * We want a proof that shows security will hold in all possible circumstances

## Issues with the one-time pad
* Even tough it has "perfect security":
    * The key has to be as long as the message
    * Keys can *never* be reused
        * The NSA decrypted soviet messages during the Cold War because they reused keys in the one-time pad

## Big picture
* We formally defined and constructed a perfectly secure cipher
* It has some drawbacks - large key
* Claude Shannon proved that such limitations are unavoidable

### New approach
* We redefine security to relax it a bit, creating **computational security**
    * A tiny amount of information can be leaked without compromising security
    * We bound the computational power instead of assuming it's infinite (e.g. if it takes 200yrs to break it should be good)

## Computational security
* This is notably different from information-theoretic security
* This is the default way security is modeled in most crypto settings
* Still allows for rigorous proofs
* Two relaxations:
    1. Security is guaranteed against modern efficient attackers, not all possible attackers
    2. Success of attack is possible, but probabilistic

### The Concrete Approach
* Bound maximum success probability for any adversary with a specified amount of time and resources
* We need to define what it means for someone to "break" the scheme, as well as precisely what the machine requirements would be for it to happen
* This involved considering what exactly we want to protect against - is it anyone with a laptop or a government with supercomputers?

### The Asymptotic Approach
* Scheme and parties are parameterized by an integer n (e.g. key length)
    * Security parameter is known by everyone and all involved quantities are functions of n
    * Adversaries are equated with probabilistic poly-time(PPT) algorithms that run for time that's a polynomial of n

## Classic ciphers
* Class of ciphers are based on letter substitutions
    * E.g. *Caesar's cipher* involved shifting all characters a certain number of positions
    * Encryption involved mapping all characters to other characters, decryption is undoing the mapping
* **Shift cipher** - shift each character by k positions, where key is randomly chosen from [0,25]
    * The key space is too small, exhaustive search can easily break it
* **Mono-alphabetic substitution cipher** - key space defines a permutation on the alphabet (e.g. swap every pair of characters BADCFE...)
    * Key space is large (26! or ~2^88), but the character mapping is fixed, so you can use character frequency statistics to figure out what the mappings likely are (e.g. most common character probably to e)
* **Poly-alphabetic shift cipher** - key is a block that is applied repeatedly to blocks of the message
    * This prevents the same character from consistently mapping to any ciphertext character, creating a many-to-many mapping that depends on the block location in the plaintext and the character location in the block
    * This is still vulnerable even if the key length is unknown by looking for patterns with 2 and 3 letter words

## Symmetric-key encryption in practice
* **Stream ciphers** have an optional key and will continuously encrypt a stream
* **Block ciphers** have an optional key and will encrypt a stream by breaking it into blocks

### Perfect encryption of a block
* **Goal**: encrypt a block of n bits using the same key all the time, without having the problem of the one-time pad
* **Approach**: encrypt via bijective random mapping T
    * Pairs computed uniformly at random
    * Key uniquely identifies a random mapping

### Primitive techniques
* **Substitution** - exchange one set of bits for another
* **Transposition** - rearrange order of ciphertext bits to break regularity and patterns
* **Confusion** - complex functional relations, changes to the plaintext don't make predictable changes to the ciphertext
* **Diffusion** - distribute info from plaintext broadly across the ciphertext, so a change to a small area of the plaintext will make changes across the whole ciphertext

### DES: Data Encryption Standard
* Symmetric block cipher developed in 1976 by IBM and US National Institute of Standards(NIST)
* No longer secure
* Repeatedly uses substitutions and transpositions 16 times (16 rounds)
* Block size 64 bits
* Key size 56 bits, used to create a new 48 bit key for each round

### EAS: Advanced Encryption System
* Developed in 1999 by Dutch cryptographers after NIST asked public for a replacement for DES
* Still considered secure
* Same principles as DES but with better design
* Uses substitution, confusion, and diffusion
* Block size 128 bites
* Key size 128, 192, or 156; used for 10, 12, or 14 rounds
    * These numbers can be higher if needed

### Block cipher modes
* **Electronic Code Book(ECB)**: split message into blocks and apply the block cipher to each
    * Simple
    * Allows for parallel processing of blocks
    * Weakness: same plaintext creates same ciphertext
* **Cipher Block Chaining(CBC)**: the resulting ciphertext of a block is used to encrypt the next block in the chain
    *
