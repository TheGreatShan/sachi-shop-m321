DROP DATABASE IF EXISTS inventory;
CREATE DATABASE inventory;

USE inventory;

-- Create table for Product
CREATE TABLE Product (
                         id UUID PRIMARY KEY,
                         product VARCHAR(255) NOT NULL,
                         description TEXT,
                         stock INT NOT NULL DEFAULT 0,
                         price FLOAT NOT NULL DEFAULT 0
);

-- Create table for Information
CREATE TABLE Information (
                             id UUID PRIMARY KEY,
                             productId UUID NOT NULL,
                             information TEXT NOT NULL,
                             stage VARCHAR(255),
                             FOREIGN KEY (productId) REFERENCES Product(id)
);
