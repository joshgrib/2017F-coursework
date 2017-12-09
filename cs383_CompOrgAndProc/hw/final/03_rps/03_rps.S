#define __SFR_OFFSET 0

#include "avr/io.h"
//#include <avr/portpins.h>

.global setup
.global loop

#define all_on  r25
#define all_off r26
#define rock r27
#define paper r28
#define scissors r29

setup:
    ldi all_on,   0b11111111
    ldi all_off,  0b00000000
    ldi rock,     0b00100000
    ldi paper,    0b01000000
    ldi scissors, 0b10000000
    out DDRD, all_off

loop:
  out PORTD, rock
  call Delay_1s
  out PORTD, all_off
  call Delay_1s
  out PORTD, paper
  call Delay_1s
  out PORTD, all_off
  call Delay_1s
  out PORTD, scissors
  call Delay_1s
  out PORTD, all_off
  call Delay_1s

Delay_1s:
    LDI     r21,   105
L1: LDI     r22,   125
L2: LDI     r23,   250
L3:
    NOP
    NOP
    DEC     r23
    BRNE    L3
    DEC     r22
    BRNE    L2
    DEC     r21
    BRNE    L1
RET



/*
 * Useful links:
 *  - https://arduino.stackexchange.com/questions/23146/make-led-blink-using-avr-assembly/23160
 *  - https://stackoverflow.com/questions/39361410/creating-a-toggle-switch-in-avr-assembly
 *  - https://www.arduino.cc/en/Reference/PortManipulation
 */
