;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;; Problem 1:
;;   Write an assembly language program to add up the numbers from 1 to 15
;;   (inclusive) and output the result to Port B (Points 15).
.include "m328Pdef.inc"

init:
    ;; set up aliases
    .def    count=r16
    .def    limit=r17
    .def    sum=r18
main:
    ldi     count,1         ;; count = 1
    ldi     limit,16        ;; limit = 15
    clr     sum             ;; sum = 0
    ser     r19             ;; r19 = 0xff
    out     DDRB, r19       ;; set PORTB for output
loop:
    cp      count,limit     ;; diff = count - limit
    brge    end             ;; if (count >= limit) { goto end } else {
    add     sum,count       ;;     sum += count
    inc     count           ;;     count += 1 }
    rjmp    loop            ;; goto loop
end:
    out     PORTB, sum      ;; print(sum)
    ret

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;; Problem 2:
;;   Store 10 numbers at data space 0x0100. Then retrieve the numbers using the
;;   register z and outputs 10 numbers from the memory to Port B. (Points 15)
.include "m328Pdef.inc"

init:
    ;; set up aliases
    .def    temp=r16
    .def    count=r17
    .def    limit=r18
main:
    ldi     count, 0        ;; count = 0
    ldi     limit, 10       ;; limit = 10
    ser     r19             ;; r19 = 0xff
    out     DDRB, r19       ;; set PORTB for output
    jmp     loop
loop:
    cp      count, limit    ;; check if the loop is done
    brge    end             ;; if we're done, go to end
    ldi     temp, count     ;; store count as a value to write
    sts     0x0100, temp    ;; store value to data space
    lds     temp, 0x0100    ;; read from data space
    out     PORTB, temp     ;; output to PORTB
    inc     count           ;; count += 1
    jmp     loop
end:
    ret

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;; Problem 3:
;;   Write an assembly language program to take the next two numbers in memory
;;   starting at data space 0x0100. Compare them and output the greater number
;;   (if the numbers are equal, output that number). (Points 15)
.include "m328Pdef.inc"

init:
    ;; set up aliases
    .def    num1=r16
    .def    num2=r17
    .def    val=r18
main:
    ser     r19             ;; r19 = 0xff
    out     DDRB, r19       ;; set PORTB for output
    lds     num1, 0x0100    ;; read number from data space
    lds     num2, 0x0100    ;; read number from data space
    cp      num1, num2      ;; compare numbers
    bgre    bigone          ;; go to appropriate case
    jmp     bigtwo
bigone:
    ldi     val, num1       ;; store number as the value to ouput
    jmp end
bigtwo:
    ldi     val, num2       ;; store number as the value to ouput
    jmp end
end:
    out     PORTB, val      ;; output to port b
    ret

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;; Problem 4:
;;   Write an assembly language program to add any 15 binary numbers. Get the
;;   numbers from the Data Space starts from 0x0100. Output the lower byte of
;;   the result to Port B and the high byte of the result to Port C. (use
;;   register z to get number). (Points 15)
.include "m328Pdef.inc"

init:
    ;; set up aliases
    .def    count=r16
    .def    limit=r17
    .def    lowsum=r18
    .def    highsum=r19
main:
    ;; initialize variables
    ldi     limit, 16
    ldi     zh, 0x01
    ldi     zl, 0x00
    ldi     lowsum, 0
    ldi     highsum, 0
    ldi     r23, 0
    ;; prep for output
    ser     r19             ;; r19 = 0xff
    out     DDRB, r19       ;; set PORTB for output
    out     DDRC, r19       ;; set PORTC for output
loop:
    ;; loop to add the numbers
    cpc     zl, limit
    brge    end
    ld      r22, z+
    add     lowsum, r22
    adc     highsum, r23
    rjmp    loop
end:
    ;; output numbers
    out     PORTB, lowsum
    out     PORTC, highsum
    ret

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;; Problem 5:
;;   Write an assembly language program to find the largest number in 15 binary
;;   numbers read from Data Space. Get the numbers from the Data Space starts
;;   from 0x0100. Output the result to Port B. (use register z to get number)
;;   (Points 20)
.include "m328Pdef.inc"

init:
    ;; set up aliases
    .def    count=r16
    .def    limit=r17
    .def    value=r18
main:
    ;; initialize variables
    ldi     limit, 16
    ldi     zh, 0x01
    ldi     zl, 0x00
    ldi     value, 0
    ;; prep for output
    ser     r19
    out     DDRB, r19
loop:
    cpc     zl, limit
    brge    end
    ld      r22, z+
    cpc     r22,value       ;; compare new value
    brge    newlarge        ;; if its a new largest value then update
    rjmp    loop
newlarge:
    mov     value, r22
    rjmp    loop
end:
    out     PORTB, value    ;; output to port b
    ret

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;; Problem 6:
;;   Write an assembly language program to do the following: get the number N
;;   from Data Space at 0x0100, add all numbers in [1, N], Output the lower byte
;;   of the result to Port B and the high byte of the result to Port C. (use
;;   register z to get number) (For example: if N is 5, do the adding 1+2+3+4+5)
;;   (Points 20)
.include "m328Pdef.inc"

init:
    ;; set up aliases
    .def    count=r16
    .def    limit=r17
    .def    sumzlow=r18
    .def    sumzhigh=r19
main:
    ;; intializing variables
    ldi     count, 1
    ldi     zh, 0x01
    ldi     zl, 0x00
    ldi     r20, 0
    ld      limit, z
    inc     limit
    clr     sumzlow
    clr     sumzhigh
    ;; prepping for output
    ser     r19             ;; r19 = 0xff
    out     DDRB, r19       ;; set PORTB for output
loop:
    ;; add up numbers
    cpc     count, limit
    brge    end
    add     sumzlow, count
    adc     sumzhigh, r20
    inc     count
    rjmp    loop
end:
    ;; output results
    out     PORTB, sumzlow
    out     PORTC, sumzhigh
    ret
