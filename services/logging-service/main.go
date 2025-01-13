package main

import (
	"fmt"
	"net/http"
	"github.com/gorilla/mux"
	"github.com/gorilla/handlers"

	"logging-service/controller"
	"logging-service/utils"
)

func setup() {
	utils.InitializeDatabase()
	utils.ConnectEureka()
	go utils.ConsumeLogs()
}

func main() {
	setup()

	r := mux.NewRouter()
	r.HandleFunc("/ping", controller.HandlePing).Methods("GET")
	r.HandleFunc("/info", controller.HandleInfo).Methods("GET")

	api := r.PathPrefix("/api").Subrouter()
	api.HandleFunc("/log", controller.HandleGetLogByUser).Methods("GET")
	api.HandleFunc("/logs", controller.HandleGetLogs).Methods("GET")
	api.HandleFunc("/log", controller.HandlePostLog).Methods("POST")
	api.HandleFunc("/log", controller.HandleDeleteLog).Methods("DELETE")

	corsHandler := handlers.CORS(
		handlers.AllowedOrigins([]string{"http://localhost:5173"}),
		handlers.AllowedMethods([]string{"GET", "POST", "DELETE", "OPTIONS"}),
		handlers.AllowedHeaders([]string{"Content-Type", "Authorization"}),
	)

	fmt.Println("Logging service started")
	http.ListenAndServe(":8000", corsHandler(r))

}