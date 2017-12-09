/* Wiring
 *    LED Layout
 *      2   5
 *      6 1 7
 *      4   3
 *    LED->Pin
 *      1->4
 *      2->5
 *      3->5
 *      4->6
 *      5->6
 *      6->7
 *      7->7
 */
#define __SFR_OFFSET 0
#include "avr/io.h"

.global setup
.global loop

//register aliases
#define I     r16
#define II    r17
#define III   r18
#define IV    r19
#define V     r20
#define VI    r24
#define all_on  r25
#define all_off r26
#define l1_i r27
#define l2_i r28
#define l3_i r29

setup:
    ldi all_on,  0b11111111
    ldi all_off, 0b00000000
    ldi I,    0b00010000
    ldi II,   0b01000000
    ldi III,  0b01010000
    ldi IV,   0b01100000
    ldi V,    0b01110000
    ldi VI,   0b11100000
    out DDRD, all_on

    //flash to start up
    call Delay_tiny
    out PORTD, I
    call Delay_tiny
    out PORTD, II
    call Delay_tiny
    out PORTD, III
    call Delay_tiny
    out PORTD, IV
    call Delay_tiny
    out PORTD, V
    call Delay_tiny
    out PORTD, VI
    call Delay_tiny

loop:
    out PORTD, all_off
    call Delay_1s
    out PORTD, IV
    call Delay_1s
    out PORTD, II
    call Delay_1s
    out PORTD, III
    call Delay_1s
    out PORTD, I
    call Delay_1s
    rjmp loop

Delay_1s:
    LDI     l1_i,   105
L1: LDI     l2_i,   125
L2: LDI     l3_i,   250
L3:
    NOP
    NOP
    DEC     l3_i
    BRNE    L3
    DEC     l2_i
    BRNE    L2
    DEC     l1_i
    BRNE    L1
RET


Delay_tiny:
    LDI     l1_i,   10
L4: LDI     l2_i,   125
L5: LDI     l3_i,   250
L6:
    NOP
    NOP
    DEC     l3_i
    BRNE    L6
    DEC     l2_i
    BRNE    L5
    DEC     l1_i
    BRNE    L4
RET
