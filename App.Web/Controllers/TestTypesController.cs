using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using App.Core.Context;
using App.Core.Models.EntityModels;

namespace App.Web.Controllers
{
    public class TestTypesController : Controller
    {
        // GET: TestTypes
        public ActionResult Index()
        {
            return View();
        }
    }
}
