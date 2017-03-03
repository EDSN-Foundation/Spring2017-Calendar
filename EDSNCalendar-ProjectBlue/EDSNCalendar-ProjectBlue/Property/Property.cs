using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EDSNCalendar_ProjectBlue.Property;

namespace EDSNCalendar_ProjectBlue.Property
{
    /// <summary>
    /// Property class represents any particular Property
    /// </summary>
    public class Property
    {
        /// <summary>
        /// Int Primary Key ID of event.
        /// </summary>
        private int propertyId;
        public int PropertyId
        {
            get { return propertyId; }
            set { propertyId = value; }
        }

        /// <summary>
        /// Name of the event.
        /// </summary>
        private string name;        
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// PropertyType this particular property belongs to
        /// </summary>
        private PropertyType propertyType;
        public PropertyType PropertyType
        {
            get { return propertyType; }
            set { propertyType = value; }
        }

        private List<int> liEvents;
        public List<int> LiEvents
        {
            get { return liEvents; }
            set { liEvents = value; }
        }
    }
}