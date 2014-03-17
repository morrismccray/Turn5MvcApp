using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Mvc;
using System.Xml.Serialization;
using Turn5MvcApp.Models;

namespace Turn5MvcApp.Controllers
{
    public class HomeController : Controller
    {
        private const string QueryStringFormat =
            @"http://api.americanmuscle.com/search/get?keywords={0}&sort_by_field={1}";

        //Must handle the request to render the view
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Search searchModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Must handle the call to the search client (see API notes on page 3)
                    string queryString = string.Format(QueryStringFormat, searchModel.Term, searchModel.Sort);
                    var request = WebRequest.Create(queryString) as HttpWebRequest;
                    if (request == null) return PartialView("SearchError", searchModel);
                    request.Method = "GET";
                    using (var response = (HttpWebResponse) request.GetResponse())
                    {
                        searchModel.Results = ParseResponse(response);
                    }
                }
                SaveToDatabase(searchModel);
                return PartialView("SearchForm", searchModel);
            }
            catch (Exception e)
            {
                //log Exception maybe in database or log file
                Debug.WriteLine("*************");
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.InnerException);

                //Errors must be handled and a friendly message displayed to the user should one be encountered
                //Note: Just a catch all for meeting requirements -- I would handle more specifically 
                return PartialView("SearchError", searchModel);       
            }
        }

        /*	Must store the following in a database:
            1.	Session id ( some unique string, doesn't actually have to be a real session id )
            2.	Keywords searched ( the terms the user searched for )
            3.	The "Name"s of the first 10 results returned from the search client */

        public void SaveToDatabase(Search searchModel)
        {
            var db = new ActualDatabaseContext();

            // Since I don't have a database, I am keeping this simple
            var searchLog = new SearchLog
            {
                SessionId = new Guid(),
                KeyWords = searchModel.Term,
                ResultNames = GetResultNames(searchModel)
            };
            db.SearchLog.Add(searchLog);

            //commented out for lack of database
            //db.SaveChanges();
        }

        //Must handle parsing the XML response returned by the client 
        private static SearchXML ParseResponse(WebResponse response)
        {
            var encode = Encoding.GetEncoding("utf-8");

            using (var dataStream = response.GetResponseStream())
            
                using (var reader = new StreamReader(dataStream, encode))
                {
                    var serializer = new XmlSerializer(typeof (SearchXML));

                    var results = ((SearchXML) serializer.Deserialize(reader));
                    return results;
                }
            
        }

        //Must handle parsing the XML response returned by the client 
        private static string GetResultNames(Search searchModel)
        {
            var resultNames = string.Empty;
            for (var index = 0; index < searchModel.Results.results.Length && index < 10; index++)
            {
                var result = searchModel.Results.results[index];
                resultNames = resultNames + result.Name + " | ";
            }
            if( resultNames.Length != 0)
            resultNames = resultNames.Remove(resultNames.Length - 3, 3);
            return resultNames;
        }
    }
}