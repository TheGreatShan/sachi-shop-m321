package handlers

import (
	"net/http"
)

// HandleInfo handles HTTP requests for the info endpoint.
// It is part of a simple logging microservice that integrates with a Eureka gateway.
// This handler currently responds with an empty response.
func HandleInfo(w http.ResponseWriter, r *http.Request) {
	w.Write([]byte("This is a simple logging service, which is apart of a microservice architecture for an Online-Shop. It manages all logs for the system."))
}