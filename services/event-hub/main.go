package main

import (
	"fmt"
	"net/http"

	"github.com/gorilla/mux"
)

func main() {
	r := mux.NewRouter()

	r.HandleFunc("/produce", Receive).Methods("POST")

	fmt.Println("Event-Hub service started")
	http.ListenAndServe(":8005", r)
}