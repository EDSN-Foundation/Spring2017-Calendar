[INSTALLATION INSTRUCTIONS]
MySQL Website->
Downloads->
Community->
MySQL Community Server (Get the msi installer if you're using windows)->
Choose Platform->
Download (You dont have to make account when it asks, link on bottom of page)->

Run Downloaded installer
Setup Type: Developer Default
Execute
Config Type: Development Machine
MySQL Root Password: pass 
(if we dont all have the same password we'll have to keep our own settings, so easier to do this)
All defaults for the rest, execute.
user: root
password: pass

[USING IT]
At the end of install it'll start the workbench which is a way to interact directly with the DB
There should be a MySqlConnection that you can use to connect to your database. Click on that.
To confirm everything is working, do the following:
On the right you'll see some default schemas "sakila", "sys", "world"
open the sakila folder with the arrow and open the tables folder and you'll see tome basic tables "actor" "address" etc. Right click on any of them and select the top option "Select Rows - Limit 1000"

If your server is up and your connection is good.. you should get a table of results. Hurray

To create our schema run our CreateSchema.sql file found In this same folder.
File->Open SQL Script->Execute(thunderbolt icon)

IMPORTANT: if we make ANY changes to the schema everyone will have to on their own run the create schema file again. The file is made you can it as many time as you want safely and it'll remake the database.
HOWEVER YOU WILL LOSE ANY CHANGES YOU HAVE MADE TO IT YOURSELF

[Example Create Event Call]
SQLData.SQLQueries.InsertEvent("TestTitle", DateTime.Now, "", "", true, "VenueName", "Address25463636 346356457", "Deescription blah blah blah", "NameHere", "EmailHur", "", "URL", "", "", "", "");