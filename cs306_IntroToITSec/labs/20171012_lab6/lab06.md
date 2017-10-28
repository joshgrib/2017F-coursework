# Lab 06 - 20171012

## Data Gathering
### 1
| Algorithm | Key size | Mode of operation | Iterations | Message          | Enc Time (microseconds) | Dec Time (microseconds) |
| :--  | :-- | :-- | :-- | :-- | :-- | :-- |
| DES       | 56  | ECB | 100000 | Lab 6 is awesome | 0.111978529 | 0.109813061 |
| TripleDES | 112 | ECB | 100000 | Lab 6 is awesome | 0.231052413 | 0.236359227 |
| RC2       | 40  | ECB | 100000 | Lab 6 is awesome | 0.153221524 | 0.130169897 |
| AES       | 128 | ECB | 100000 | Lab 6 is awesome | 0.039915166 | 0.048997299 |
| Blowfish  | 32  | ECB | 100000 | Lab 6 is awesome | 3.674158452 | 3.673577337 |

### 2
| Algorithm | Key size | Mode of operation | Iterations | Message          | Enc Time (microseconds) | Dec Time (microseconds) |
| :--  | :-- | :-- | :-- | :-- | :-- | :-- |
| AES | 128 | ECB  | 100000 | Lab 6 is awesome | 0.039718654 | 0.052759466 |
| AES | 128 | CBC  | 100000 | Lab 6 is awesome | 0.042247859 | 0.047753416 |
| AES | 128 | OFB  | 100000 | Lab 6 is awesome | 0.067076417 | 0.071069361 |
| AES | 128 | PCBC | 100000 | Lab 6 is awesome | 0.051517176 | 0.066494304 |

### 3
| Algorithm | Key size | Mode of operation | Iterations | Message          | Enc Time (microseconds) | Dec Time (microseconds) |
| :--  | :-- | :-- | :-- | :-- | :-- | :-- |
| RC2 | 128  | ECB | 100000 | Lab 6 is awesome | 0.140922384 | 0.127861726 |
| RC2 | 256  | ECB | 100000 | Lab 6 is awesome | 0.132301977 | 0.122220371 |
| RC2 | 512  | ECB | 100000 | Lab 6 is awesome | 0.121418943 | 0.112115757 |
| RC2 | 1024 | ECB | 100000 | Lab 6 is awesome | 0.091218209 | 0.072514624 |

### 4
| Algorithm | Key size | Mode of operation | Iterations | Message Length   | Enc Time (microseconds) | Dec Time (microseconds) |
| :--  | :-- | :-- | :-- | :-- | :-- | :-- |
| AES | 128 | ECB | 100000 | 10    |  |  |
| AES | 128 | ECB | 100000 | 100   |  |  |
| AES | 128 | ECB | 100000 | 1000  |  |  |
| AES | 128 | ECB | 100000 | 10000 |  |  |
