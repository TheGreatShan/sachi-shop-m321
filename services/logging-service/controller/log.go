package controller

import (
	"encoding/json"
	"net/http"

	"logging-service/structs"
	"logging-service/db"
)

func HandleGetLogByUser(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")

	user := r.URL.Query().Get("user")
	if user == "" {
		w.WriteHeader(http.StatusBadRequest)
		response := structs.Response{Message: "Missing 'user' query parameter"}
		json.NewEncoder(w).Encode(response)
		return
	}

	var logs []structs.Log
	result := db.ActiveDb.Where("user = ?", user).Find(&logs)
	if result.Error != nil {
		w.WriteHeader(http.StatusInternalServerError)
		response := structs.Response{Message: "Failed to retrieve logs"}
		json.NewEncoder(w).Encode(response)
		return
	}

	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(logs)
}

func HandleGetLogs(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")

	var logs []structs.Log
	result := db.ActiveDb.Find(&logs)
	if result.Error != nil {
		w.WriteHeader(http.StatusInternalServerError)
		response := structs.Response{Message: "Failed to retrieve logs"}
		json.NewEncoder(w).Encode(response)
		return
	}

	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(logs)
}


func HandlePostLog(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")

	var log structs.Log
	err := json.NewDecoder(r.Body).Decode(&log)
	if err != nil {
		w.WriteHeader(http.StatusBadRequest)
		response := structs.Response{Message: "Failed to POST log"}
		json.NewEncoder(w).Encode(response)
		return
	}

	result := db.ActiveDb.Create(&log)
	if result.Error != nil {
		w.WriteHeader(http.StatusBadRequest)
		response := structs.Response{Message: "Failed to create log"}
		json.NewEncoder(w).Encode(response)
		return
	}

	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(log)
}

func HandleDeleteLog(w http.ResponseWriter, r *http.Request) {
    w.Header().Set("Content-Type", "application/json")

    id := r.URL.Query().Get("id")
    user := r.URL.Query().Get("user")

    if id == "" || user == "" {
        w.WriteHeader(http.StatusBadRequest)
        response := structs.Response{Message: "Missing 'id' or 'user' query parameter"}
        json.NewEncoder(w).Encode(response)
        return
    }

    result := db.ActiveDb.Where("user = ? AND id = ?", user, id).Delete(&structs.Log{})
    if result.Error != nil {
        w.WriteHeader(http.StatusInternalServerError)
        response := structs.Response{Message: "Failed to delete log"}
        json.NewEncoder(w).Encode(response)
        return
    }

    if result.RowsAffected == 0 {
        w.WriteHeader(http.StatusNotFound)
        response := structs.Response{Message: "Log not found"}
        json.NewEncoder(w).Encode(response)
        return
    }

    w.WriteHeader(http.StatusOK)
    response := structs.Response{Message: "Log deleted successfully"}
    json.NewEncoder(w).Encode(response)
}
