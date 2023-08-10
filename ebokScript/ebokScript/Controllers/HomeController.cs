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
        public IActionResult page()
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
            return View(pages);
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
                return View(messages);
        }

        public IActionResult ShowPage(int id)
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

            if (pages == null)
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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}