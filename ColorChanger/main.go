package main

import (
	settings "ColorChanger/Settings"
	twitch "ColorChanger/Twitch"

	"fmt"
)

func main() {
	fmt.Println("Started ColorChanger")
	s := settings.LoadSettings()
	twitchClient := twitch.NewClient(s)
	twitchClient.Initialize()
}
