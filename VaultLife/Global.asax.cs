using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Quartz;
using Quartz.Impl;
using System.Threading;
using System.Globalization;
using Vaultlife.Controllers;
using Vaultlife.Service;
using System.Web.Http;
using Vaultlife.Handlers;


namespace Vaultlife
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.JobFactory = new RuleJobFactory();
            scheduler.Start();

           // Create a job of Type WriteToConsoleJob
            IJobDetail emaiSendJob = JobBuilder.Create(typeof(EmailSendingJob)).WithIdentity("EmailSendJob", "EmailSendJobGroup").Build();

            //Schedule this job to execute every minute, forever.
            ITrigger emailSendTrigger = TriggerBuilder.Create().WithSchedule(SimpleScheduleBuilder.RepeatMinutelyForever()).StartNow().WithIdentity("EmailSendJobTrigger", "EmailSendJobGroup").Build();
            if (!scheduler.CheckExists(emaiSendJob.Key))
            {
                scheduler.ScheduleJob(emaiSendJob, emailSendTrigger); 
            }
        }


    public static void RegisterRoutes(RouteCollection routes)
    {
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

        routes.MapRoute(
            "Default",                                              // Route name
            "{controller}/{action}/{id}",                           // URL with parameters
            new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
        );


        }

        protected void Session_Start(Object sender, EventArgs e)
        {

        }


        void Application_AcquireRequestState(object sender, EventArgs e)
        {
            //Session is Available here
            HttpContext context = HttpContext.Current;
            if (HttpContext.Current.Session != null)
            {
                CultureInfo ci = (CultureInfo)this.Session["Culture"];
                //Checking first if there is no value in session 
                //and set default language 
                //this can happen for first user's request
                if (ci == null)
                {
                    //Sets default culture to english invariant
                    string langName = "en";

                    //Try to get values from Accept lang HTTP header
                    if (HttpContext.Current.Request.UserLanguages != null &&
                         HttpContext.Current.Request.UserLanguages.Length != 0)
                    {
                        //Gets accepted list 
                        langName = HttpContext.Current.Request.UserLanguages[0].Substring(0, 2);
                    }
                    ci = new CultureInfo(langName);
                    this.Session["Culture"] = ci;
                }
                //Finally setting culture for each request
                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            }

        }

  /*      protected void Application_Error(object sender, EventArgs e)
        {
            // Do whatever you want to do with the error

            //Show the custom error page...
            Server.ClearError();
            var routeData = new RouteData();
            routeData.Values["controller"] = "Error";

            if ((Context.Server.GetLastError() is HttpException) && ((Context.Server.GetLastError() as HttpException).GetHttpCode() != 404))
            {
                routeData.Values["action"] = "Index";
            }
            else
            {
                // Handle 404 error and response code
                Response.StatusCode = 404;
                routeData.Values["action"] = "NotFound404";
            }
            Response.TrySkipIisCustomErrors = true; // If you are using IIS7, have this line
            IController errorsController = new ErrorController();
            HttpContextWrapper wrapper = new HttpContextWrapper(Context);
            var rc = new System.Web.Routing.RequestContext(wrapper, routeData);
            errorsController.Execute(rc);
        }
*/


        protected void Application_End()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Shutdown(true);
            //TODO do we need to catch a SchedulerException ?
        }
    }
}
