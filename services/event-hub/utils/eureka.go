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

	instance := eureka.NewInstanceInfo("localhost", "event-hub-service", "event-hub-service-1", 8005, 30, false)

	err := client.RegisterInstance("event-hub-service", instance)
	if err != nil {
		log.Fatal("Error registering instance:", err)
	}
	fmt.Println("Instance registered")
}
