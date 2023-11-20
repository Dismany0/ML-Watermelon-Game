# ML Watermelon Game
 A version of the Suika Game created in Unity that utilizes Unity's ML Agents package to train a neural net capable of playing the game.

 ## Description
 The [Suika Game](https://en.wikipedia.org/wiki/Suika_Game) or Watermelon Game is a game for the Nintendo Switch that received a global release around October 2023. The objective of the game is obtain a high score through combining fruit inside of a limited basket. When two fruit of the same type make contact, they merge into a larger fruit and points are awarded. 

 This project applies reinforcement learning to train a neural net that learns how to play the Suika Game. It was trained on a recreated version of the Suika Game designed in Unity. The results of the training can be viewed in the documented video below.

 ## How To Run
 In the releases there are two version provided, one which is playable, and one which is the an automatic version that utilizes a trained model. 
 In the playable version, the controls are as follows:
 | Key      | Control |
| ----------- | ----------- |
| A      | Move Left       |
| D   | Move Right        |
| Spacebar      | Drop Fruit  |
| R   | Restart Level        |
| N      | Skip Background Song   |

In the trained version, it plays fully automatically, and the Camera is set to automatically move around to provide different views of the training area used. There are no controls here, just watch and enjoy or something.

A youtube video that better documents this project is here
https://youtu.be/xGfSq6Ig7EU?si=jO9YaOVU56aYOUOa
