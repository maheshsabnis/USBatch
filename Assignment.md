1. Create an Azure Function and take an experience for performing Sql Server Read Operations on Product Table for Searching Products based on follwing criteria
	= Search only by Product Name
		- Function will return product by product name for all manufaturers

	=  Search only by Manufacturer
		- Function will return all products by Manufacturer
	= Search by and and or condition by Manufacturer and ProductName
		- The serach input to function will be as follows
			- string manufacture, string condition, string productname
				- condition values will be OR, AND