<!DOCTYPE html>
<!--
Proof of concept
Created by Ricky on 3/03/17
-->
<html>
    <head>
        <meta charset="UTF-8">
        <title></title>
    </head>
    <body>
        <?php
        echo "Hello world! <br>";
        $servername = "localhost";
        $username = "root";
        $password = '';
        $dbname = "myDB";
        
        // Create connection
        $connect = mysqli_connect($servername, $username, $password);
        
        // Check connection
        if ($connect->connect_error)
            die("Connection failed: " . mysqli_error($connect));
        echo "Connected successfully <br>";
        
        // Create database
        $sql = "CREATE DATABASE IF NOT EXISTS myDB";
        if(mysqli_query($connect, $sql))
            echo "Database created successfully <br>";
        else
            echo "Error creating datababse: " . mysqli_error($connect) . "<br>";
        
        // Create connection
        $connect = mysqli_connect($servername, $username, $password, $dbname);
        // Check connection
        if (!$connect) 
            die("Connection failed: " . mysqli_connect_error());
        
        $sql = "CREATE TABLE IF NOT EXISTS PHOTO(
        PhotoID int(16) NOT NULL,
        Title varchar(64),
        Author varchar(64),
        Date DATE,
        Event varchar(64),
        EventType varchar(64),
        Link varchar(256) NOT NULL
        )";
        
        if(mysqli_query($connect, $sql))
            echo "Table PHOTO created successfully <br>";
        else
            echo "Error creating table PHOTO: " . mysqli_error($connect) . "<br>";
        
        $sql = "CREATE TABLE IF NOT EXISTS TAG(
        PhotoID int(16) NOT NULL,
        Tag varchar(64)
        )";
        
        if(mysqli_query($connect, $sql))
            echo "Table TAG created successfully <br>";
        else
            echo "Error creating table TAG: " . mysqli_error($connect) . "<br>";
        
        // Clear table each run for testing purposes
        $sql = "TRUNCATE TABLE PHOTO";
        if(mysqli_query($connect, $sql))
            echo "PHOTO table cleared <br>";
        else
            echo "Failed to clear PHOTO table: " . mysqli_error($connect) . "<br>";
        
        $sql = "TRUNCATE TABLE TAG";
        if(mysqli_query($connect, $sql))
            echo "TAG table cleared <br>";
        else
            echo "Failed to clear TAG table: " . mysqli_error($connect) . "<br>";
        
        // Insert a test record
        $sql = "INSERT INTO PHOTO(PhotoID, Title, Author, Date, Event, EventType, Link)
            VALUES ('0', 'Cool Picture', 'John', '20170305', 'Fun Event', 'test', 'http//:LinkHere')";
        if(mysqli_query($connect, $sql))
            echo "New record successfully inserted <br>";
        else
            echo "Error inserting record: " . mysqli_error($connect) . "<br>";
        {
            // Insert tags
            $sql = "INSERT INTO TAG(PhotoID, Tag)
                VALUES ('0', '#fun')";
            if(mysqli_query($connect, $sql))
                echo "New tag successfully inserted <br>";
            else
                echo "Error inserting tag: " . mysqli_error($connect) . "<br>";
            
            $sql = "INSERT INTO TAG(PhotoID, Tag)
                VALUES ('0', '#cool')";
            if(mysqli_query($connect, $sql))
                echo "New tag successfully inserted <br>";
            else
                echo "Error inserting tag: " . mysqli_error($connect) . "<br>";
        }
            
        // Insert a test record
        $sql = "INSERT INTO PHOTO(PhotoID, Title, Author, Date, Event, EventType, Link)
            VALUES ('01', 'Nice Picture', 'Joe', '', 'Fun Event', 'test', 'http//:DifferentLinkHere')";
        if(mysqli_query($connect, $sql))
            echo "New record successfully inserted <br>";
        else
            echo "Error inserting record: " . mysqli_error($connect) . "<br>";
        
        // Insert a test record
        $sql = "INSERT INTO PHOTO(PhotoID, Title, Author, Date, Event, EventType, Link)
            VALUES ('003', 'Awesome Picture', '', '', 'Amazing Event', '', 'http//:AnotherLinkHere')";
        if(mysqli_query($connect, $sql))
            echo "New record successfully inserted <br>";
        else
            echo "Error inserting record: " . mysqli_error($connect) . "<br>";
        {
            // Insert tags
            $sql = "INSERT INTO TAG(PhotoID, Tag)
                VALUES ('3', '#fun')";
            if(mysqli_query($connect, $sql))
                echo "New tag successfully inserted <br>";
            else
                echo "Error inserting tag: " . mysqli_error($connect) . "<br>";
        }

        // List distinct event names
        $sql = "SELECT DISTINCT Event FROM PHOTO";
        $result = mysqli_query($connect, $sql);
        
        if (mysqli_num_rows($result) > 0)    
        {
            echo "<ul>";
            while($row = $result->fetch_assoc())
            {
                echo "<li>";
                echo "<div class=\"collapsible-header\" >" . $row['Event'] . "</div>" ; //prints event name
                
                // grab table values from distinct event
                $sql2 = "SELECT * FROM PHOTO WHERE Event='" . $row['Event'] . "'";
                $result2 = mysqli_query($connect, $sql2);
                if (mysqli_num_rows($result2) > 0)    
                {
                    echo "<ul>";
                    while($row2 = $result2->fetch_assoc())
                    {
                        echo "<li>";
                        echo "<a href = " . $row2['Link'] . ">" . $row2['Title'] . "</a>" ; // prints hyperlink with photo title
                    }
                    echo "</ul>";
                }
                else
                    echo "0 results <br>";
            }
            echo "</ul>";
        }
        else
            echo "0 results <br>";
    
        echo "I want to see the pictures with '#fun'!!!";
        $sql = "SELECT * FROM TAG WHERE Tag='#fun'";
        $result = mysqli_query($connect, $sql);
        
        if (mysqli_num_rows($result) > 0)    
        {
            while($row = $result->fetch_assoc())
            {
                $sql2 = "SELECT * FROM PHOTO WHERE PhotoID='" . $row['PhotoID'] . "'";
                $result2 = mysqli_query($connect, $sql2);
                
                if (mysqli_num_rows($result2) > 0)    
                {
                    echo "<ul>";
                    while($row2 = $result2->fetch_assoc())
                    {
                        echo "<li>";
                        echo "<a href = " . $row2['Link'] . ">" . $row2['Title'] . "</a>" ; // prints hyperlink with photo title
                    }
                    echo "</ul>";
                }
                else
                    echo "0 results <br>";
            }
        }
        ?>
    </body>
</html>