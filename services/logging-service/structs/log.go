package structs

import(
	"gorm.io/gorm"
	"time"
)

type Log struct {
	gorm.Model
	Level   string `gorm:"size:255;not null;" json:"level"`
	Message string `gorm:"size:255;not null;" json:"message"`
	Timestamp time.Time `gorm:"size:255;not null;" json:"timestamp"`
	User  string `gorm:"size:255;not null;" json:"user"`
}