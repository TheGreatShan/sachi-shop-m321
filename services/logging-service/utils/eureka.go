package utils

import (
	"fmt"
	"log"
	"github.com/ArthurHlt/go-eureka-client/eureka"
)

func ConnectEureka() {
	client := eureka.NewClient([]string{
		"http://localhost:8761/eureka",
	})

	instance := eureka.NewInstanceInfo("localhost", "logging-service", "logging-service-1", 8000, 30, false)

	err := client.RegisterInstance("logging-service", instance)
	if err != nil {
		log.Fatal("Error registering instance:", err)
	}
	fmt.Println("Instance registered")
}
