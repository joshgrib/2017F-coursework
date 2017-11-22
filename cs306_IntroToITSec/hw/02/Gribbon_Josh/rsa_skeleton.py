import random

# For the lab, complete modexp(), RSA_enc(), and RSA_dec().
# HW 2 will allow you to submit the fully completed code from the lab,
#   as well as egcd(), mulinv(), and keygen(), for extra credit on the
#   assignment.
# You must work independently since this assignment will be part of HW 2.

# test constants
test_p = 23
test_q = 47
test_e =  35
test_d = 347
message = "Hello world!"

def calc_n(p, q):
    # do not modify!
    return p * q

def calc_phi(p, q):
    # do not modify!
    return (p - 1) * (q - 1)

def modexp(b, e, m):
    # returns b^e % m efficiently
    if m == 1: return 0
    result = 1
    b = b % m
    while( e > 0 ):
        if(e%2 == 1):
            result = (result*b)%m
        e = e >> 1
        b = (b * b) % m
    return result

def RSA_enc(plaintext, key):
    # key should be a tuple (n, e)
    # return a list of integers
    n, e = key
    cipher = [(ord(char) ** e) % n for char in plaintext]
    return cipher

def RSA_dec(ciphertext, key):
    # key should be a tuple (n, e)
    # return a string
    n, e = key
    plaintext = [chr((char ** e) % n) for char in ciphertext]
    return ''.join(plaintext)

def test():
    # do not modify!
    n       = calc_n(test_p, test_q)
    private = [n, test_d]
    public  = [n, test_e]

    print("Public key:",public)
    print("Private key:",private)

    print("Original message:",message)
    ciphertext = RSA_enc(message,public)
    print("Encrypted message:",ciphertext)
    plaintext  = RSA_dec(ciphertext,private)
    print("Decrypted message:",plaintext)

# === Below this comment is the portions of this assignment that contribute to HW 2 ===

def egcd(b, n):
    # runs the extended Euclidean algorithm on b and n
    # returns a triple (g, x, y) s.t. bx + ny = g = gcd(b, n)
    # https://en.wikibooks.org/wiki/Algorithm_Implementation
    #   /Mathematics/Extended_Euclidean_algorithm
    x0, x1, y0, y1 = 1, 0, 0, 1
    while n != 0:
        q, b, n = b // n, n, b % n
        x0, x1 = x1, x0 - q * x1
        y0, y1 = y1, y0 - q * y1
    return  b, x0, y0

def mulinv(e, n):
    # returns the multiplicative inverse of e in n
    g, x, _ = egcd(e, n)
    if g == 1:
        return x % n

def checkprime(n, size):
    # do not modify!
    # determine if a number is prime
    if n % 2 == 0 or n % 3 == 0: return False
    i = 0

    # fermat primality test, complexity ~(log n)^4
    while i < size:
        if modexp(random.randint(1, n - 1), n - 1, n) != 1: return False
        i += 1

    # division primality test
    i = 5
    while i * i <= n:
        if n % i == 0: return False
        i += 2
        if n % i == 0: return False
        i += 4
    return True

def primegen(size):
    # do not modify!
    # generates a <size> digit prime
    if(size == 1): return random.choice([2, 3, 5, 7])
    lower = 10 ** (size - 1)
    upper = 10 ** size - 1
    p = random.randint(lower, upper)
    p -= (p % 6)
    p += 1
    if p < lower: p += 6
    elif p > upper: p -= 6
    q = p - 2
    while p < upper or q > lower:
        if p < upper:
            if checkprime(p, size): return p
            p += 4
        if q > lower:
            if checkprime(q, size): return q
            q -= 4
        if p < upper:
            if checkprime(p, size): return p
            p += 2
        if q > lower:
            if checkprime(q, size): return q
            q -= 2


def keygen(size):
    # generate a random public/private key pair
    # size is the digits in the rsa modulus, approximately. must be even, >2
    # return a tuple of tuples, [[n, e], [n, d]]
    assert(size % 2 == 0 and size > 2) # keep this line!

    p = primegen(size)
    q = primegen(size)
    while p == q: # make sure p != q
        q = primegen(size)

    n = calc_n(p, q)
    phi = calc_phi(p, q)
    e = random.randrange(1, phi)
    d = mulinv(e, phi)

    return ((n, e), (n, d))

def customkeytest(text, size):
    keypair = keygen(size)

    print("Public key:",keypair[0])
    print("Private key:",keypair[1])

    print("Original message:",text)
    ciphertext = RSA_enc(text,keypair[0])
    print("Encrypted message:",ciphertext)
    plaintext  = RSA_dec(ciphertext,keypair[1])
    print("Decrypted message:",plaintext)

if __name__ == "__main__":
    test()
    print "------------------------"
    customkeytest(message, 4)
