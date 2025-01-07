package main

import (
	"fmt"
	"net/http"
	"github.com/gorilla/mux"

	"logging-service/handlers"
	"logging-service/utils"
)

func main() {
	utils.InitializeDatabase()
	utils.ConnectEureka()

	r := mux.NewRouter()
	r.HandleFunc("/ping", handlers.HandlePing).Methods("GET")
	r.HandleFunc("/info", handlers.HandleInfo).Methods("GET")

	api := r.PathPrefix("/api").Subrouter()
	api.HandleFunc("/log", handlers.HandleGetLogByUser).Methods("GET")
	api.HandleFunc("/log", handlers.HandlePostLog).Methods("POST")
	api.HandleFunc("/log", handlers.HandleDeleteLog).Methods("DELETE")

	fmt.Println("Logging service started")
	http.ListenAndServe(":8000", r)

}