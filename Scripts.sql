Create Table Products(
  ProductRowId int Identity Primary Key,
  ProductId varchar(20) Unique,
  ProductName varchar(400) Not Null,
  Manufacturere varchar(400) Not Null,
  Price int Not Null 
)