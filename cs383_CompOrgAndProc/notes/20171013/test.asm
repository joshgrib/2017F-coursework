; AVR Assembly example
.def i = r16
.def sum = r17

main:
    ldi i,1    ; i = 1
    ldi sum,0  ; sum = 0
    rjmp loop  ; jump to loop
loop:
    add sum,i  ; sum = sum + i
    cpi i,10   ; compare i and 10
    breq end   ; if i == 10, jump to end
    inc i      ; increment i
    rjmp loop  ; jump to top of loop
end:
    ret; return
