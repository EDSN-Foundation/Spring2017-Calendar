using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace EDSNCalendar_ProjectBlue.Event
{
    /// <summary>
    /// The EventManager is a static class that handles all functionality 
    /// of events that exist in the calendar system.
    /// </summary>
    public static class EventManager
    {

        /// <summary>
        /// Collection of events that are ready to be reviewed by an administrator.
        /// </summary>
        private static Dictionary<int, Event> submittedEvents = new Dictionary<int, Event>();

        public static Dictionary<int, Event> SubmittedEvents
        {
            get
            {
                return submittedEvents;
            }
        }

        /// <summary>
        /// Collection of events that are ready to be reviewed by an administrator.
        /// </summary>
        private static Dictionary<int, Event> publishedEvents = new Dictionary<int, Event>();

        public static Dictionary<int, Event> PublishedEvents
        {
            get
            {
                return publishedEvents;
            }
        }

        static EventManager()
        {
            publishedEvents.Clear();
            DataTable dtPublishedActiveEvents = SQLData.SQLQueries.GetAllEvents(2, true);
            foreach (DataRow publishedRow in dtPublishedActiveEvents.Rows)
            {
                int iEventId = (int)publishedRow["iEventId"];
                Event publishedEvent = new Event(iEventId);
                publishedEvents.Add(iEventId, publishedEvent);
            }

            submittedEvents.Clear();
            DataTable dtSubmittedActiveEvents = SQLData.SQLQueries.GetSubmittedEvents();
            foreach (DataRow submittedRow in dtSubmittedActiveEvents.Rows)
            {
                int iEventId = (int)submittedRow["iEventId"];
                Event submittedEvent = new Event(iEventId);
                submittedEvents.Add(iEventId, submittedEvent);
            }
        }

        public static void updateEventManager()
        {
            publishedEvents.Clear();
            DataTable dtPublishedActiveEvents = SQLData.SQLQueries.GetAllEvents(2, true);
            foreach (DataRow publishedRow in dtPublishedActiveEvents.Rows)
            {
                int iEventId = (int)publishedRow["iEventId"];
                Event publishedEvent = new Event(iEventId);
                publishedEvents.Add(iEventId, publishedEvent);
            }

            submittedEvents.Clear();
            DataTable dtSubmittedActiveEvents = SQLData.SQLQueries.GetSubmittedEvents();
            foreach (DataRow submittedRow in dtSubmittedActiveEvents.Rows)
            {
                int iEventId = (int)submittedRow["iEventId"];
                Event submittedEvent = new Event(iEventId);
                submittedEvents.Add(iEventId, submittedEvent);
            }
        }

        public static String ToJSONRepresentation(bool published)
        {
            Dictionary<int, Event> events = published ? PublishedEvents : SubmittedEvents;
            StringBuilder sb = new StringBuilder();
            JsonWriter jw = new JsonTextWriter(new StringWriter(sb));
            jw.Formatting = Formatting.Indented;
            jw.WriteStartArray();
            foreach (Event e in events.Values)
            {
                jw.WriteStartObject();
                jw.WritePropertyName("id");
                jw.WriteValue(e.EventId);
                jw.WritePropertyName("title");
                jw.WriteValue(e.Title);
                jw.WritePropertyName("hostName");
                jw.WriteValue(e.HostName);
                jw.WritePropertyName("hostEmail");
                jw.WriteValue(e.HostEmail);
                jw.WritePropertyName("hostPhoneNumber");
                jw.WriteValue(e.HostPhoneNumber);
                jw.WritePropertyName("venueName");
                jw.WriteValue(e.VenueName);
                jw.WritePropertyName("address");
                jw.WriteValue(e.Address);
                jw.WritePropertyName("description");
                jw.WriteValue(e.Description);
                jw.WritePropertyName("image");
                jw.WriteValue(e.Image);
                jw.WritePropertyName("registrationURL");
                jw.WriteValue(e.RegistrationURL);
                jw.WritePropertyName("submitterName");
                jw.WriteValue(e.SubmitterName);
                jw.WritePropertyName("submitterEmail");
                jw.WriteValue(e.SubmitterEmail);
                jw.WritePropertyName("date");
                jw.WriteValue(e.Date);
                jw.WritePropertyName("start");
                jw.WriteValue(e.StartTime);
                jw.WritePropertyName("end");
                jw.WriteValue(e.EndTime);               
                jw.WritePropertyName("allDay");
                jw.WriteValue(e.AllDay);
                jw.WritePropertyName("postDate");
                jw.WriteValue(e.PostDate);
                jw.WritePropertyName("isPublished");
                jw.WriteValue(e.IsPublished);
                jw.WritePropertyName("isActive");
                jw.WriteValue(e.IsActive);              
                jw.WriteEndObject();
            }
            jw.WriteEndArray();
            return sb.ToString();
        }

        public static String PropertiesToJSONRepresentation()
        {
            var properties = SQLData.SQLQueries.GetEventsByProperty();
            StringBuilder sb = new StringBuilder();
            JsonWriter jw = new JsonTextWriter(new StringWriter(sb));
            jw.Formatting = Formatting.Indented;
            jw.WriteStartArray();
            foreach (var property in properties)
            {
                jw.WriteStartObject();
                jw.WritePropertyName("property");
                jw.WriteValue(property.Name);
                jw.WritePropertyName("events");
                jw.WriteStartArray();
                foreach (var e in property.LiEvents)
                {
                    jw.WriteValue(e);
                }
                jw.WriteEndArray();
                jw.WriteEndObject();
            }
            jw.WriteEndArray();
            return sb.ToString();
        }

    }
}