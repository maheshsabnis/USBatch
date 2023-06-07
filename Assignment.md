1. Create an Azure Function and take an experience for performing Sql Server Read Operations on Product Table for Searching Products based on follwing criteria
	= Search only by Product Name
		- Function will return product by product name for all manufaturers

	=  Search only by Manufacturer
		- Function will return all products by Manufacturer
	= Search by and and or condition by Manufacturer and ProductName
		- The serach input to function will be as follows
			- string manufacture, string condition, string productname
				- condition values will be OR, AND
				1. 

2. Using The Blob Triggers and Queue Trigger
 - Modify the HTTP API creatyed usig HttpTrigger in Assignment 1 (above) as follows
	- Create a Post Request to Accept the Products Infromation 
	- Reader the Posted Record for the Product and read the Category
	- Based on the Category Name, create queues and add the data for Each Category in seperate Queue
	- Create Azure Function based on QueueTriggers and these functions will Read data from queue for Each Category and write the data in respective Table in Azure SQL Serverless Instance
- Create a Azure Function that will read all .txt files from BLOB Storage and if the first line in the file is containing text as 'Record No.', then read contents of the file and print on console  
	