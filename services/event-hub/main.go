package main

import (
	"fmt"
	"net/http"
	"github.com/gorilla/mux"
	"github.com/gorilla/handlers"

	"event-hub/utils"
	"event-hub/controller"
)

func main() {
	r := mux.NewRouter()

	r.HandleFunc("/produce", controller.Receive).Methods("POST")
	r.HandleFunc("/info", controller.HandleInfo).Methods("GET")

	corsHandler := handlers.CORS(
		handlers.AllowedOrigins([]string{"http://localhost:5173"}),
		handlers.AllowedMethods([]string{"GET", "POST", "DELETE", "OPTIONS"}),
		handlers.AllowedHeaders([]string{"Content-Type", "Authorization"}),
	)

	fmt.Println("Event-Hub service started")
	utils.ConnectEureka()
	http.ListenAndServe(":8005", corsHandler(r))
}