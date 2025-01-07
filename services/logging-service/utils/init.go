package utils

import(
	"fmt"

	"logging-service/db"
	"logging-service/structs"
)

func InitializeDatabase() {
	db.SetUpDatabase()
	db.ActiveDb.AutoMigrate(&structs.Log{})

	fmt.Println("Database Migrated")
}