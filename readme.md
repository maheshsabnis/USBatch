Server=tcp:MyDbServer;Initial Catalog=MyCompany;Persist Security Info=False;User ID=MaheshAdmin;Password=P@MyPwd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;

1. Storage Service
	- Foundantional Services
		- Table, Schemaless NoSQL Service
		- BLOB, Test, Stream type of data
			- used for data files for Data Operations as will as analysis
			- have Video and Audio files for streaming
			- Apllication Level Log
				- Audit Logs  
		- Queue, Messaging
		- Azure Files
		- Azure Disks
		- Azure Elestic Storage Access Network (Preview) 
	- Database
2. Package for Accessing Storage in App
	-  Microsoft.Storage.{STORAGE-TYPE}
	- e.g.
		- Microsoft.Storage.Queues	