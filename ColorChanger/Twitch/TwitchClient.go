package twitch

import (
	settings "ColorChanger/Settings"
	"fmt"
	"strings"

	"github.com/ahmetb/go-linq"
	irc "github.com/gempir/go-twitch-irc/v2"
)

type Client struct {
	settings  settings.Settings
	ircClient *irc.Client
	colorIdx  int
}

func NewClient(s settings.Settings) *Client {
	return &Client{
		settings:  s,
		ircClient: irc.NewClient(s.Username, s.OAuthToken),
		colorIdx:  0,
	}
}

func (client *Client) Initialize() {
	client.ircClient.OnPrivateMessage(func(msg irc.PrivateMessage) {
		if msg.User.Name == client.settings.Username {
			client.changeColor()
		}
	})

	getChannels(&client.settings.Channels)
	if len(client.settings.Channels) == 0 {
		panic("The channel list can't be empty.")
	}

	client.joinChannels()
	err := client.ircClient.Connect()
	if err != nil {
		panic(err)
	}
}

func (client *Client) joinChannels() {
	for _, c := range client.settings.Channels {
		client.ircClient.Join(c)
		fmt.Println("Joined channel <#" + c + ">")
	}
}

func (client *Client) changeColor() {
	color := client.settings.Colors[client.colorIdx]
	client.ircClient.Say(client.settings.Channels[0], ".color "+color)
	fmt.Println("Changed color to", color)

	client.colorIdx++
	if client.colorIdx >= len(client.settings.Colors) {
		client.colorIdx = 0
	}
}

func getChannels(channels *[]string) {
	linq.From(*channels).SelectT(func(c string) string {
		return strings.ToLower(c)
	}).Distinct().ToSlice(channels)
}
