<!doctype html>
<html>
<head>
<meta charset="utf-8">
<title>Demonstrator Details Submission</title>
<style type="text/css">
body,td,th {
	font-family: "Gill Sans", "Gill Sans MT", "Myriad Pro", "DejaVu Sans Condensed", Helvetica, Arial, sans-serif;
	color: #FFFFFF;
}
body {
	background-color: #BE0403;
}
Input{float:right;}
Select{float:right;}
Label{float:left;}
#main {width:480px;}
.field {margin: 5px;}
.phptest{color:#0F4997;}

</style>
<?php

	#connection test
	$conn = sqlsrv_connect('cairo.cms.waikato.ac.nz', array("Database" => "VirtualChair"));
		if( $conn === false )
		{
		     echo "Could not connect.\n";
		     die( print_r( sqlsrv_errors(), true));
		}
	/*else
		{
			echo "connected.\n";
		} */
	
	
	
	
	

#Initializing variables
$varFamName = $varFirstName = $varUsername =  $varAddress = $varDegree = $varMajor = 
$varEnrolled =  $varPreviousCourse = $varPreferred = $varTelPhone =  $varID = $varNumberAge = $varHours = $varGender = $varEmail = $varYear = $varLastYear = "";
#if Submit is clicked, check if all the boxes have been filled in appropriately.
  if(isset($_POST['submit'])) 
  {
	if(isset($_POST['textfieldFamName'])) $varFamName = $_POST['textfieldFamName'];
	if(isset($_POST['textfieldFirstName'])) $varFirstName = $_POST['textfieldFirstName'];
	if(isset($_POST['telPhone'])) $varTelPhone = $_POST['telPhone'];
	if(isset($_POST['numberAge'])) $varNumberAge = $_POST['numberAge'];
	if(isset($_POST['select'])) $varGender = $_POST['select'];
	if(isset($_POST['textfieldUsername'])) $varUsername = $_POST['textfieldUsername'];
	if(isset($_POST['numberID'])) $varID = $_POST['numberID'];
	if(isset($_POST['email'])) $varEmail = $_POST['email'];
	if(isset($_POST['textfieldAddress'])) $varAddress = $_POST['textfieldAddress'];
	if(isset($_POST['textfieldDegree'])) $varDegree = $_POST['textfieldDegree'];
	if(isset($_POST['textfieldMajor'])) $varMajor = $_POST['textfieldMajor'];
	if(isset($_POST['numberYear'])) $varYear = $_POST['numberYear'];
	if(isset($_POST['selectLastYear'])) $varLastYear = $_POST['selectLastYear'];
	if(isset($_POST['textfieldEnrolled'])) $varEnrolled = $_POST['textfieldEnrolled'];
	if(isset($_POST['numberHours'])) $varHours = $_POST['numberHours'];
	if(isset($_POST['textfieldPreviousCourse'])) $varPreviousCourse = $_POST['textfieldPreviousCourse'];
	if(isset($_POST['textfieldPrefered'])) $varPreferred = $_POST['textfieldPrefered'];
	#Simple output check
  /*  print ($varFamName . $varFirstName . $varTelPhone . $varNumberAge . $varGender . $varUsername . $varID . $varEmail . $varAddress);
	print ($varDegree . $varMajor . $varYear . $varLastYear. $varEnrolled . $varHours . $varPreviousCourse . $varPreferred); */
	
	$sql = 'INSERT INTO [VirtualChair].[SCMS\nmr13].Demonstrator (studentID, name, lastName, phoneNo, age, gender, username, summerAddr, major, degree, studyYear, email, hoursWanted, lastYear) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)';
	

	$params = array($varID, $varFirstName, $varFamName, $varTelPhone, $varNumberAge, $varGender, $varUsername, $varAddress, $varMajor, $varDegree, $varYear, $varEmail, $varHours, $varLastYear);

	$stmt = sqlsrv_query($conn, $sql, $params);
	if( $stmt === false )
	{
	     echo "\nError in statement execution.\n";
	     die( print_r( sqlsrv_errors(), true));
	}
	else
	{
	     echo "The query was successfully executed.";
	}

	/* Close connection resources. */
	sqlsrv_free_stmt( $stmt);
	sqlsrv_close( $conn);
  
  }
?>
</head>

<body>

<div id="main">

 
<div id= "logo">
<img src="coat-of-arms.png" width="253" height="75" alt=""/>
</div>
<div id="submitbox" width ="500">

<form action="index.php" method="post">

<div class="field">
  <label for="textfieldFirstName">First Name:</label>
  <input name="textfieldFirstName" type="text" required id="textfieldFirstName" maxlength="200"><br></div> 

<div class="field">
  <label for="textfieldFamName"><span style="">Family</span> Name:</label>
  <input name="textfieldFamName" type="text"  required id="textfieldFamName" maxlength="200"><br></div> 

<div class="field">
  <label for="telPhone">	Phone:</label>
  <input name="telPhone" type="tel"  id="telPhone" maxlength="13"><br></div> 

<div class="field">
  <label for="numberAge">Age:</label>
  <input name="numberAge" type="number"  required id="numberAge" max="130" min="16" step="1"><br></div> 

<div class="field">
  <label for="select">Gender:</label>
  <select name="select"  id="select">
    <option value="Female">Female</option>
    <option value="Male">Male</option>
    <option value="Other">Other</option>
  </select><br></div> 

<div class="field">
  <label for="textfieldUsername">Username:</label>
  <input name="textfieldUsername" type="text" required id="textfieldUsername" placeholder=" eg. xyz8" maxlength="20">	
  <br></div> 


<div class="field">
  <label for="numberID">ID Number:</label>
  <input name="numberID" type="number"  requiredid="numberID" max="9999999" min="0"><br></div> 

<div class="field">
  <label for="email">Email:</label>
  <input name="email" type="email"  required id="email" maxlength="200"><br></div> 

<div class="field">
  <label for="textfieldAddress">Summer Address:</label>
  <input name="textfieldAddress" type="text" id="textfieldAddress" maxlength="200"><br></div> 

<div class="field">
  <label for="textfieldDegree">Degree:</label>
  <input name="textfieldDegree" type="text" required id="textfieldDegree" maxlength="200"><br></div> 

<div class="field">
  <label for="textfieldMajor">Major:</label>
  <input name="textfieldMajor" type="text" required id="textfieldMajor" maxlength="200"><br></div> 

<div class="field">
  <label for="numberYear">Year of Study:</label>
  <input name="numberYear" type="number" required id="numberYear" max="10" min="1"><br></div> 

<div class="field">
  <label for="selectLastYear">Last Year?:</label>
  <select name="selectLastYear" id="selectLastYear">
    <option value=0>No</option>
    <option value=1>Yes</option>
  </select><br></div> 

<div class="field">
  <label for="textfieldEnrolled">Papers Erolled In:</label>
  <input name="textfieldEnrolled" required type="text" id="textfieldEnrolled"><br></div> 

<div class="field">
  <label for="numberHours">Number of Hours Wanted:</label>
  <input type="number" name="numberHours" required id="numberHours"><br></div> 

<div class="field">
  <label for="textfieldPreviousCourse">Previous Demo Experience:</label>
  <input type="text" name="textfieldPreviousCourse" id="textfieldPreviousCourse"><br></div> 

<div class="field">
  <label for="textfieldPrefered">Preferred Classes to Demo:</label>
  <input name="textfieldPrefered" type="text" required id="textfieldPrefered"><br></div> 

<div class="field">
  <input name="submit" type="submit" id="submit" title="Submit" value="Submit">
<br></div>
</form>

</div>
</div>
</div>

</body>
</html>