


CREATE PROCEDURE usp_InsertTest 

AS
BEGIN
    INSERT INTO PHOTO(PhotoID, Title, Author, Date, Event, EventType, Link)
                 VALUES ('0', 'Cool Picture', 'John', '20170305', 'Fun Event', 'test', 'http//:LinkHere')
END;

