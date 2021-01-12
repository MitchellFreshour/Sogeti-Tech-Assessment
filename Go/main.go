package main

import (
	"io"
	"net/http"
)

func HealthHandler(w http.ResponseWriter, r *http.Request) {
	w.WriteHeader(http.StatusOK)
	w.Header().Set("Content-Type", "application/json")
	io.WriteString(w, `{"message": "You keep using that word. I do not think it means what you think it means."}`)
}

func main() {
	http.HandleFunc("/health", HealthHandler)
}
