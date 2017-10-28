# CS 383 Midterm

1. **What­ does ALU stand for? What does CU stand for? What is a register? Be precise. Name several different kinds of values that a register might hold. What is the purpose of the instruction register?**
    * Arithmetic Logic Unit
    * Control Unit
    * A register is a block of storage that the CPU uses to store and retrieve values, commonly either 32 or 64 bits today.
    * A register could hold: data, memory addresses, program counter,  accumulator, anything the CPU needs to directly interact with.
    * The instruction register stores the value of the instruction being executed.

2. **Most CPUs today are superscalar. What does that mean?**
    * A superscalar CPU can execute more than one instruction per clock cycles. This is accomplished using multiple execution units that can handle instructions independently at the same time.

3. **Suppose that a CPU always executes the two instructions following a branch instruction, regardless of whether the branch is taken or not. Explain how this can eliminate most of the delay resulting from branch dependency in a pipelined CPU. What penalties or restrictions does this impose on the programs that are executed on this machine?**
    * A pipelined CPU looks at the next few instruction sets and tries to split the tasks up across different parts of the CPU if possible, getting more done in one clock cycle. This means we can start working on the first lines after the branch before knowing if the branch will be taken or not, so we don't have to wait to for the branch decision.
    * Pipelining can cause issues for programs if they have instruction sets close together where the first needs to finish executing before the second can begin. For example if you put a value in 2 registers then add the values in the third instruction, you could end up doing the addition before both values are stored in the registers.

4. **Although CPU remain the same over the years, various improvements occurred in their architecture. Describe them in detail.**
    * Instruction pipelining - this is when the CPU looks at the next few instructions and tries to split them up across the CPU so multiple things can happen at the same time. This allows more instructions to be executed at the same time, increasing the processing speed of the processor.
    * Superscalar execution - this is the CPU outsourcing certain things to separate execution units, similar to a load balancer splitting up the work for several servers.
    * Parallel processing - this is combining multiple CPUs on a computer to further increase the processing power of a machine.

5. **What’s the problem with designing smaller CPU and the Moore’s law?**
    * Moore's law, stating that transistor density of circuits has been roughly doubles every year, means that at some point we would theoretically need transistors that would be smaller than an electron. This would of course not be possible because electrons need to travel through the transistors for the computer to function. As a result, we will either eventually reach a point where Moore's law comes to an end, or need a new type of computer that has different basic principles.

6. **Write a small program in assembly that calculates the following equation: 2x^2-3+4.**
```
ldi r16, x          ; load x value into register
mul r16, r16        ; square the value
mul r16, 0x10       ; multiply by 2
sub r16, 0x11       ; subtract 3
add r16, 0x100      ; add 4
```
