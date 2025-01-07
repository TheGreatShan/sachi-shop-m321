package main

import (
	"github.com/gorilla/mux"
	"net/http"
)

func main() {
	r := mux.NewRouter()

	r.HandleFunc("/receive", Receive).Methods("POST")
	http.ListenAndServe(":8001", r)
}