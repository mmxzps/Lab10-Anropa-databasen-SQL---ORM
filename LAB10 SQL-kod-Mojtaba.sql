----skapa i SSMS-SQL

--Hämta alla produkter med deras namn, pris och kategori namn. Sortera på kategori namn och sen produkt namn
select ProductName,UnitPrice,CategoryName from Products 
join Categories on Products.CategoryID = Categories.CategoryID
order by CategoryName, ProductName

--Hämta alla kunder och antal ordrar de gjort. Sortera fallande på antal ordrar.
select CompanyName, count(Customers.CustomerID) as AmountOrders from Customers
join Orders on Customers.CustomerID = Orders.CustomerID
group by CompanyName
order by AmountOrders desc

--Hämta alla anställda tillsammans med territorie de har hand om (EmployeeTerritories och Territories tabellerna)
select FirstName, LastName, Title, TerritoryDescription from Employees
join EmployeeTerritories on Employees.EmployeeID = EmployeeTerritories.EmployeeID
join Territories on EmployeeTerritories.TerritoryID = Territories.TerritoryID

--Extra utmaning
select CompanyName,((UnitPrice*Quantity) *(1-Discount)) as totalCost from Customers
join Orders on Customers.CustomerID = Orders.CustomerID
join [Order Details] on Orders.OrderID = [Order Details].OrderID
order by totalCost desc
