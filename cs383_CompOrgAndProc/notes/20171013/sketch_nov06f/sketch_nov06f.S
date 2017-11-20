#include "avr/io.h"
.text
.global setup
.global loop
.global timer
.global again

setup:
    ldi r16, 0b00000000
    sts DDRB, r16
loop:
    call blink_S
    call blink_O
    call blink_S 
    jmp loop

blink_S:
    ldi r21, 0b00000011
    ldi r17, 0b00100000     //light ON
    sts PORTB, r17
    call Delay_small
    ldi r17, 0b00000000     //light OFF
    sts PORTB, r17
    call Delay_small 
    ldi r17, 0b00100000     //light ON
    sts PORTB, r17
    call Delay_small
    ldi r17, 0b00000000     //light OFF
    sts PORTB, r17
    call Delay_small 
    ldi r17, 0b00100000     //light ON
    sts PORTB, r17
    call Delay_small
    ldi r17, 0b00000000     //light OFF
    sts PORTB, r17
    call Delay_big
    ret

blink_O:
    ldi r17, 0b00100000     //light ON
    sts PORTB, r17
    call Delay_big
    ldi r17, 0b00000000     //light OFF
    sts PORTB, r17
    call Delay_big 
    ldi r17, 0b00100000     //light ON
    sts PORTB, r17
    call Delay_big
    ldi r17, 0b00000000     //light OFF
    sts PORTB, r17
    call Delay_big 
    ldi r17, 0b00100000     //light ON
    sts PORTB, r17
    call Delay_big
    ldi r17, 0b00000000     //light OFF
    sts PORTB, r17
    call Delay_big
    ret

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

Delay_big:              
    LDI     r18,   32  
Delay4:
    LDI     r19,   125   
Delay5:
    LDI     r20,   250   
Delay6:
    DEC     r20         
    NOP                
    BRNE    Delay3    
    DEC     r19         
    BRNE    Delay5      
    DEC     r18          
    BRNE    Delay6         
RET
