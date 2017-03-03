using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace EDSNCalendar_ProjectBlue.Event
{
    /// <summary>
    /// Event class represents any particular Event that has 
    /// been submitted, verified, or removed from the Calendar.
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Int Primary Key ID of event.
        /// </summary>
        private int eventId;

        /// <summary>
        /// Name of the event.
        /// </summary>
        private string title;

        /// <summary>
        /// The person who is hosting the event.
        /// </summary>
        private string hostName;

        /// <summary>
        /// Contact email of the host.
        /// </summary>
        private string hostEmail;

        /// <summary>
        /// Contact phone number of the host.
        /// </summary>
        private string hostPhoneNumber;

        /// <summary>
        /// Name of the facility where the event will be hosted.
        /// </summary>
        private string venueName;

        /// <summary>
        /// Address of the event.
        /// </summary>
        private string address;
        
        /// <summary>
        /// Description of the event.
        /// </summary>
        private string description;

        /// <summary>
        /// Optional image attachment.
        /// </summary>
        private byte[] image;

        /// <summary>
        /// Registration URL.
        /// </summary>
        private string registrationURL;

        /// <summary>
        /// Name of the user submitting the event.
        /// </summary>
        private string submitterName;

        /// <summary>
        /// Email of the user submitting the event.
        /// </summary>
        private string submitterEmail;

        /// <summary>
        /// Official date of the event..
        /// </summary>
        private string date;

        /// <summary>
        /// Official date of the event..
        /// </summary>
        private string endDate;

        /// <summary>
        /// Time of when the event begins.
        /// </summary>
        private string startTime;

        /// <summary>
        /// Time of when the event ends.
        /// <summary>
        private string endTime;

        /// <summary>
        /// Flag indicated if the event lasts all day. 
        /// </summary>
        private bool allDay;

        /// <summary>
        /// Date the event was originally submitted
        /// </summary>
        private DateTime postDate;

        /// <summary>
        /// Date the event was originally submitted
        /// </summary>
        private DateTime publishDate;

        /// <summary>
        /// The state of the event. Refers to whether the event has been submitted or published.
        /// </summary>
        private bool isPublished;

        /// <summary>
        /// An active event will be shown to users/administartors, while an inactive event won't be.
        /// </summary>
        private bool isActive;


        public Event()
        { }
        /// <summary>
        /// Constructor that initializes an event and get an existing event's data
        /// </summary>
        /// <param name="iEventId"></param>
        public Event(int iEventId)
        {
            DataTable dtEvent = SQLData.SQLQueries.GetEvent(iEventId);
            this.eventId = (int)dtEvent.Rows[0]["iEventId"];
            this.title = dtEvent.Rows[0]["vEventTitle"].ToString();
            this.hostName = dtEvent.Rows[0]["vOrganizerName"].ToString();
            this.hostEmail = dtEvent.Rows[0]["vOrganizerEmail"].ToString();
            this.hostPhoneNumber = dtEvent.Rows[0]["vOrganizerPhoneNumber"].ToString();
            this.venueName = dtEvent.Rows[0]["vVenueName"].ToString();
            this.address = dtEvent.Rows[0]["vAddress"].ToString();
            this.description = dtEvent.Rows[0]["vDescription"].ToString();
            if(!String.IsNullOrEmpty(dtEvent.Rows[0]["mbImage"].ToString()))
            {
                this.Image = (byte[])dtEvent.Rows[0]["mbImage"];
            }         
            this.registrationURL = dtEvent.Rows[0]["vRegistrationURL"].ToString();
            this.submitterName = dtEvent.Rows[0]["vSubmitterName"].ToString();
            this.submitterEmail = dtEvent.Rows[0]["vSubmitterEmail"].ToString();
            this.date = dtEvent.Rows[0]["dEventDate"].ToString();
            this.endDate = dtEvent.Rows[0]["dEndDate"].ToString();
            if(!String.IsNullOrWhiteSpace(dtEvent.Rows[0]["vStartTime"].ToString()))
            {
                this.startTime = dtEvent.Rows[0]["vStartTime"].ToString();
            }
            if (!String.IsNullOrWhiteSpace(dtEvent.Rows[0]["vStartTime"].ToString()))
            {
                this.endTime = dtEvent.Rows[0]["vEndTime"].ToString();
            }
            this.allDay = Convert.ToBoolean(Convert.ToInt32(dtEvent.Rows[0]["bAllDay"].ToString()));//bool.Parse(dtEvent.Rows[0]["bAllDay"].ToString());
            if(!String.IsNullOrWhiteSpace(dtEvent.Rows[0]["dtPublishDate"].ToString()))
            {
                this.publishDate = Convert.ToDateTime(dtEvent.Rows[0]["dtPublishDate"].ToString());
            }
            this.postDate = Convert.ToDateTime(dtEvent.Rows[0]["dtPostDate"].ToString());
            this.isPublished = Convert.ToBoolean(Convert.ToInt32(dtEvent.Rows[0]["bPublished"].ToString()));
            this.isActive = Convert.ToBoolean(Convert.ToInt32(dtEvent.Rows[0]["bActive"].ToString()));
        }

        /// <summary>
        /// Constructor that intializes a new event object
        /// </summary>
        public Event(string title, string hostName, string hostEmail, string hostPhoneNumber, string venueName,
                     string address, string description, string registrationURL, string submitterName,
                     string submitterEmail, string date, string endDate, string startTime, string endTime, bool allDay)
        {
            this.title = title;
            this.hostName = hostName;
            this.hostEmail = hostEmail;
            this.hostPhoneNumber = hostPhoneNumber;
            this.venueName = venueName;
            this.address = address;
            this.description = description;
            this.registrationURL = registrationURL;
            this.submitterName = submitterName;
            this.submitterEmail = submitterEmail;
            this.date = date;
            this.endDate = endDate;
            this.startTime = startTime;
            this.endTime = endTime;
            this.allDay = allDay;
            this.isPublished = false;
            this.isActive = true;
        }
 
        public int EventId
        {
            get
            {
                return eventId;
            }

            set
            {
                eventId = value;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
            }
        }

        public string HostName
        {
            get
            {
                return hostName;
            }

            set
            {
                hostName = value;
            }
        }

        public string HostEmail
        {
            get
            {
                return hostEmail;
            }

            set
            {
                hostEmail = value;
            }
        }

        public string HostPhoneNumber
        {
            get
            {
                return hostPhoneNumber;
            }

            set
            {
                hostPhoneNumber = value;
            }
        }

        public string VenueName
        {
            get
            {
                return venueName;
            }

            set
            {
                venueName = value;
            }
        }

        public string Address
        {
            get
            {
                return address;
            }

            set
            {
                address = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

        public byte[] Image
        {
            get
            {
                return image;
            }

            set
            {
                image = value;
            }
        }

        public string RegistrationURL
        {
            get
            {
                return registrationURL;
            }

            set
            {
                registrationURL = value;
            }
        }

        public string SubmitterName
        {
            get
            {
                return submitterName;
            }

            set
            {
                submitterName = value;
            }
        }

        public string SubmitterEmail
        {
            get
            {
                return submitterEmail;
            }

            set
            {
                submitterEmail = value;
            }
        }

        public string Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }

        public string EndDate
        {
            get
            {
                return endDate;
            }

            set
            {
                endDate = value;
            }
        }

        public string StartTime
        {
            get
            {
                return startTime;
            }

            set
            {
                startTime = value;
            }
        }

        public string EndTime
        {
            get
            {
                return endTime;
            }

            set
            {
                endTime = value;
            }
        }

        public bool AllDay
        {
            get
            {
                return allDay;
            }

            set
            {
                allDay = value;
            }
        }

        public DateTime PostDate
        {
            get
            {
                return postDate;
            }
            set
            {
                postDate = value;
            }
        }

        public DateTime PublishDate
        {
            get
            {
                return publishDate;
            }
            set
            {
                publishDate = value;
            }
        }

        public bool IsPublished
        {
            get
            {
                return isPublished;
            }

            set
            {
                isPublished = value;
            }
        }

        public bool IsActive
        {
            get
            {
                return isActive;
            }

            set
            {
                isActive = value;
            }
        }

    }

}