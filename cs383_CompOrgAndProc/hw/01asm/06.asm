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
