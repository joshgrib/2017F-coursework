#define __SFR_OFFSET 0

#include "avr/io.h"
#include <avr/portpins.h>

/*
// This mostly works with a button connected to pin 0 and ground, and a light to pin 8 and ground
.global setup
.global loop

setup:
  clr r16
  
loop:
  in r17, PIND          // get state of pins on Port D
  andi r17, (1<<PD0)    // you should mask to get only what you want
  cpi r17, 0            // compare result to 0 (pushbutton is pressed)
  brne loop            // if != 0, go check again
  cpi r16, 0             // compare del to 0
  brne dec_jmp           // if != 0, ignore this run
  ldi r17, (1<<PB0)     // otherwise, write logic 1 to pin 0 of Port B
  out PINB, r17         // which toggles the state of the pin
  ldi r16, 250           // next one will be ignored for 250 runs

dec_jmp:
  dec r16                // decrement del
  rjmp loop
*/


//This works to blink pin 13 on and off
.global setup
.global loop

setup:
    //set pin 13 as output
    ldi r16, 0b00100000
    out DDRB, r16

loop:
    //toggle the light
    out PINB, r16
    call Delay_small
    rjmp loop

Delay_small:              
    LDI     r18,   64  
Delay1:
    LDI     r19,   125   
Delay2:
    LDI     r20,   250   
Delay3:
    DEC     r20         
    NOP                
    BRNE    Delay3    
    DEC     r19         
    BRNE    Delay2      
    DEC     r18          
    BRNE    Delay1         
RET



/*
 * Useful links:
 *  - https://arduino.stackexchange.com/questions/23146/make-led-blink-using-avr-assembly/23160
 */
