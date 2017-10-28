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
