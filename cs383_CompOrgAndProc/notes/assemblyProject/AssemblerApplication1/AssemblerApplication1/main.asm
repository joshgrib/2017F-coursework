.include "m328Pdef.inc"

init:
    ;; set up aliases
    .def    count=r16
    .def    limit=r17
    .def    largest=r18
main:
    ldi     limit, 19
    ldi     zh, 0x01
    ldi     zl, 0x00
    ldi     largest, 0
loop:
    cpc     zl, limit
    brge    end
    ld      r22, z+
    cpc     r22,largest
    brge    largefound
    rjmp    loop
largefound:
    mov     largest, r22
    rjmp    loop
end:
	ser		r19
	out		DDRB, r19
    out     PORTB, largest
    ret