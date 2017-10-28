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
