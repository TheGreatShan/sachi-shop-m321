package utils

import (
	"fmt"
	"time"
	"net/http"
	"bytes"
	"encoding/json"
)

func ConnectEureka() {
	instanceInfo := map[string]interface{}{
		"instance": map[string]interface{}{
			"instanceId":       "localhost:logging-service:8000",
			"hostName":         "localhost",
			"app":              "logging-service",
			"ipAddr":           "localhost",
			"vipAddress":       "logging-service",
			"secureVipAddress": "logging-service",
			"status":           "UP",
			"port": map[string]interface{}{
				"$":    8000,
				"@enabled": true,
			},
			"securePort": map[string]interface{}{
				"$":    443,
				"@enabled": false,
			},
			"healthCheckUrl":   "http://localhost:8000/health",
			"statusPageUrl":    "http://localhost:8000/info",
			"homePageUrl":      "http://localhost:8000",
			"dataCenterInfo": map[string]string{
				"@class": "com.netflix.appinfo.InstanceInfo$DefaultDataCenterInfo",
				"name":   "MyOwn",
			},
		},
	}

	jsonData, err := json.Marshal(instanceInfo)
	if err != nil {
		fmt.Println("Error marshalling JSON:", err)
		return
	}

	eurekaURL := "http://localhost:8761/eureka/apps/logging-service"
	
	for {
		resp, err := http.Post(eurekaURL, "application/json", bytes.NewBuffer(jsonData))
		if err != nil {
			fmt.Println("Error registering with Eureka:", err)
		} else {
			defer resp.Body.Close()
			if resp.StatusCode == http.StatusNoContent || resp.StatusCode == http.StatusOK {
				fmt.Println("Successfully registered with Eureka")
				go sendHeartbeat(eurekaURL)
				return
			}
			fmt.Printf("Unexpected status code from Eureka: %d\n", resp.StatusCode)
		}
		time.Sleep(10 * time.Second)
	}
}

func sendHeartbeat(eurekaURL string) {
	heartbeatURL := fmt.Sprintf("%s/localhost:logging-service:8000", eurekaURL)
	for {
		req, err := http.NewRequest(http.MethodPut, heartbeatURL, nil)
		if err != nil {
			fmt.Println("Error creating PUT request:", err)
			continue
		}
		req.Header.Set("Content-Type", "application/json")
		resp, err := http.DefaultClient.Do(req)
		if err != nil {
			fmt.Println("Error sending heartbeat:", err)
		} else {
			defer resp.Body.Close()
			if resp.StatusCode == http.StatusOK {
				fmt.Println("Heartbeat sent successfully")
			} else {
				fmt.Printf("Unexpected status code for heartbeat: %d\n", resp.StatusCode)
			}
		}
		time.Sleep(30 * time.Second)
	}
}
