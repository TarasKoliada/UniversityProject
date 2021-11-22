create database Online_Food_Ordering;
use Online_Food_Ordering;

CREATE TABLE Customer(
	customer_id INT IDENTITY(1, 1) NOT NULL,
	customer_surname VARCHAR(30) NOT NULL,
	customer_first_name VARCHAR(30) NOT NULL,
	customer_middle_name VARCHAR(30) NOT NULL,
	customer_email VARCHAR(50) NOT NULL,
	customer_phone_number VARCHAR(15) NOT NULL,
	customer_username VARCHAR(30) NOT NULL,
	customer_password VARCHAR(30) NOT NULL,
	customer_account_status BIT NOT NULL
)
ALTER TABLE Customer
ADD CONSTRAINT PK_customer_id PRIMARY KEY CLUSTERED (customer_id);

ALTER TABLE Customer 
ADD CONSTRAINT customer_username UNIQUE (customer_username);



CREATE TABLE Site_Info(
	site_id INT IDENTITY(1, 1) NOT NULL,
	site_name VARCHAR(30) NOT NULL,
	site_description VARCHAR(100),
	site_contact_info VARCHAR(15) NOT NULL,
	company_adress VARCHAR(100) NOT NULL,
	last_update DATETIME NOT NULL
)
ALTER TABLE Site_Info
ADD CONSTRAINT PK_site_id PRIMARY KEY CLUSTERED (site_id);

ALTER TABLE Site_Info 
ADD CONSTRAINT last_update DEFAULT (getdate()) FOR last_update;



CREATE TABLE Employee(
	employee_id INT IDENTITY(1, 1) NOT NULL,
	site_id INT NOT NULL,
	employee_surname VARCHAR(30) NOT NULL,
	employee_first_name VARCHAR(30) NOT NULL,
	employee_middle_name VARCHAR(30) NOT NULL,
	email_adress VARCHAR(50) NOT NULL,
	employee_contact VARCHAR(15) NOT NULL,
	system_login VARCHAR(30) NOT NULL,
	system_password VARCHAR(30) NOT NULL
)
ALTER TABLE Employee
ADD CONSTRAINT PK_employee_id PRIMARY KEY CLUSTERED (employee_id);

ALTER TABLE Employee
ADD CONSTRAINT FK_Employee_To_SiteInfo
FOREIGN KEY (site_id) REFERENCES Site_Info(site_id);

ALTER TABLE Employee ADD employee_preferences BIT NOT NULL;

ALTER TABLE Employee 
ADD CONSTRAINT system_login UNIQUE (system_login);



CREATE TABLE Orders(
	order_id INT IDENTITY(1, 1) NOT NULL,
	customer_id INT NOT NULL,
	order_date DATETIME NOT NULL,
	total_price FLOAT NOT NULL,
	order_status BIT NOT NULL,
	employee_id INT NOT NULL
)
ALTER TABLE Orders
ADD CONSTRAINT PK_order_id PRIMARY KEY CLUSTERED (order_id);

ALTER TABLE Orders
ADD CONSTRAINT FK_Orders_To_Employee
FOREIGN KEY (employee_id) REFERENCES Employee(employee_id);

ALTER TABLE Orders
ADD CONSTRAINT FK_Orders_To_Customer
FOREIGN KEY (customer_id) REFERENCES Customer(customer_id);

ALTER TABLE Orders
ADD CONSTRAINT order_date DEFAULT (getdate()) FOR order_date;



CREATE TABLE Order_details(
	order_details_id INT IDENTITY(1, 1) NOT NULL,
	order_id INT NOT NULL,
	dish_id INT NOT NULL,
	number_od_servings INT NOT NULL,
	total_dish_price FLOAT NOT NULL
)
ALTER TABLE Order_details
ADD CONSTRAINT PK_order_details_id PRIMARY KEY CLUSTERED (order_details_id);

ALTER TABLE Order_details
ADD CONSTRAINT FK_Order_details_To_Orders
FOREIGN KEY (order_id) REFERENCES Orders(order_id);

ALTER TABLE Order_details
ADD CONSTRAINT FK_Order_details_To_Dish
FOREIGN KEY (dish_id) REFERENCES Dish(dish_id);

ALTER TABLE Order_details ADD note VARCHAR(100) NULL;




CREATE TABLE Dish(
	dish_id INT IDENTITY(1, 1) NOT NULL,
	dish_name VARCHAR(50) NOT NULL,
	dish_price FLOAT NOT NULL,
	menu_type_id INT NOT NULL,
	dish_ingredients VARCHAR NOT NULL,
	dish_status BIT NOT NULL
)
ALTER TABLE Dish
ADD CONSTRAINT PK_dish_id PRIMARY KEY CLUSTERED (dish_id);

ALTER TABLE Dish
ADD CONSTRAINT FK_Dish_To_Menu_type
FOREIGN KEY (menu_type_id) REFERENCES Menu_type(menu_type_id);

ALTER TABLE Dish
ALTER COLUMN dish_ingredients VARCHAR(100) NOT NULL;

ALTER TABLE Dish ADD dish_weight VARCHAR(100) NOT NULL;





CREATE TABLE Menu_type(
	menu_type_id INT IDENTITY(1, 1) NOT NULL,
	menu_type_name VARCHAR(30) NOT NULL,
	type_description VARCHAR NOT NULL
)
ALTER TABLE Menu_type
ADD CONSTRAINT PK_menu_type_id PRIMARY KEY CLUSTERED (menu_type_id);

ALTER TABLE Menu_type ADD site_id INT NOT NULL;




CREATE TABLE Payment(
	payment_id INT IDENTITY(1, 1) NOT NULL,
	order_id INT NOT NULL,
	price FLOAT NOT NULL,
	paid_by VARCHAR NOT NULL,
	payment_date DATETIME NOT NULL,
	employee_id INT NOT NULL
)

ALTER TABLE Payment
ADD CONSTRAINT PK_payment_id PRIMARY KEY CLUSTERED (payment_id);

ALTER TABLE Payment
ADD CONSTRAINT FK_Payment_To_Orders
FOREIGN KEY (order_id) REFERENCES Orders(order_id);

ALTER TABLE Payment
ADD CONSTRAINT FK_Payment_To_Employee
FOREIGN KEY (employee_id) REFERENCES Employee(employee_id);

ALTER TABLE Payment
ADD CONSTRAINT payment_date DEFAULT (getdate()) FOR payment_date;

ALTER TABLE Payment
ALTER COLUMN paid_by VARCHAR(50) NOT NULL;




CREATE TABLE Rating(
	rating_id INT IDENTITY(1, 1) NOT NULL,
	dish_id INT NOT NULL,
	score VARCHAR(1) NOT NULL, 
	remarks VARCHAR NOT NULL,
	recorded_date DATETIME NOT NULL,
	customer_id INT NOT NULL
)

ALTER TABLE Rating
ADD CONSTRAINT PK_rating_id PRIMARY KEY CLUSTERED (rating_id);

ALTER TABLE Rating
ADD CONSTRAINT FK_Rating_To_Dish
FOREIGN KEY (dish_id) REFERENCES Dish(dish_id);

ALTER TABLE Rating
ADD CONSTRAINT FK_Rating_To_Customer
FOREIGN KEY (customer_id) REFERENCES Customer(customer_id);

ALTER TABLE Rating
ADD CONSTRAINT recorded_date DEFAULT (getdate()) FOR recorded_date;





CREATE TABLE Administrator(
	admin_id INT IDENTITY(1, 1) NOT NULL,
	site_id INT NOT NULL,
	admin_name VARCHAR(30) NOT NULL,
	admin_surname VARCHAR(30) NOT NULL,
	admin_middle_name VARCHAR(30) NOT NULL,
	admin_login VARCHAR(100) NOT NULL,
	admin_password VARCHAR(100) NOT NULL,
	admin_contact VARCHAR(50) NOT NULL,
	admin_preferences BIT NOT NULL
)

ALTER TABLE Administrator
ADD CONSTRAINT PK_admin_id PRIMARY KEY CLUSTERED (admin_id);

ALTER TABLE Administrator
ADD CONSTRAINT FK_Admin_To_Site
FOREIGN KEY (site_id) REFERENCES Site_Info(site_id);

ALTER TABLE Administrator 
ADD CONSTRAINT admin_login UNIQUE (admin_login);


ALTER TABLE Menu_type
ADD CONSTRAINT FK_Type_to_Site
FOREIGN KEY (site_id) REFERENCES Site_Info(site_id);






INSERT INTO Site_Info(site_name, site_description, site_contact_info, company_adress) 
VALUES ('BLACK & WHITE', 'Our restaurant offers a small but very tasty selection of dishes that will not leave anyone indifferent.', 'Mail: BlackAndWhite-offers@gmail.com, Hotline: +380672563715', 'Ukraine, Lviv, vul. Knyazya Romana 6');


INSERT INTO Site_Info(site_name, site_description, site_contact_info, company_adress) 
VALUES ('LOFT Pizza-bar', 'The place, which will surprise you with the delicacy of our dishes and will become your favorite place.', 'Mail: LoftPizzaBar_line@gmail.com, Hotline: +380986743698', 'Ukraine, Lviv, Market Square 44');


INSERT INTO Menu_type(menu_type_name, type_description, site_id) VALUES ('Japanese dishes', 'Presented all types of japanese cuisine', 1), ('Ukrainian dishes', 'Presented all types of ukrainian cuisine', 1), ('Russian dishes', 'Presented all types of russian cuisine', 1);

INSERT INTO Menu_type(menu_type_name, type_description, site_id) VALUES ('Japanese dishes', 'Presented all types of japanese cuisine', 2), ('Italian dishes', 'Presented all types of italian cuisine', 2), ('Russian dishes', 'Presented all types of russian cuisine', 2);



INSERT INTO Dish(dish_name, dish_price, dish_ingredients, dish_status, dish_weight, menu_type_id) VALUES
('Sushi California', 10, 'rice', 1, 200, 4),
('Varenyky with Cheese', 5, 'Cheese', 1, 500, 2),
('Sushi Losos', 15, 'Meat', 1, 180, 1),
('Pizza Bavaria', 11, 'Salami', 1, 670, 5),
('Pampushki', 20, 'Miaso', 1, 330, 3),
('Draniki', 3, 'Potato', 2, 100, 6)



INSERT INTO Administrator(site_id, admin_name, admin_surname, admin_middle_name, admin_login, admin_password, admin_contact, admin_preferences)
VALUES
(1, 'Artem', 'Poliakov', 'Ivanovych', 'artempoliakov01', 'admin01', '+380675621379', 1),
(2, 'Victor', 'Lebedev', 'Maksymovych', 'victorlebedev02', 'admin02', '+380982351783', 0),
(1, 'Anton', 'Bondar', 'Olehovych', 'antonbondar01', 'admin01', '+380689325561', 0),
(2, 'Bohdan', 'Pantushenko', 'Tarasovych', 'bohdanpantushenko02', 'admin02', '+380973318943', 1)

delete from Orders;
DBCC CHECKIDENT ('Orders', RESEED, 0);

delete from Customer;
DBCC CHECKIDENT ('Customer', RESEED, 0);

delete from Order_details;
DBCC CHECKIDENT ('Order_details', RESEED, 0);

delete from Employee;
DBCC CHECKIDENT ('Employee', RESEED, 0);

delete from Payment;
DBCC CHECKIDENT ('Payment', RESEED, 0);