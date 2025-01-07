package main

import(
	"fmt"

	"encoding/json"
	"net/http"
)

func Receive(w http.ResponseWriter, r *http.Request) {
    var message map[string]interface{}
    err := json.NewDecoder(r.Body).Decode(&message)
    if err != nil {
        http.Error(w, err.Error(), http.StatusBadRequest)
        return
    }

    messageString := fmt.Sprintf("%v", message)

    ProduceEvent(messageString)

    w.WriteHeader(http.StatusOK)
    json.NewEncoder(w).Encode(message)
}