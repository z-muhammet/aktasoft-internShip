using ebokScript.Context;
using ebokScript.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ebokScript.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
            public IActionResult message2()
            {
                List<messageScript> messagesScripts = null;
                using (var messagescontext = new messagesContext())
                {
                    messagesScripts = messagescontext.message.ToList();
                }

                return View(messagesScripts);
            }

        public IActionResult page(int id)
        {
            var pages = GetPagesFromDatabase();
            var page = pages.FirstOrDefault(p => p.id == id);

            if (page == null)
            {
                return NotFound();
            }

            return View(new List<PageScript> { page });

            /* SqlConnection sql = new SqlConnection("server=MZRN\\SQLEXPRESS;database=page;Trusted_Connection=True;TrustServerCertificate=true;");
             SqlCommand cmd = new SqlCommand("select * from page_Table", sql);
             sql.Open();
             SqlDataReader reader = cmd.ExecuteReader();
             List<PageScript> pages = new List<PageScript>();
             while (reader.Read())
             {
                 pages.Add(
                 new PageScript
                 {
                     id = Convert.ToInt32(reader["id"]),
                     Title = reader["Title"].ToString(),
                     Content = reader["Content"].ToString()
                 });
             }
             sql.Close();
             return View(pages);*/
        }

            public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult books() {
            return View();
        }
        public ActionResult DifferentView()
        {
            return View("DifferentView"); // "DifferentView" adlı farklı bir görünümü çağırır
        }
        public IActionResult getMessageFromDatabase()
        {
            SqlConnection sql = new SqlConnection("server=MZRN\\SQLEXPRESS;database=messageScript;Trusted_Connection=True;TrustServerCertificate=true;");
            SqlCommand cmd = new SqlCommand("select * from message", sql);
            sql.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<messageScript> messages = new List<messageScript>();
            while (reader.Read())
            {
                messages.Add(
                new messageScript
                {
                    id = Convert.ToInt32(reader["id"]),
                    name = reader["name"].ToString(),
                    message = reader["message"].ToString()
                });
            }
            sql.Close();
            return View(messages);
        }   
        public IActionResult message()
        {

            SqlConnection sql = new SqlConnection("server=MZRN\\SQLEXPRESS;database=messageScript;Trusted_Connection=True;TrustServerCertificate=true;");
            SqlCommand cmd = new SqlCommand("select * from message", sql);
            sql.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<messageScript> messages = new List<messageScript>();
            while (reader.Read())
            {
                messages.Add(
                new messageScript
                {
                    id = Convert.ToInt32(reader["id"]),
                    name = reader["name"].ToString(),
                    message = reader["message"].ToString()
                });
            }
            sql.Close();

            return View("message", messages);
        }
        private List<PageScript> GetPagesFromDatabase()
        {
            SqlConnection sql = new SqlConnection("server=MZRN\\SQLEXPRESS;database=page;Trusted_Connection=True;TrustServerCertificate=true;");
            SqlCommand cmd = new SqlCommand("select * from page_Table", sql);
            sql.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<PageScript> pages = new List<PageScript>();
            while (reader.Read())
            {
                pages.Add(
                new PageScript
                {
                    id = Convert.ToInt32(reader["id"]),
                    Title = reader["Title"].ToString(),
                    Content = reader["Content"].ToString()
                });
            }
            sql.Close();
            return pages;
        }
        public IActionResult ShowPage(int id)
        {
            var pages = GetPagesFromDatabase();
            var page = pages.FirstOrDefault(p => p.id == id);

            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }
        public IActionResult pageViewer() {     
            SqlConnection sql = new SqlConnection("server=MZRN\\SQLEXPRESS;database=page;Trusted_Connection=True;TrustServerCertificate=true;");
            SqlCommand cmd = new SqlCommand("select * from page_Table", sql);
            sql.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<PageScript> pages = new List<PageScript>();
            while (reader.Read())
            {
                pages.Add(
                new PageScript{
                    id = Convert.ToInt32(reader["id"]),
                    Title = reader["Title"].ToString(),
                    Content = reader["Content"].ToString()
                });
            }
            sql.Close();
            return View(pages);
               }

        public IActionResult SqlInsert(string name, string message)
        {
            SqlConnection sql = new SqlConnection("server=MZRN\\SQLEXPRESS;database=messageScript;Trusted_Connection=True;TrustServerCertificate=true;");
            SqlCommand cmd = new SqlCommand("insert into message(name,message) values(@name,@message)", sql);
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(message))
            {
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@message", message);
             
                sql.Open();
                cmd.ExecuteNonQuery();
                sql.Close();
                return RedirectToAction("message");
            }
         
            return RedirectToAction("message");
        }
        public IActionResult SqlInsert2(string name, string message)
        {

            using (var context = new messagesContext())
            {
                var newMessage = new messageScript()
                {
                    name = name,
                    message = message,
                };

                context.message.Add(newMessage);
                context.SaveChanges();

                return RedirectToAction("message2");
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}