# nibbles.net
A little .net console game engine I put together from scratch to explore game programming. I created this for fun and educational purposes.  It's a work in progress and may never be completed. Enjoy!

## The Challenges
 - Create a game engine that can easily be reused to create mini games
 - Don't use 3rd party libraries.  Everything must be coded from scratch.
 - The engine is purely based on a .net console window to simulate the golden age of oldschool DOS text based games.

## Background
I was inspired to a video I saw on YouTube with this dude creating "snakes" on an Apple ][.
https://www.youtube.com/watch?v=7r83N3c2kPw&t=1088s  So I decided to give it a go for fun and made my own with a .net console app.

The engine name was inspired from a QBasic variation of snakes called "Nibbles" that I used to tinker around with and modify -- one of my first programming activities when I was a kid. :)
https://www.youtube.com/watch?v=rTzev59RUGw

## Updates
- Created snakes game
- Added experimental Tetris to try using the engine to build another game -- not complete.
- Added UI sprites for displaying dialogs, etc
- Added Menu system (technically, this is just another "game" using the engine)
- Added Top Score (also another "game")
- I broke the rule and used Newtonsoft so I could serialize some C# objects persist top scores to disk. :)

<img src="https://github.com/MetalHexx/nibbles.net/blob/main/nibbles-promo.bmp?raw=true">
