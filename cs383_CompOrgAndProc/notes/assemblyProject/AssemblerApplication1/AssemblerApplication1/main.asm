;
; AssemblerApplication1.asm
;
; Created: 9/28/2017 2:00:00 PM
; Author : Josh Gribbon
;

.include "m328Pdef.inc"


;general idea
; 1. put stuff in RAM
; 2. load values into stack
; 3. initialize port B
; 3. pop out to reverse while outputting

init:
	; load data into RAM
	ldi r16, 1
	sts $0100, r16
	ldi r16, 2
	sts $0101, r16
	ldi r16, 3
	sts $0102, r16
	ldi r16, 4
	sts $0103, r16
	ldi r16, 5
	sts $0104, r16

main:
	ldi r21, low(RAMEND)
	out spl, r21
	ldi r22, high(RAMEND)
	out sph, r22
	; load values from RAM into stack
	lds r16, $0100
	push r16
	lds r16, $0101
	push r16
	lds r16, $0102
	push r16
	lds r16, $0103
	push r16
	lds r16, $0104
	push r16
	; initialize portb
	ser r16
	out DDRB, r16
	; pop from stack and output to portb
	pop r16
	out PORTB, r16
	pop r16
	out PORTB, r16
	pop r16
	out PORTB, r16
	pop r16
	out PORTB, r16
	pop r16
	out PORTB, r16
	
