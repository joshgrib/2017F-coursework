;
; AssemblerApplication1.asm
;
; Created: 9/28/2017 2:00:00 PM
; Author : Josh Gribbon
;

.include "m328Pdef.inc"

main:
	ldi r22, 0x20 ;store value in register
	sts 0x0100, r22 ;store register value in memory
	ldi zh, 0x01 ;store memory address in 'z' register (2 parts: z-high and z-low)
	ldi zl, 0x00
	ldi r20, 0x05 ;store a value in register
	clr r23 ;clear out registers
	clr r22
	clr r19

loop:
	ld r16, z+ ;
	add r21, r16

