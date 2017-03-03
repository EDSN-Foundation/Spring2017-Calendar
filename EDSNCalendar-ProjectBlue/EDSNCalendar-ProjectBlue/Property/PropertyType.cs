using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDSNCalendar_ProjectBlue.Property
{
    public class PropertyType
    {
        /// <summary>
        /// Int Primary Key ID of event.
        /// </summary>
        private int propertyTypeId;
        public int PropertyTypeId
        {
            get { return propertyTypeId; }
            set { propertyTypeId = value; }
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
        private List<Property> propertyList;
        public List<Property> PropertyList
        {
            get { return propertyList; }
            set { propertyList = value; }
        }

        
    }
}