package controller

import(
	"net/http"
)

func HandleInfo(w http.ResponseWriter, r *http.Request) {
	w.Write([]byte("This is a simple event-hub service, which is apart of a microservice architecture for an Online-Shop. It retrieves all events of the system and processes them as needed."))
}