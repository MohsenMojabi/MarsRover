# Mars Rover Kata

#### TODO List:
  1 - Considering big numbers for a planet 
  <br />2 - Considering size of Rover at the edges and obstacles
  <br />3 - Creating UI
  <br />4 - Ability to define distance for every movement
  
#### How to call api
After running the application, the swagger UI will be displayed.
<br />All the parameters are required except obstacles.

<br />  **edges**   ===>   2 integer for edges in this syntax : **10 10**
<br />  **obstacles(optional)**   ===>   List of all obstacles in this syntax : **(3 4)(2 3)(6 4)**
<br />  **initialPosition**   ===>   Initial position and direction of the rover in this syntax : **7 5 n**
<br />  **commands**   ===>   Sequence of commands in this syntax : **fflbBRfFfLbBrFff**

## Incremental Kata - no peeping ahead!
This is an incremental kata to simulate a real business situation: work your way through the steps in order, but do not read the next requirement before you have finished your current one.

## Your Task
You’re part of the team that explores Mars by sending remotely controlled vehicles to the surface of the planet. Develop an API that translates the commands sent from earth to instructions that are understood by the rover.

## Requirements
- You are given the initial starting point (x,y) of a rover and the direction (N,S,E,W) it is facing.
- The rover receives a character array of commands.
- Implement commands that move the rover forward/backward (f,b).
- Implement commands that turn the rover left/right (l,r).
- Implement wrapping at edges. But be careful, planets are spheres. Connect the x edge to the other x edge, so (1,1) for x-1 to (5,1), but connect vertical edges towards themselves in inverted coordinates, so (1,1) for y-1 connects to (5,1).
- Implement obstacle detection before each move to a new square. If a given sequence of commands encounters an obstacle, the rover moves up to the last possible point, aborts the sequence and reports the obstacle.


## Rules
- Hardcore TDD. No Excuses!
- Change roles (driver, navigator) after each TDD cycle.
- No red phases while refactoring.
- Be careful about edge cases and exceptions. We can not afford to lose a mars rover, just because the developers overlooked a null pointer.



