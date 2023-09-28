# No Escape Unity

[![X](https://img.shields.io/badge/Follow%20%40iwritegames-000000?style=for-the-badge&logo=x&logoColor=white)](https://www.x.com/iwritegames)
[![Twitch](https://img.shields.io/badge/Twitch-9146FF?style=for-the-badge&logo=twitch&logoColor=white)](https://www.twitch.com/iwritegames)
[![Youtube](https://img.shields.io/badge/YouTube-FF0000?style=for-the-badge&logo=youtube&logoColor=white)](https://www.youtube.com/channel/UCRFsluuJre6OWpiT1hFJmjA?sub_confirmation=1)
[![Itch.io](https://img.shields.io/badge/Itch.io-FA5C5C?style=for-the-badge&logo=itchdotio&logoColor=white)](https://i-write-games.itch.io/)
[![Instagram](https://img.shields.io/badge/Instagram-E4405F?style=for-the-badge&logo=instagram&logoColor=white)](https://www.instagram.com/iwritegames)
[![Github](https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white)](https://github.com/IWriteGames)

## Download Build for Windows

[![Available on itch.io](http://jessemillar.github.io/available-on-itchio-badge/badge-color.png)](https://i-write-games.itch.io/no-escape)

## About me

Hi! I write games and I like it! I'm Alex and welcome to my Github! I'm Gamedev and Gamedesigner and I upload my projects to Github to share them and discuss how they are programmed, which you will find later in this archive.

The reason is that I like my public repositories to be educational first and foremost. In the README you will find hints on how to improve the code!

If you like what I do, please follow me on my social networks!

## About the project

This project is a first-person shooter, where our goal is just to try to survive the maximum number of rounds while avoiding being killed by zombies.

I modelled the game entirely with Blender 3 except for the zombie characters, which come from the [Kenney.nl](https://www.kenney.nl/) website

![Image Capture](https://iwritegame.com/github/img/no-escape-readme.jpg)

## About the development and improvements

First of all, I wanted to refactor this project as I did with the Senet Game, but due to the changes in Unity I have decided to go ahead in my Unreal Engine studio, so the project will stay as it is.

This project has several problems that need to be fixed.

In terms of coding, it has the same problem as the 2D shooting gallery. Why create and destroy bullets and so many zombies at once? The project was going to be refactored using the object pool method. The code also needs to be reviewed in terms of the issue of public and private variables - do they have to be like this? No, if we were to transfer this code to a multiplayer game we would have major security holes.

About the mechanics of the game, two things are very important. A player can survive infinitely because by giving him the ability to jump, he can reach areas where zombies will never attack him. Also, the paths that the AI can make have a couple of bugs prepared for the careful eye.

Finally, the UI of the Options menu could also be improved.

## Unity Version

[![Made with Unity](https://img.shields.io/badge/Unity-2022.3.3f1-57b.svg?&logo=unity)](https://www.unity.com)

## License

[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg)](https://www.gnu.org/licenses/gpl-3.0)

