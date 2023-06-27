CREATE DATABASE AviaBooking;

USE AviaBooking;

CREATE TABLE Airlines (
    airline_id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(50),
    rating FLOAT
);

CREATE TABLE Clients (
  client_id INT IDENTITY(1,1) PRIMARY KEY,
  nickname VARCHAR(30),
  name VARCHAR(50),
  surname VARCHAR(50),
  email VARCHAR(50),
  password VARCHAR(256),
  role VARCHAR(10)
);

CREATE TABLE Flights (
    flight_id INT IDENTITY(1,1) PRIMARY KEY,
    airline_id INT,
    departure VARCHAR(50),
    arrival VARCHAR(50),
    departure_date DATE,
    arrival_date DATE,
    seats_available INT,
    class VARCHAR(10),
    FOREIGN KEY (airline_id) REFERENCES Airlines (airline_id)
);


CREATE TABLE Payments (
    payment_id INT IDENTITY(1,1) PRIMARY KEY,
    client_id INT,
    flight_id INT,
    payment_date DATE,
    FOREIGN KEY (client_id) REFERENCES Clients(client_id),
    FOREIGN KEY (flight_id) REFERENCES Flights(flight_id)
);

CREATE TABLE Passengers (
  passenger_id INT NOT NULL PRIMARY KEY IDENTITY,
  first_name VARCHAR(50) NOT NULL,
  last_name VARCHAR(50) NOT NULL,
  gender CHAR(1) NOT NULL CHECK (gender IN ('Ì', 'Æ')),
  birth_date DATE NOT NULL,
  flight_id INT NOT NULL,
  FOREIGN KEY (flight_id) REFERENCES Flights(flight_id)
);

CREATE TABLE Reviews
(
    id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    airline_id int NOT NULL,
    client_id int NOT NULL,
    review_text varchar(500) NOT NULL,
	score int NOT NULL,
    FOREIGN KEY (airline_id) REFERENCES Airlines(airline_id),
    FOREIGN KEY (client_id) REFERENCES Clients(client_id)
);

DROP table Reviews;
