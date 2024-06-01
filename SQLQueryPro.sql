Create table products 
(
	prID int primary key identity,
	prName varchar(50),
	prPrice float,
	CategoryID int,
	prImage image
)

Select prID,prName,prPrice, CategoryID, c.catName from products p inner join category c on c.catID = p.CategoryID