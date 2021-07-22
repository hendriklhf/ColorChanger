# ColorChanger
This program will change your Twitch chat color to another one after each message you send.

# Usage
## Build
In order to use this you will have to build the project first.<br />
Any easy way would be to use Visual Studio.<br />
When executing, the executable and the .json file have to be in the same directory.
## Settings
Insert your username and and OAuth token into the ```Settings.json```.
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
If no console output shows, just restart the program.<br />
This program will probably only work on Windows.
