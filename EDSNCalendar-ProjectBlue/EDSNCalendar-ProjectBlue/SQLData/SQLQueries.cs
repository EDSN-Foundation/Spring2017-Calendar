using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EDSNCalendar_ProjectBlue.SQLData;
using EDSNCalendar_ProjectBlue.Event;
using EDSNCalendar_ProjectBlue.Property;
using System.Data;
using MySql.Data.MySqlClient;

namespace EDSNCalendar_ProjectBlue.SQLData
{
    /// <summary>
    /// A library of queries that can be run with parameters/returns similar to functions
    /// </summary>
    public static class SQLQueries
    {
        //Example Usage of SQLDataAdapter to run Queries
        public static void ExampleQuery2()
        {
            string sQuery = "INSERT INTO categories(vCategory) VALUES('Whatever')";
            SQLDataAdapter.QueryExecute(sQuery);
        }
        //Example Usage of SQLDataAdapter to run Queries
        public static DataTable ExampleQuery3()
        {
            DataTable dtAllCategories;
            dtAllCategories = SQLDataAdapter.Query4DataTable("SELECT * FROM categories");
            return dtAllCategories;
        }

        /// <summary>
        /// Creates a new submitted event in DB
        /// </summary>
        /// <param name="sEventTitle">Event Title</param>
        /// <param name="dEventDate">Date of Event</param>
        /// <param name="sStartTime">Event Start Time(optional)</param>
        /// <param name="sEndTime">Event End Time(optional)</param>
        /// <param name="bAllDay">All Day Event(optional)</param>
        /// <param name="sVenueName">Venue Name</param>
        /// <param name="sAddress">Address</param>
        /// <param name="sDescription">Description</param>
        /// <param name="sOrganizerName">Organizer's Name</param>
        /// <param name="sOrganizerEmail">Organizer's Email</param>
        /// <param name="sOrganizerPhoneNumber">Organizer's Phone Number</param>
        /// <param name="sOrganizerURL">Organizer's URL(optional)</param>
        /// <param name="sCost">Cost(optional)</param>
        /// <param name="sRegistrationURL">Registration URL(optional)</param>
        /// <param name="sSubmitterName">Submitter's Name(optional)</param>
        /// <param name="sSubmitterEmail">Submitter's Email(optional)</param>
        /// <returns></returns>
        public static int InsertSubmittedEvent(string sEventTitle, string dEventDate, string dEndDate ,string sStartTime, string sEndTime, bool bAllDay, string sVenueName, string sAddress, string sDescription, string sOrganizerName,
                                string sOrganizerEmail, string sOrganizerPhoneNumber, string sOrganizerURL, string sCost, string sRegistrationURL, string sSubmitterName, string sSubmitterEmail)
        {

            int iRowsAffected = 0; 
            string sQuery = "INSERT INTO calendarevent(vEventTitle, dEventDate, dEndDate, vStartTime, vEndTime, bAllDay, vVenueName, vAddress, vDescription, vOrganizerName, vOrganizerEmail, vOrganizerPhoneNumber," +
                                                                        "vOrganizerURL, vCost, vRegistrationURL, vSubmitterName, vSubmitterEmail)" +
                            "VALUES('" + sEventTitle + "','" + dEventDate + "','" + dEndDate + "','" + sStartTime + "','" + sEndTime + "'," + Convert.ToInt32(bAllDay) + ",'" + sVenueName + "','" + sAddress + "','" +
                                         sDescription + "','" + sOrganizerName + "','" + sOrganizerEmail + "','" + sOrganizerPhoneNumber + "','" + sOrganizerURL + "','" + sCost + "','" + sRegistrationURL + "','" + sSubmitterName + "','" + sSubmitterEmail + "')";
            iRowsAffected = SQLDataAdapter.QueryExecute(sQuery);
            int eventId = SQLDataAdapter.LastInsertedId;
            var submittedEvent = new Event.Event(sEventTitle, sOrganizerName, sOrganizerEmail, sOrganizerPhoneNumber, sVenueName, sAddress, sDescription, sRegistrationURL, sSubmitterName, sSubmitterEmail, dEventDate, dEndDate, sStartTime, sEndTime, bAllDay);
            submittedEvent.EventId = eventId;

            EventManager.SubmittedEvents.Add(eventId, submittedEvent);
            return eventId;
        }

        /// <summary>
        /// Publishes an existing submitted event in DB
        /// </summary>
        /// <param name="iEventId">id# of event to publish</param>
        /// <returns>Number of rows affected, signifying if the creation was successful.</returns>
        public static int PublishEvent(int iEventId)
        {
            int iRowsAffected = 0;
            String sQuery = "UPDATE calendarevent SET bPublished = 1, dtPublishDate = NOW() WHERE iEventId = " + iEventId;
            iRowsAffected = SQLDataAdapter.QueryExecute(sQuery);
            var tempEvent = EventManager.SubmittedEvents[iEventId];
            EventManager.SubmittedEvents.Remove(iEventId);
            EventManager.PublishedEvents.Add(tempEvent.EventId, tempEvent);
            return iRowsAffected;
        }

        /// <summary>
        /// Returns a table of events. returns all events by default but parameters can be used to get only active/published events
        /// </summary>
        /// <param name="iPublishStatus">Determines the published status of the list to be returned 0: all events, 1: Submitted only, 2: Published only</param>
        /// <param name="bActiveOnly">Determines whether only active events are returned. Default: false</param>
        /// <returns>Multi rowed table with each row holding an event's attributes.</returns>
        public static DataTable GetAllEvents(int iPublishStatus = 0, bool bActiveOnly = false)
        {
            DataTable dtEvents = new DataTable();
            string sQuery = string.Empty;
            switch(iPublishStatus)
            {
                case (0):   //All Events
                    sQuery = "SELECT * FROM calendarevent WHERE (bActive = 1 OR bActive = " + Convert.ToInt32(bActiveOnly) + ")";
                    break;
                case (1):   //Submitted Events Only
                    sQuery = "SELECT * FROM calendarevent WHERE (bActive = 1 OR bActive = " + Convert.ToInt32(bActiveOnly) + ") AND bPublished = 0";
                    break;
                case (2):   //Published Events Only
                    sQuery = "SELECT * FROM calendarevent WHERE (bActive = 1 OR bActive = " + Convert.ToInt32(bActiveOnly) + ") AND bPublished = 1";
                    break;
            }
            dtEvents = SQLDataAdapter.Query4DataTable(sQuery);
            return dtEvents;
        }

        public static List<Event.Event> getAllEventsList(int iPublishStatus = 0, bool bActiveOnly = false)
        {
            DataTable dtEvents = new DataTable();
            string sQuery = string.Empty;
            switch (iPublishStatus)
            {
                case (0):   //All Events
                    sQuery = "SELECT iEventId FROM calendarevent WHERE (bActive = 1 OR bActive = " + Convert.ToInt32(bActiveOnly) + ")";
                    break;
                case (1):   //Submitted Events Only
                    sQuery = "SELECT iEventId FROM calendarevent WHERE (bActive = 1 OR bActive = " + Convert.ToInt32(bActiveOnly) + ") AND bPublished = 0";
                    break;
                case (2):   //Published Events Only
                    sQuery = "SELECT iEventId FROM calendarevent WHERE (bActive = 1 OR bActive = " + Convert.ToInt32(bActiveOnly) + ") AND bPublished = 1";
                    break;
            }
            dtEvents = SQLDataAdapter.Query4DataTable(sQuery);
            List<Event.Event> list = new List<Event.Event>();
            foreach(DataRow row in dtEvents.Rows)
            {
                int iEventId = (int)row[0];
                Event.Event e = new Event.Event(iEventId);
                list.Add(e);
            }
            return list;
        }

        /// <summary>
        /// Returns a table of events. returns all events by default but parameters can be used to get only active/published events
        /// </summary>
        /// <param name="bPublishedOnly">Determines whether only published events are returned. Default: false</param>
        /// <param name="bActiveOnly">Determines whether only active events are returned. Default: false</param>
        /// <returns>Multi rowed table with each row holding an event's attributes.</returns>
        public static DataTable GetSubmittedEvents()
        {
            DataTable dtEvents = new DataTable();
            string sQuery = "SELECT * FROM calendarevent WHERE bPublished = 0 AND bActive = 1";
            dtEvents = SQLDataAdapter.Query4DataTable(sQuery);
            return dtEvents;
        }

        /// <summary>
        /// Returns an event object representing the last submitted event.
        /// </summary>
        /// <returns>Returns a populated event object</returns>
        public static Event.Event GetLastEvent()
        {
            int iEventId;
            string sQuery = "SELECT iEventId FROM calendarevent ORDER BY iEventId DESC LIMIT 1";
            iEventId = SQLDataAdapter.Query4Int(sQuery);

            Event.Event ev = new Event.Event(iEventId);
            return ev;
        }

        public static void UpdateEvent(Event.Event ev)
        {
            string sQuery = "UPDATE calendarevent SET vEventTitle = '" + ev.Title + "', vOrganizerName = '" + ev.HostName + "', vOrganizerEmail = '" + ev.HostEmail + "', vOrganizerPhoneNumber = '" + ev.HostPhoneNumber + "', vVenueName = '" + ev.VenueName + "', vAddress = '" + ev.Address + "', vDescription = '" + ev.Description + "', vRegistrationURL = '" + ev.RegistrationURL + "', vSubmitterName = '" + ev.SubmitterName + "', vSubmitterEmail = '" + ev.SubmitterEmail + "', dEventDate = '" + ev.Date + "', dEndDate = '" + ev.EndDate + "', vStartTime = '" + ev.StartTime + "', vEndTime = '" + ev.EndTime + "', bAllDay = " + ev.AllDay + " WHERE iEventId = " + ev.EventId + " ";
            int iRowsAffected = SQLDataAdapter.QueryExecute(sQuery);
        }

        public static void UpdateEventImage(Event.Event ev)
        {
            SQLDataAdapter.QueryUpdateImage(ev.EventId, ev.Image);
        }

        /// <summary>
        /// Returns a single rowed table which has all of that event's data.
        /// </summary>
        /// <param name="iEventId">Event to Query for</param>
        /// <returns>Single rowed Datatable with all event attributes.</returns>
        public static DataTable GetEvent(int? iEventId)
        {
            DataTable dtEvents = new DataTable();
            string sQuery = "SELECT * FROM calendarevent WHERE iEventId = " + iEventId;
            dtEvents = SQLDataAdapter.Query4DataTable(sQuery);
            return dtEvents;
        }

        /// <summary>
        /// Deactives an event by Event Id
        /// </summary>
        /// <param name="iEventId"></param>
        /// <returns>Number of rows affected, ideally 1</returns>
        public static int DeactivateEvent(int iEventId)
        {
            int iRowsAffected = 0;
            string sQuery = "UPDATE calendarevent SET bActive = 0 WHERE iEventId = " + iEventId;
            iRowsAffected = SQLDataAdapter.QueryExecute(sQuery);
            return iRowsAffected;
        }

        public static List<PropertyType> getAllPropertyTypes(bool bActiveOnly = false)
        {
            List<PropertyType> li = new List<PropertyType>();
            string sQuery = "SELECT * FROM propertytype WHERE (bActive = 1 or bActive = " + Convert.ToInt32(bActiveOnly) + ")";
            DataTable dtPropertyTypes = SQLDataAdapter.Query4DataTable(sQuery);
            foreach(DataRow row in dtPropertyTypes.Rows)
            {
                PropertyType pt = new PropertyType();
                pt.PropertyTypeId = int.Parse(row["iPropertyTypeId"].ToString());
                pt.Name = row["vPropertyType"].ToString();
                pt.PropertyList = getPropertyList(pt, true);
                li.Add(pt);
            }
            return li;
        }

        public static List<Property.Property> getPropertyList(PropertyType propertyType, bool bActiveOnly = false)
        {
            List<Property.Property> li = new List<Property.Property>();
            string sQuery = "SELECT * FROM property WHERE iPropertyTypeId = "+ propertyType.PropertyTypeId + " AND (bActive = 1 or bActive = " + Convert.ToInt32(bActiveOnly) + ")";
            DataTable dtPropertys = SQLDataAdapter.Query4DataTable(sQuery);
            foreach (DataRow row in dtPropertys.Rows)
            {
                Property.Property p = new Property.Property();
                p.PropertyId = int.Parse(row["iPropertyId"].ToString());
                p.Name = row["vProperty"].ToString();
                p.PropertyType = propertyType;
                li.Add(p);
            }
            return li;
        }

        public static int CreateNewPropertyType(string sPropertyTypeName)
        {
            int iRowsAffected = 0;
            string sQuery = "INSERT INTO propertytype(vPropertyType) VALUES('"+sPropertyTypeName+"')";
            iRowsAffected = SQLDataAdapter.QueryExecute(sQuery);
            return iRowsAffected;
        }

        public static int CreateNewProperty(int iPropertyTypeId, string sPropertyName)
        {
            int iRowsAffected = 0;
            string sQuery = "INSERT INTO property(iPropertyTypeId, vProperty) VALUES(" + iPropertyTypeId + ",'" + sPropertyName + "')";
            iRowsAffected = SQLDataAdapter.QueryExecute(sQuery);
            return iRowsAffected;
        }

        public static int AddPropertyToEvent(int iEventId, int iPropertyId)
        {
            int iRowsAffected = 0;
            string sQuery = "INSERT INTO eventproperties(iEventId, iPropertyId) VALUES(" + iEventId + "," + iPropertyId + ")";
            iRowsAffected = SQLDataAdapter.QueryExecute(sQuery);
            return iRowsAffected;
        }

        public static List<String> GetUserList()
        {
            DataTable dtUsers = new DataTable();
            string sQuery = "SELECT UserName FROM aspnetusers";
            dtUsers = SQLDataAdapter.Query4DataTableUser(sQuery);
            List<String> list = new List<String>();
            foreach (DataRow row in dtUsers.Rows)
            {
                list.Add(row["UserName"].ToString());
            }
            return list;
        }

        public static void RemoveUser(string UserName)
        {
            string sQuery = "DELETE FROM aspnetusers WHERE UserName = '" + UserName + "'";
            int iRowsAffected = SQLDataAdapter.QueryExecuteUser(sQuery);
        }

        public static DataTable GetCalendarSettings()
        {
            DataTable dtSettings = new DataTable();
            string sQuery = "SELECT * FROM calendarsettings";
            dtSettings = SQLDataAdapter.Query4DataTable(sQuery);
            return dtSettings;
        }

        public static DataTable GetPreselectedFilters()
        {
            DataTable dtSelected = new DataTable();
            string sQuery = "Select * from preselectedcalendarfilters";
            dtSelected = SQLDataAdapter.Query4DataTable(sQuery);
            return dtSelected;
        }

        public static void UpdateCalendarSettings(string FilterE, string MonthE, string PosterE, string ListE, string Default, string sGlobalHeader)
        {
            int month = 0;
            if(MonthE == "on")
            {
                month = 1;
            }
            int poster = 0;
            if(PosterE == "on")
            {
                poster = 1;
            }
            int list = 0;
            if(ListE == "on")
            {
                list = 1;
            }
            int filter = 0;   
            if(FilterE == "Enabled")
            {
                filter = 1;
            }
            string sQuery = "UPDATE calendarsettings SET bFilterEnabled = " + filter + ", bMonthEnabled = " + month + ", bPosterEnabled = " + poster + ", bListEnabled = " + list + ", sDefault = '" + Default + "', sGlobalHeader = '" + sGlobalHeader + "'";
            int iRowsAffected = SQLDataAdapter.QueryExecute(sQuery);
        }

        public static void UpdatePreselectedFilters(List<int> li)
        {
            string sQuery = "DELETE FROM preselectedcalendarfilters";
            int iRowsAffected = SQLDataAdapter.QueryExecute(sQuery);

            sQuery = "INSERT INTO preselectedcalendarfilters(iPropertyId) VALUES(";
            foreach(int i in li)
            {
                sQuery = sQuery + i + "),(";
            }
            sQuery = sQuery.Remove(sQuery.Length - 2);
            iRowsAffected = SQLDataAdapter.QueryExecute(sQuery);
        }

        public static List<Property.Property> GetEventsByProperty()
        {
            List<Property.Property> liProp = new List<Property.Property>();
            List<PropertyType> liPT = getAllPropertyTypes(true);
            foreach(PropertyType pt in liPT)
            {
                foreach(Property.Property p in pt.PropertyList)
                {
                    int iPropertyId = p.PropertyId;
                    p.LiEvents = getEventsForProperty(iPropertyId);
                    liProp.Add(p);
                }
            }

            return liProp;
        }

        public static List<int> getEventsForProperty(int iPropertyId)
        {
            List<int> liEvents = new List<int>();

            string sQuery = "SELECT iEventId FROM eventproperties WHERE iPropertyId = " + iPropertyId;
            DataTable dtEvents = SQLDataAdapter.Query4DataTable(sQuery);
            foreach(DataRow row in dtEvents.Rows)
            {
                liEvents.Add((int)row[0]);
            }

            return liEvents;
        }
    }
}