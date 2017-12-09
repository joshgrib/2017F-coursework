#define __SFR_OFFSET 0
#include "avr/io.h"

.global setup
.global loop

//set up some register aliases
#define red     r16
#define yellow  r17
#define green   r18
#define all_off r19
#define all_on  r20
#define l1_i    r21
#define l2_i    r22
#define l3_i    r23
#define time_s  r24

setup:

    ldi red,    0b10000000   //red light pin 7
    ldi yellow, 0b01000000   //yellow light pin 6
    ldi green,  0b00100000   //green light pin 5
    ldi all_off,0b00000000
    ldi all_on, 0b11111111
    out DDRD, all_on

loop:
    //turn on red light
    out PORTD, red    //red light on
    ldi time_s, 10    //set for 10 second delay
    call Delay_time   //run the delay

    out PORTD, green  //turn yellow on
    ldi time_s, 7
    call Delay_time

    out PORTD, yellow //turn yellow on
    ldi time_s, 2
    call Delay_time

    rjmp loop

Delay_time:
LI:
    call Delay_1s       //delay 1 second
    dec time_s
    brne LI
    out PORTD, all_off  //turn off lights
    ret

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
