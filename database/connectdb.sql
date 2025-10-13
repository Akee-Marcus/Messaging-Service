<?php
-- database configuration
    $username = "your_name";
    $password = "your_password";
    $hostname = "localhost";

-- connection to the database
    $dbConnect = mysql_connect($hostname,$username,$password)
    or die("Unable toconnecttoMySQL");
    echo "ConnectedtoMySQL";
-- select a specific database
    $dbSelect=mysql_select_db("dbName",$dbConnect)
    or die("Could not select dbName");
    echo "SelecteddbName";
 ?>

$username="your_name";
$password="your_password";
$hostname="localhost";
-- create connection
 $dbConnect= newmysqli($hostname,$username,$password);
-- check connection
 if($dbConnect->connect_error){
 die("Connectionfailed:".$dbConnect->connect_error);
 }
 echo"Connectedsuccessfully";
-- selectaspecificdatabase
 $mysqli->select_db("dbName");
