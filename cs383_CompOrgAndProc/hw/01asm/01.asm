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
