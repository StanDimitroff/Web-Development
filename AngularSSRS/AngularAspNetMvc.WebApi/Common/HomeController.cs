using System.Web.Http;
using AngularAspNetMvc.Models.Common;
using AngularAspNetMvc.Models.Core;
using AngularAspNetMvc.WebApi.Core;
using System.Collections.Generic;

namespace AngularAspNetMvc.WebApi.Common
{
    public class HomeController : RelayController
    {
        [HttpGet]
        public List<MenuItem> Menu()
        {
            return new List<MenuItem>
            {
                new MenuItem
                {
                    Path = "/home",
                    Controller = "homeController",
                    TemplateUrl = "home/home",
                    Title = "Home",
                    IsMenu = true
                },
                new MenuItem
                {
                    Path = "/contacttypes",
                    Controller = "app.contacts.contactsTypesController",
                    TemplateUrl = "contacttypes/index",
                    Title = "Contact Types",
                    IsMenu = true
                },
                new MenuItem
                {
                    Path = "/contacts",
                    Controller = "app.contacts.contactsController",
                    TemplateUrl = "contacts/index",
                    Title = "Contacts",
                    IsMenu = true
                },
                new MenuItem
                {
                    Path = "/contacttype/edit/:id",
                    Controller = "app.contacts.contactsTypesEditController",
                    TemplateUrl = "contacttypes/edit",
                    Title = "Contacts",
                    IsMenu = false
                },
                new MenuItem
                {
                    Path = "/contact/edit/:id",
                    Controller = "app.contacts.contactsEditController",
                    TemplateUrl = "contacts/edit",
                    Title = "Contacts",
                    IsMenu = false
                },
                new MenuItem
                {
                    Path = "/reports",
                    Controller = "app.reports.reportsController",
                    TemplateUrl = "reports/index",
                    Title = "Reports",
                    IsMenu = true
                },
                new MenuItem
                {
                    Path = "/reports/view/:id",
                    Controller = "app.reports.reportsViewController",
                    TemplateUrl = "reports/viewReport",
                    Title = "Reports",
                    IsMenu = false
                }
            };
        }
    }
}
