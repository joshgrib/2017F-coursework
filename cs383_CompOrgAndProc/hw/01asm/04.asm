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
