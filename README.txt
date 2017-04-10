Set-up Database
	 Requires:
	  Microsoft SQL Server
	  
	 Create Database:
	  Open Database Manager from 'All Programs > Microsoft SQL Server 2008 R2 > Database Manager'
	  Click Create Table enter desired database name e.g. Scheduler. take note of the database name now displayed 
	   in the list box as it will be now in the form of 'username_DatabaseName' e.g.(nmr13_Scheduler)
	  Alternatively if you have the correct permissions just run 'CREATE DATABSE' 
	 
	 Create Tables 
	  Open 'createTables_MSSQL.sql' using Microsoft SQL Server Management Studio 
	  When prompted connect to desired SQL server
	  From the 'Available Databases' drop down box select the database you created in the last step
	  Click 'Query > Execute' to execute the script to create all the tables
	  
	 Populate Database:
	  Open 'populateLabs.sql' in MS SQL Server 
	  When prompted connect to desired SQL server
	  From the 'Available Databases' drop down box select the database you created in the last step
	  Click 'Query > Execute'
	  
	 Multiple Users:
	  (REQUIRED IN R BLOCK LABS)
	  Depending on permissions when setting up the MS SQL server you may have to run
	  edit 'createSynonms.sql' replace the word 'username' with the username of the person who created the database intially.
	  Execute 'createSynonms.sql' on new each new user the wishes to access the database.
  
Set-Up Web App using IIS:
	 Open 'index.php' using a text editor and change the values 'cairo.cms.waikato.ac.nz' to your SQL server location 
	 then change VirtualChair to the Database you created in the first step on the line containing 
	'$conn = sqlsrv_connect('cairo.cms.waikato.ac.nz', array("Database" => "VirtualChair"));' 
	 Move the contents of 'Web Application' into '\wwww\wwwroot\'

 
 

