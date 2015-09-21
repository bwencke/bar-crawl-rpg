Bar Crawl RPG
=============

Tiled (http://mapmaker.org) is used for building maps.
- The tiles for building maps are stored in the tiles folder
- If you need more tiles, look at http://opengameart.org/content/liberated-pixel-cup-0

Tiled2Unity (http://www.seanba.com/tiled2unity) converts the .tmx files generated in Tiled so that they can be used in Unity.
- If you are on a Mac, this is a pain in the ass. Do these things (from Jesse A's comment on http://www.seanba.com/tiled2unity-is-on-github.html):
    1. Download mono here:
    http://www.mono-project.com/docs/about-mono/supported-platforms/osx/

    2. Download the updated Tiled2Unity files here:
    https://github.com/Gnumaru/Tiled2Unity/raw/master/tool/Tiled2Unity/build/Tiled2UnityTool.zip

    3. This is the important part here… Go to your downloads folder and open up the folder you just downloaded called “Tiled2UnityTool”.

    4. Grab all those files (starts with Ookii.Dialogs.Modified.dll and the last one is Tiled2Unity.exe) and place them in your home folder. You need to either drag and drop them there or copy and paste them there. The home folder for me is the last folder in my Finder under my favorites off to the left hand side.

    5. Once all the files are there you need to open up the Terminal window. An easy way to find the terminal is to click the magnifying glass that is at the top right of your Mac. When the search feature opens up type “Terminal” and it will be the first thing that pops up.

    6. In the terminal window type mono Tiled2Unity.exe
- Export path = root folder of Unity project
- Vertex Scale = 0.01

Unity (http://unity3d.com) is used to actually build the game.

Sprites:
- Lots of sprites available: http://untamed.wild-refuge.net/rmxpresources.php?characters
- Maybe a good resource for building sprites: http://gaurav.munjal.us/Universal-LPC-Spritesheet-Character-Generator/
