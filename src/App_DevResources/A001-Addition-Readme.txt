
# Tasks
As user progress then 50% percent of the level are from previous levels
fill level 50% from previous level (not all previous)


English Aplhabet 26 Letters
A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z

F(0,20)  => 0,20 - 0,20

# Tags
Addition, Place-Value

## Level 1
10+
0,1,2
0+10, 10+0, 10+1, 1+10, 10+2, 2+10

## Level 2
10+
3,4
3+10, 10+3, 4+10, 10+4

## Level 3
10+
5,6
5+10, 10+5, 6+10, 10+6

## Level 4
10+
7,9
7+10, 10+7, 8+10, 10+8

## Level 5
10+
9
9+10, 9+10

## Level 6
0-12+
0-12

## Level 7
+10
10,20,30,40,50,60,70,80,90
10+10, 10+20, 20+10

## Level 8
+20,30
10,20,30,40,50,60,70,80,90
...

## Level 9
+40,50
10,20,30,40,50,60,70,80,90
...

## Level 10
+60,70
10,20,30,40,50,60,70,80,90
60+10, 10+60

## Level 11
+80,90
10,20,30,40,50,60,70,80,90
...

Addition sequences (higher level)
0-20
0-20
0+0, 1+1, 2+2, 3+3, 19+19, 20+20


Addition basic 0-9
100+
0-9

Addition sequences
10-100+
10-100

-------------------------------------

Place value
10-90
0-9
10+0, 10+1, 20+0, 20+1

-------------------------------------

Addition without carrying
0-90
0-9
31+8, 21+8

-------------------------------------

Addition Two Plus one Digit addition (this might carry)
10-90
53+4, 88+3, 80+8,25+6

-------------------------------------

Addition by 5
5, 10, 15, 20, 25, 30, 35
5, 10, 15, 20, 25, 30, 35
15+35

-------------------------------------

Addition for Time
45+45, 15+15, 30+30, 60+30

-------------------------------------

Multiplies (Skip counting)
2,3,4
2+2+2+2+2
3+3+3
4+4+4

Multiplies (Skip counting)
5,6,7
5+5+5

Multiplies (Skip counting)
8,9
8+8

-------------------------------------

Single digit doubling
3+3
4+4

Double digit doubling
10+10
11+11

-------------------------------------

Triple digit doubling
100+100
200+200



#Campaign - 1.Basic
// A(1,4)  => 1 to 4 + 1
// B(1,9)  => 1 to 9 + 1
// C(0,9)  => 0 to 9 + 0
// D(0,9)  => 0+0, 1+1, 2+2, to 9+9
// E(0,9)   => 0,9 - 0,9
// F(0,20)  => 0,20 - 0,20
// G Mix, A,B,C,D,E,F

#Campaign - 2.Place Value
// A(10,90) 	=> 10 to 90 + 0 to 9            - tens and ones
// B(10,90) 	=> 10 to 90 + 10 to 99          - tens and tens
// C(100,900)   => 100 to 900 + 0 to 9          - hundreds and ones
// D(100,900)   => 100 to 900 + 10 to 99        - hundreds and tens
// E(100,900)   => 100 to 900 + 100 to 999      - hundreds and tens
// F(1000,9000) => 1000 to 9000 + 0 to 9        - thousands and ones
// G(1000,9000) => 1000 to 9000 + 10 to 99      - thousands and tens
// H(1000,9000) => 1000 to 9000 + 1000 to 9999  - thousands and thousands

// B(10,999) 	=> 100 to 999   + 100, 200, 300 to 900      
// C(1000,9999)	=> 1000 to 9999 + 1000, 2000 to 9000
// D(10,9999)   => 10 to 9999   + 1000, 1010, 1100
// E Mix, A,B,C,D

#Campaign - No Carry

        /// <summary>
        /// 3. Campaign, Addition No Carry. Two plus one digit addition.
        /// </summary>
        /// <example>
        /// A(0, 99) => 10 to 99 + 0 to 9
        /// 
        /// 12 + 4
        /// 78 + 1
        /// 99 + 0
        /// </example>

#Campaign - Carry

        /// <summary>
        /// 4. Campaign, Addition Carry. Two plus one digit addition.
        /// </summary>
        /// <example>
        /// A(11, 99) => 11 to 99 + 1 to 9
        /// 
        /// 12 + 8
        /// 47 + 5
        /// </example>

# Campaign - General Skills

        /// <summary>
        /// 5. Campaign, Addition general skills for specific applications such as (money, multiplies of 2-3-5-10-15-25-50, and time).
        /// </summary>
        /// <example>
        /// A(0, 50)    => 0 to 50 + 0 to 50 steps by 2
        /// B(0, 60)    => 0 to 60 + 0 to 60 steps by 3
        /// C(0, 60)    => 0 to 60 + 0 to 60 steps by 5
        /// D(0, 60)    => 0 to 60 + 0 to 60 steps by 10
        /// E(0, 60)    => 0 to 60 + 0 to 60 steps by 15
        /// F(0, 60)    => 0 to 60 + 0 to 60 steps by 20,
        /// G(0, 100)   => 0 to 100 + 0 to 100 steps by 25
        /// H(0, 100)   => 0 to 100 + 0 to 100 steps by 50
        /// I(0, 100)   => 0 to 100 + 0 to 100 steps by Mix all above.
        /// 
        /// 2 + 2
        /// 22 + 12
        /// </example>

#Campaign - Multiples

        /// <summary>
        /// 7. Campaign, Addition with multiples / skip counting.
        /// </summary>
        /// <example>
        /// A(0, 4)  => 0 to 4 => min 2 and max 5 numbers (random)
        /// B(0, 9)  => 0 to 9 => min 2 and max 5 numbers (random)
        /// 
        /// 5 + 5 + 5 + 5
        /// 21 + 21 + 21
        /// </example>

# Campaign - Doubling Values

        /// <summary>
        /// 8. Campaign, Addition doubling values.
        /// </summary>
        /// <example>
        /// A(0, 9)     => 0 to 9
        /// B(10, 99)   => 10 to 99
        /// 
        /// 4 + 4
        /// 22 + 22
        /// </example>

#Campaign - Decimals

        /// <summary>
        /// 6. Campaign, Addition with decimals.
        /// </summary>
        /// <example>
        /// A(0, 9)     => 0 to 9 + 0 to 9 by tenths
        /// B(10, 99)   => 10 to 99 + 10 to 99 by tenths
        /// C(0, 9)     => 0 to 9 + 0 to 9 by hundredths
        /// D(10, 99)   => 10 to 99 + 10 to 99 by hundredths
        /// 
        /// 1.2 + 2.5
        /// 4.10 + 19
        /// 25.10 + 45.24
        /// </example>



## Tasks
think how to include previous levels test if has mix
    here we should have some group to see what levels shall be used

    Root Grup
        Parent (group)
            Level 1
            Level 2
            Level n

    Well that might work but desing not friendly. Tags?
        Possible

     Level 1
        Addition, Basic,
     Level 2
         Addition, Basic,
     Level n
