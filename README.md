# nibbles.net
A little .net console game engine I put together to explore game programming. 

## The Challenge
 - Create a reusable game engine that can easily be used to create new mini games
 - Don't use 3rd party libraries.  Everything is coded from scratch.
 - The engine is purely based on a .net console window to simulate the golden age of oldschool DOS text based games.

## Background
I was inspired to a video I saw on YouTube with this dude creating "snakes" on an Apple ][.
https://www.youtube.com/watch?v=7r83N3c2kPw&t=1088s  So I decided to give it a go for fun and made my own with a .net console app.

The engine name was inspired from a QBasic variation of snakes called "Nibbles" that I used to tinker around with and modify -- one of my first programming activities when I was a kid. :)
https://www.youtube.com/watch?v=rTzev59RUGw

## Updates
- Created snakes game
- Added experimental Tetris to try using the engine to build another game -- not complete.
- Added Menu system (technically, this is just another game using the engine)
- Added Top Score (also another game)
- I broke the rule and used Newtonsoft so I could serialize some C# objects persist top scores to disk. :)

<img src="https://github.com/MetalHexx/nibbles.net/blob/main/nibbles-promo.bmp?raw=true" style=" width:60% ; height:60% " >
