# ColorChanger
This program will change your Twitch chat color to another one after each message you send.

# Usage
## Build and execution
There are releases in this repository, but I currently don't know how to properly because I am an "unknown publisher", so the Windows Defender Smart Screen will try to stop you when starting the executable.<br />
~~A safe way would be to download the repository and build it yourself with, for example, Visual Studio 2019.~~<br />
Currently not buildable, because the repository contains an unfinished version.
## Settings
Insert your username and OAuth token into the ```Settings.json```.
```json
"AccountSettings": {
  "Username": "",
  "OAuthToken": "",
  "Channels": null
},
```
If ```"Channels"``` is ```null``` the program will automatically use your Chatterino tabs as channels.<br />
If you only want it to work is specific channels, you can just create an array of channels there, like this:<br />
```json
"Channels": [
  "channel1",
  "channel2"
]
```
You can also change the color array in the settings to your custom colors.
## Issues
If no console output shows, just restart the program.
