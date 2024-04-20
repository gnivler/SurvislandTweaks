This is made with BepInex, so you need to install it following their guide:
https://docs.bepinex.dev/master/articles/user_guide/installation/index.html

With BepInEx installed, now place the SurvislandTweaks.dll into BepInEx/Plugins/SurvislandTweaks/ (subfolder optional)

Run the game once and a config file is created called BepInex/Config/SurvislandTweaks.cfg

Edit the values in there to your liking and start the game back up.


This is what the config looks like.  I use 0.6 for vertical as shown, which is something you must edit while following the steps above.

## Settings file was created by plugin SurvislandTweaks v1.0.0
## Plugin GUID: SurvislandTweaks

[General]

## Set upper limit
# Setting type: Int32
# Default value: 99999
FPS = 99999

## True/False
# Setting type: Boolean
# Default value: true
FastWheel = true

## Adjust vertical mouse pitch speed by this factor
# Setting type: Single
# Default value: 1
VertSpeedFactor = 0.6
