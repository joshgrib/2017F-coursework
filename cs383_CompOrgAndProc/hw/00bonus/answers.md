# CS 383 Bonus Assignment
Josh Gribbon

## Question 1: Calculate the absolute value of two numbers’ difference. The number is stored at r16 and r17 (aka. |r16-r17|). Store the result to register r20. In the program, you are not allowed to use compare instruction (cp).
```

```

## Question 2: Write an assembly program that will reverse a 10-element array stored in memory, using the stack. The elements should start at location 0x0100. Your program should start by loading 10 elements into memory.
```

```

## Question 3: Use the time delay chart provided in the slides. Given the follow program, how long will this code take to run on a machine operating at 1 GHz? Since comparisons are just arithmetic, you may assume “cp” takes 1 cycle
```
    ldi r16, 0      ; 1 cycle
    ldi r17, 10     ; 1 cycle
    ldi r19, 0      ; 1 cycle
l1: cp  r16, r17    ; 1 cycle
    breq e1         ; 2/1 cycle
    add r19, r16    ; 1 cycle
    inc r16         ; 1 cycle
    jmp l1          ; 3 cycles
e1: nop             ; 1 cycle
```
* 3 cycles before l1
* l1 loops 10 times before the comparison is equal, with a single-cycle branch check each time(10*7=70)
* the first two lines of l1 run once and branch(3 cycles)
* nop at the end for 1 cycle
* Total cycles: 77
* 1 GHz = 1 nanosecond per cycle
* **Total time: 77 nanoseconds**

## Question 4: Explain the following code
```
; read in from ports
in r16, DDRC            ; input to r16
in r17, PORTC           ; input to r17
; set bits 0-5 of port C as inputs
cbr r16, 0b00111111     ; r16 OR  00111111
; set up pull-up resistors
sbr r17, 0b00111111     ; r17 AND 00111111
; output to port C
out DDRC, r16           ; output r16
out PORTC, r17          ; output r17
```
