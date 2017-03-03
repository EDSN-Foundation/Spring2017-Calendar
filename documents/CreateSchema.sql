-- CREATE SCHEMA TEST
-- BY : ADAM BURTON
CREATE SCHEMA IF NOT EXISTS edsncalendar;
CREATE SCHEMA IF NOT EXISTS edsncalendaradmin;

DROP TABLE IF EXISTS preselectedcalendarfilters;
DROP TABLE IF EXISTS eventproperties;
DROP TABLE IF EXISTS property;
DROP TABLE IF EXISTS propertytype;
DROP TABLE IF EXISTS calendarevent;
DROP TABLE IF EXISTS calendarsettings;

CREATE TABLE propertytype
(
	iPropertyTypeId INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    vPropertyType VARCHAR(100),
    bActive BIT DEFAULT 1
);

CREATE TABLE property
(
	iPropertyId INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    iPropertyTypeId INT NOT NULL,
    vProperty VARCHAR(100),
    bActive BIT DEFAULT 1,
    FOREIGN KEY (iPropertyTypeId)
		REFERENCES propertytype(iPropertyTypeId)
        ON DELETE CASCADE
);

CREATE TABLE calendarevent
(
	iEventId INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    vEventTitle VARCHAR(255),
    dEventDate VARCHAR(50),
    dEndDate VARCHAR(50),
    vStartTime VARCHAR(50),
    vEndTime VARCHAR(50),
    bAllDay BIT,
    vVenueName VARCHAR(255) NOT NULL,
    vAddress VARCHAR(255) NOT NULL,
    vDescription VARCHAR(3000) NOT NULL,
    mbImage MEDIUMBLOB DEFAULT NULL,
    vOrganizerName VARCHAR(100) NOT NULL,
    vOrganizerEmail VARCHAR(50) NOT NULL,
    vOrganizerPhoneNumber VARCHAR(20) NOT NULL,
    vOrganizerURL VARCHAR(255),
    vCost VARCHAR(50),
    vRegistrationURL VARCHAR(255),
    vSubmitterName VARCHAR(100),
    vSubmitterEmail VARCHAR(50),
    dtPostDate DATETIME DEFAULT NOW(),
    dtPublishDate DATETIME DEFAULT NULL,
    bPublished BIT DEFAULT 0,
    bActive BIT DEFAULT 1
);

CREATE TABLE eventproperties
(
	iEventPropertyId INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    iEventId INT NOT NULL,
    iPropertyId INT NOT NULL,
    FOREIGN KEY (iEventId)
		REFERENCES calendarevent(iEventId)
        ON DELETE CASCADE,
	FOREIGN KEY (iPropertyId)
		REFERENCES property(iPropertyId)
        ON DELETE CASCADE
);

CREATE TABLE calendarsettings
(
	bFilterEnabled BIT DEFAULT 1,
	bMonthEnabled BIT DEFAULT 1,
    bPosterEnabled BIT DEFAULT 1,
    bListEnabled BIT DEFAULT 1,
    sDefault VARCHAR(6) DEFAULT 'Poster',
    sGlobalHeader VARCHAR(20000) DEFAULT NULL
);

CREATE TABLE preselectedcalendarfilters
(
	iPropertyId INT NOT NULL PRIMARY KEY,
    FOREIGN KEY (iPropertyId)
		REFERENCES property(iPropertyId)
        ON DELETE CASCADE
);

INSERT INTO propertytype(vPropertyType)
VALUES('Categories'),('Tags'),('Region'),('Country'),('Programs');

INSERT INTO property(iPropertyTypeId,vProperty)
VALUES(1, 'Movies'),(1,'Religion'),(2,'#Fitness'),(2,'#Community'),(2,'Featured'),(3, 'Hartford, Ct'),(4,'USA'),(4,'Canada'),(5,'Birthday'),(1, 'Pool');

INSERT INTO calendarevent(vEventTitle, dEventDate, dEndDate, vStartTime, vEndTime, bAllDay, vVenueName, vAddress, vDescription, vOrganizerName,
						   vOrganizerEmail, vOrganizerPhoneNumber, vOrganizerURL, vCost, vRegistrationURL, vSubmitterName, vSubmitterEmail, dtPublishDate ,bPublished, bActive, mbImage)
VALUES('Friendship Group of Hartford', '2016-12-12', '2016-12-12', '2016-12-12T21:00:00', NULL, 0, 'Sportsmans Athletic Club', '2976 Main St, Hartford, CT 06120, USA', '', '',
	   '', '', NULL, 'FREE', NULL, 'Adam', 'AdamEmail@email.com', '2016-12-1',1, 1, NULL),
	  ('Pool Night', '2016-12-13', '2016-12-13', '2016-12-13T19:00:00', NULL, 0, 'West Indian Social Club of Hartford', '3340 Main St, Hartford, CT 06120, USA', '', '',
	   '', '954-410-9999', NULL, 'FREE', NULL, 'Adam', 'AdamEmail@email.com', '2016-12-5',1, 1, NULL),
	  ('Community Birthday Friday', '2016-12-16', '2016-12-16', '2016-12-16T8:00:00', NULL, 0, 'West Indian Social Club of Hartford', '3340 Main St, Hartford, CT 06120, USA', '', '',
	   '', '', NULL, NULL, NULL, 'Adam', 'AdamEmail@email.com', '2016-12-4',1, 1, NULL),
	  ('Friendship Group of Hartford', '2016-12-15', '2016-12-15', NULL, NULL, 1, 'American Legion', '2121 Main St, Hartford, CT 06120, USA', '', '',
	   '', '', NULL, 'FREE', NULL, 'Adam', 'AdamEmail@email.com', '2016-12-3',1, 1, NULL),
	  ('Boxing Day', '2016-12-26', '2016-12-26', NULL, NULL, 1, '', '', 'Boxing Day is celebrated in U.K, Canada, Caribbean and other places around the world', '',
	   '', '', NULL, 'FREE', NULL, 'Adam', 'AdamEmail@email.com', '2016-12-2',1, 1, NULL),
	  ('WISC Christmas Party', '2016-12-10', '2016-12-10', '2016-12-10T18:00:00','2016-12-10T18:00:00', 0, 'West Indian Social Club of Hartford', '2121 Main St, Hartford, CT 06120, USA', '', '',
	   '', '', NULL, NULL, NULL, 'Adam', 'AdamEmail@email.com', '2016-12-5',1, 1, NULL),
	  ('Kwanzaa: A Celebration of Family, Community and Culture', '2016-12-26', '2017-1-1', '2016-12-26T24:00:00', '2016-12-26T24:00:00', 0, '', 'Nowhere St, Nowhere, 06062', 'Kwanzaa holiday was created to introduce and reinforce seven 
																																													    principles which are believed to be the core value systems fro 
                                                                                                                                                                                        the healthy and thriving families, stable and loving and caring 
                                                                                                                                                                                        relationships, effective parenting practices, school achievement, 
                                                                                                                                                                                        non violent, safe and productive communities. 
                                                                                                                                                                                        The seven days of Kwanzaa holiday is organized around seven principles.', '',
	   '', '', NULL, 'FREE', NULL, 'Adam', 'AdamEmail@email.com', '2016-12-7',1, 1, NULL),
	  ('Caribbean Grand Market', '2016-12-24', '2016-12-24', '2016-12-24T19:00:00', NULL, 1, '', '', '', '',
	   '', '', NULL, 'FREE', NULL, 'Adam', 'AdamEmail@email.com', '2016-12-1',1, 1, NULL);
       
INSERT INTO eventproperties(iEventId, iPropertyId)
VALUES(4,5),(8,5),(7,5),(2,10);

INSERT INTO calendarsettings(bFilterEnabled, bMonthEnabled, bPosterEnabled, bListEnabled, sDefault)
VALUES(1, 1, 1, 1, 'Poster');
