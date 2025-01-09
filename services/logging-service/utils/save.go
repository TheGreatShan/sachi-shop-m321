package utils

import (
	"fmt"
	"regexp"
	"time"
	
	"logging-service/db"
	"logging-service/structs"
)

func SaveLog(logStr string) {
	re := regexp.MustCompile(`level:(\w+) message:(.*?) timestamp:(\S+) user:(\S+)`)

	matches := re.FindStringSubmatch(logStr)

	if len(matches) < 5 {
		fmt.Println("Invalid log format")
		return
	}

	level := matches[1]
	message := matches[2]
	timestamp, err := time.Parse(time.RFC3339, matches[3])
	if err != nil {
		fmt.Println("Error parsing timestamp:", err)
		return
	}
	user := matches[4]

	log := structs.Log{
		Level:     level,
		Message:   message,
		Timestamp: timestamp,
		User:      user,
	}

	err = db.ActiveDb.Create(&log).Error
	if err != nil {
		fmt.Println("Error saving log to database:", err)
	} else {
		fmt.Printf("%s\n", logStr)
	}
}
