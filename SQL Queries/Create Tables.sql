create table Transaction_Type (
id serial primary key NOT NULL unique,
transaction_type varchar not null unique
);

create table Transaction_Categories (
id serial primary key not null unique,
category varchar not null unique,
transaction_type_id integer references Transaction_Type (id)
);

create table Wallets (
id serial primary key not null unique,
wallet varchar unique not null
balance decimal (10, 2) not null default 0.00 check(balance >= 0.00)
);

create table Transactions (
id serial primary key not null unique,
transaction_category_id integer references Transaction_Categories (id) not null,
wallet_id integer references Wallets (id) not null,
amount DECIMAL(10, 2) not null,
created_at timestamp not null
);