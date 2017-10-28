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
