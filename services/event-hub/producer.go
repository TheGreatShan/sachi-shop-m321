package main

import (
	"fmt"
	"github.com/confluentinc/confluent-kafka-go/v2/kafka"
)

func ProduceEvent(message string, topic string) {
    fmt.Println("Starting Producer")

    p, err := kafka.NewProducer(&kafka.ConfigMap{"bootstrap.servers": "localhost"})
    if err != nil {
        panic(err)
    }
    defer p.Close()

    go func() {
        for e := range p.Events() {
            switch ev := e.(type) {
            case *kafka.Message:
                if ev.TopicPartition.Error != nil {
                    fmt.Printf("Delivery failed: %v\n", ev.TopicPartition)
                } else {
                    fmt.Printf("Delivered message to %v\n", ev.TopicPartition)
                }
            }
        }
    }()

    p.Produce(&kafka.Message{
        TopicPartition: kafka.TopicPartition{Topic: &topic, Partition: kafka.PartitionAny},
        Value:          []byte(message),
    }, nil)

    p.Flush(15 * 1000)
}