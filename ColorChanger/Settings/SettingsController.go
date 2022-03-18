package settings

import (
	"encoding/json"
	"fmt"
	"os"
	"regexp"
	"strings"

	linq "github.com/ahmetb/go-linq"
)

const (
	Path = "./Settings.json"
)

var (
	colorPattern = regexp.MustCompile("(?i)^#?[A-F0-9]{6}$")
)

func LoadSettings() Settings {
	content, err := os.ReadFile(Path)
	if err != nil {
		panic(err)
	}
	var settings Settings
	err = json.Unmarshal(content, &settings)
	if err != nil {
		panic(err)
	}
	fmt.Println("Loaded settings")
	VerifyColors(&settings.Colors)
	settings.Username = strings.ToLower(settings.Username)
	return settings
}

func VerifyColors(colors *[]string) {
	if len(*colors) == 0 {
		panic("The color array can't be empty")
	}

	linq.From(*colors).WhereT(func(c string) bool {
		return colorPattern.MatchString(c)
	}).ToSlice(colors)
	for i, c := range *colors {
		if !strings.HasPrefix(c, "#") {
			(*colors)[i] = "#" + c
		}
	}
	fmt.Println("Colors verified")
}
