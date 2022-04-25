CREATE TABLE Product (
	ProductId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Name varchar(25) NOT NULL unique,
	Description varchar(50) NOT NULL,
	Price decimal(8,2) NOT NULL CHECK(Price >= 0.00)
);

CREATE TABLE OrderState (
	OrderStateId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	State varchar(20) NOT NULL
);

CREATE TABLE Stock (
	StockId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	ProductId int NOT NULL REFERENCES Product(ProductId) ON UPDATE CASCADE ON DELETE CASCADE,
	AvailableStock int NOT NULL CHECK(AvailableStock >= 0)
);

CREATE TABLE [Order] (
	OrderId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	ProductId int NOT NULL REFERENCES Product(ProductId) ON UPDATE CASCADE ON DELETE CASCADE,
	Name varchar(25) NOT NULL unique,
	CreatedDateUTC DateTime NOT NULL,
	Quantity int NOT NULL CHECK(Quantity >= 0),
	OrderStateId int NOT NULL REFERENCES OrderState(OrderStateId) ON UPDATE CASCADE ON DELETE CASCADE
);