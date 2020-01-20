using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Vaultlife.Models;

namespace Vaultlife.Controllers
{
    public class GamePlayController : Controller
    {
        private ITest _testInterface;

        public GamePlayController(ITest testParam)
        {
            _testInterface = testParam;
        }

        // GET: GamePlay
        public ContentResult Index()
        {
            return Content("Working");
        }

        public ActionResult InterfaceTest()
        {

            return Content(_testInterface.TestString);
        }
    }
}