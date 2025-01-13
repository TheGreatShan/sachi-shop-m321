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
			"instanceId":       "localhost:event-hub-service:8005",
			"hostName":         "localhost",
			"app":              "event-hub-service",
			"ipAddr":           "localhost",
			"vipAddress":       "event-hub-service",
			"secureVipAddress": "event-hub-service",
			"status":           "UP",
			"port": map[string]interface{}{
				"$":    8005,
				"@enabled": true,
			},
			"securePort": map[string]interface{}{
				"$":    443,
				"@enabled": false,
			},
			"healthCheckUrl":   "http://localhost:8005/health",
			"statusPageUrl":    "http://localhost:8005/info",
			"homePageUrl":      "http://localhost:8005",
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

	eurekaURL := "http://localhost:8761/eureka/apps/event-hub-service"
	
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
	heartbeatURL := fmt.Sprintf("%s/localhost:event-hub-service:8005", eurekaURL)
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
