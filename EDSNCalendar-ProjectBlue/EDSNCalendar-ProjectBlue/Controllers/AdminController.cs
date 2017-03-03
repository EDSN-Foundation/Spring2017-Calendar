using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EDSNCalendar_ProjectBlue.Event;
using EDSNCalendar_ProjectBlue.SQLData;
using EDSNCalendar_ProjectBlue.Property;
using System.Data;

namespace EDSNCalendar_ProjectBlue.Controllers
{
    public class AdminController : Controller
    {

        // GET: Admin
        [Authorize]
        public ActionResult Index()
        {

            ViewBag.PublishedEvents = EventManager.PublishedEvents.Values.Count;
            ViewBag.SubmittedEvents = EventManager.SubmittedEvents.Values.Count;

            return View();
        }
        [Authorize]
        public PartialViewResult EventSubmit()
        {
            List<PropertyType> liPropertyType = new List<PropertyType>();
            liPropertyType = SQLQueries.getAllPropertyTypes(true);
            List<MultiSelectList> liMultiSelect = new List<MultiSelectList>();
            foreach (PropertyType pt in liPropertyType)
            {
                List<Property.Property> tempProp = new List<Property.Property>();
                {
                    liMultiSelect.Add(new MultiSelectList(pt.PropertyList, "propertyId", "name"));
                }
            }
            ViewBag.PropertyTypes = liPropertyType;
            ViewBag.PropertyLists = liMultiSelect;
            return PartialView("~/Views/Calendar/SubmitEvent.cshtml");
        }
        [Authorize]
        public ActionResult EventList(int? Published)
        {
            //Get Number of Published Events
            int publishedCount = EventManager.PublishedEvents.Values.Count;
            ViewBag.PublishedEvents = publishedCount;

            //Get Number of Submitted Events
            int submittedCount = EventManager.SubmittedEvents.Values.Count;
            ViewBag.SubmittedEvents = submittedCount;

            //Get Number of Total Events
            ViewBag.TotalEvents = submittedCount + publishedCount;

            ////list = SQLQueries.getAllEventsList(Published, true);
            //List<SelectListItem> items = new List<SelectListItem>();
            //items.Add(new SelectListItem { Text = "All", Value = "0" });
            //items.Add(new SelectListItem { Text = "Submitted", Value = "1" });
            //items.Add(new SelectListItem { Text = "Published", Value = "2" });
            //ViewBag.SelectedItem = "All";
            //ViewBag.PublishedStatusList = items;

            //Based on optional parameter get a list of events to display to user
            List<Event.Event> list = new List<Event.Event>();            
            switch (Published)
            {
                case null:
                case (0):    //All Events
                    list = SQLQueries.getAllEventsList(0, true);
                    break;
                case (1):       //Submitted Events Only
                    list = SQLQueries.getAllEventsList(1, true);
                    break;
                case (2):       //Published Events Only
                    list = SQLQueries.getAllEventsList(2, true);
                    break;
            }

            return View(list);
        }
        [Authorize]
        public ActionResult EventDetails(int id)
        {
            Event.Event ev;
            ev = new Event.Event(id);
            return View(ev);
        }
        [Authorize]
        public ActionResult ConfirmDelete(int id)
        {
            Event.Event ev;
            ev = new Event.Event(id);
            return View(ev);
        }
        [Authorize]
        public ActionResult DeleteEvent(int id)
        {
            Event.Event ev;
            ev = new Event.Event(id);
            SQLQueries.DeactivateEvent(id);
            if (ev.IsPublished)
                EventManager.PublishedEvents.Remove(id);
            else
                EventManager.SubmittedEvents.Remove(id);
            return View(ev);
        }
        [Authorize]
        public ActionResult ConfirmPublish(int id)
        {
            Event.Event ev;
            ev = new Event.Event(id);
            return View(ev);
        }
        [Authorize]
        public ActionResult PublishEvent(int id)
        {
            Event.Event ev;           
            ev = new Event.Event(id);
            SQLQueries.PublishEvent(id);
            return View(ev);
        }
        [Authorize]
        public ActionResult WidgetCreator()
        {
            List<PropertyType> liPropertyType = new List<PropertyType>();
            liPropertyType = SQLQueries.getAllPropertyTypes(true);
            List<MultiSelectList> liMultiSelect = new List<MultiSelectList>();
            foreach (PropertyType pt in liPropertyType)
            {
                List<Property.Property> tempProp = new List<Property.Property>();
                {
                    liMultiSelect.Add(new MultiSelectList(pt.PropertyList, "propertyId", "name"));
                }
            }
            ViewBag.PropertyTypes = liPropertyType;
            ViewBag.PropertyLists = liMultiSelect;
            return View();
        }
        [Authorize]
        public ActionResult Properties(int? id)
        {
            List<PropertyType> liPropertyType = new List<PropertyType>();
            liPropertyType = SQLQueries.getAllPropertyTypes(true);
            ViewBag.PropertyTypes = liPropertyType;
            ViewBag.SelectedPropertyType = id;

            List<Property.Property> liProperty = new List<Property.Property>();
            foreach (PropertyType pt in liPropertyType)
            {
                if (id == pt.PropertyTypeId)
                {
                    liProperty = pt.PropertyList;
                }
            }

            return View(liProperty);
        }
        [HttpPost]
        public ActionResult AddNewProperty(string ida, string NewProperty)
        {
            int id = Convert.ToInt32(ida);
            int a = SQLQueries.CreateNewProperty(id, NewProperty);
            return Redirect("Properties/" + ida);
        }
        [HttpPost]
        public ActionResult AddNewPropertyType( string NewPropertyType)
        {
            int a;
            if(NewPropertyType!=null && NewPropertyType != "")
                a = SQLQueries.CreateNewPropertyType( NewPropertyType);
            return Redirect("Properties");
        }
        public ActionResult EditEvent(int id)
        {
            Event.Event ev;
            ev = new Event.Event(id);
            return View(ev);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEvent(Event.Event ev)
        {
            SQLQueries.UpdateEvent(ev);
            var file = Request.Files["file"];
            if (file != null)
            {
                //MySqlComm
                byte[] fileBytes = new byte[file.ContentLength];
                file.InputStream.Read(fileBytes, 0, file.ContentLength);
                ev.Image = fileBytes;
                SQLData.SQLQueries.UpdateEventImage(ev);
            }

            return RedirectToAction("EventDetails", new { id = ev.EventId });
        }
        [Authorize]
        public ActionResult CalendarSettings()
        {
            List<PropertyType> liPropertyType = new List<PropertyType>();
            liPropertyType = SQLQueries.getAllPropertyTypes(true);
            List<MultiSelectList> liMultiSelect = new List<MultiSelectList>();

            DataTable dtSelectedProps = SQLQueries.GetPreselectedFilters();
            List<int> liSelectedProps = new List<int>();
            foreach(DataRow row in dtSelectedProps.Rows)
            {
                liSelectedProps.Add(int.Parse(row[0].ToString()));
            }
            foreach (PropertyType pt in liPropertyType)
            {
                List<int> sel = new List<int>();
                foreach (Property.Property p in pt.PropertyList)
                {
                    if(liSelectedProps.Contains(p.PropertyId))
                    {
                        sel.Add(p.PropertyId);
                    }
                }

                liMultiSelect.Add(new MultiSelectList(pt.PropertyList, "propertyId", "name", sel.ToArray()));


            }
            ViewBag.PropertyTypes = liPropertyType;
            ViewBag.PropertyLists = liMultiSelect;

            DataTable dtCalendarSettings = SQLQueries.GetCalendarSettings();
            ViewBag.GlobalHeader = dtCalendarSettings.Rows[0]["sGlobalHeader"].ToString();
            ViewBag.DefaultView = dtCalendarSettings.Rows[0]["sDefault"].ToString();
            ViewBag.FilterEnabled = dtCalendarSettings.Rows[0]["bFilterEnabled"].ToString();
            ViewBag.MonthEnabled = dtCalendarSettings.Rows[0]["bMonthEnabled"].ToString();
            ViewBag.PosterEnabled = dtCalendarSettings.Rows[0]["bPosterEnabled"].ToString();
            ViewBag.ListEnabled = dtCalendarSettings.Rows[0]["bListEnabled"].ToString();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditCalendarSettings(FormCollection form)
        {
            var FilterEnabled = Convert.ToString(form["Filterbar"]);
            var MonthEnabled = Convert.ToString(form["MonthE"]);
            var PosterEnabled = Convert.ToString(form["PosterboardE"]);
            var ListEnabled = Convert.ToString(form["ListE"]);
            var Default = Convert.ToString(form["Default"]);
            var GlobalHeader = Convert.ToString(form["headercode"]);
            SQLQueries.UpdateCalendarSettings(FilterEnabled, MonthEnabled, PosterEnabled, ListEnabled, Default, GlobalHeader);

            List<PropertyType> liPropertyType = SQLQueries.getAllPropertyTypes(true);
            List<int> liSelectedProps = new List<int>();
            foreach (PropertyType pt in liPropertyType)
            {
                string sName = pt.Name;
                var props = Convert.ToString(form[sName]);
                if(props != null)
                {
                    string[] sProps = props.Split(',');
                    foreach (string s in sProps)
                    {
                        liSelectedProps.Add(int.Parse(s));                       
                    }
                }
            }
            SQLQueries.UpdatePreselectedFilters(liSelectedProps);
            return RedirectToAction("CalendarSettings");
        }
        [Authorize]
        public ActionResult UserList()
        {
            List<String> users = SQLQueries.GetUserList();           
            return View(users);
        }
        [Authorize]
        public ActionResult ConfirmRemoveUser(int id)
        {
            //s = s.Replace('$', '@');
            List<String> users = SQLQueries.GetUserList();
            ViewBag.Username = users[id];
            ViewBag.UserId = id;
            return View();
        }
        [Authorize]
        public ActionResult RemoveUser(int id)
        {
            //SQLQueries.RemoveUser(s);
            List<String> users = SQLQueries.GetUserList();
            ViewBag.Username = users[id];
            SQLQueries.RemoveUser(users[id]);
            return View();
        }
    }
}