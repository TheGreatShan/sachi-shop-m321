package db

import (
	"gorm.io/driver/mysql"
	"gorm.io/gorm"
)

var ActiveDb *gorm.DB

func SetUpDatabase() {
	dsn := "root:humanities@best-password.ByFar2025@tcp(localhost:3306)/logging?charset=utf8mb4&parseTime=True&loc=Local"

	var err error
	ActiveDb, err = gorm.Open(mysql.Open(dsn), &gorm.Config{})
	if err != nil {
		panic("Failed to Connect to Database")
	}
}
