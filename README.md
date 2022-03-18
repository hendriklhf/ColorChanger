# ColorChanger
This program will change your Twitch chat color to another one after each message you send.
## How to use
- You will need to have go installed. You can get it here: [go.dev](https://go.dev/dl)
- Execute the following:

```shell
git clone https://github.com/Sterbehilfe/ColorChanger.git
cd ColorChanger/
./build.sh
```
- You can now find an executable and a JSON file ion the "Build" folder.
- Now insert your Twitch username and your OAuth token into it and define the channels you would like this to work in. You can use the predefined colors or define your own colors.
  Which should look like the following:

```json
{
  "Username": "strbhlfe",
  "OAuthToken": "oauth:abcdefghijklmnopqr0123456789",
  "Channels": [
    "strbhlfe",
    "okayegteatime"
  ],
  "Colors": [
    "#FF0000",
    "#FF005A",
    "#FF00B4",
    "#F000FF",
    "#9600FF",
    "#3C00FF",
    "#001EFF",
    "#0078FF",
    "#00CDFF",
    "#00FFD7",
    "#00FF7D",
    "#00FF23",
    "#37FF00",
    "#91FF00",
    "#EBFF00",
    "#FFBE00",
    "#FF6400",
    "#FF0F00"
  ]
}
```
