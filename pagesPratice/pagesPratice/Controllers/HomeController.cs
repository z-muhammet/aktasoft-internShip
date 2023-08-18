using pagesPratice.Models;
using pagesPratice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Xml.Linq;

namespace pagesPratice.Controllers
{

    public class HomeController : Controller
    {
        private static int id;
        public static void setId(int id)
        {
            HomeController.id = id;
        }
        public static int getId()
        {
            return HomeController.id;
        }
        private pageEntities1 db = new pageEntities1();
        List<book> books = new List<book>();
        public ActionResult Index()
        {
           
            books = db.books.ToList();
            return View(books);
        }
        public ActionResult create()
        {
            books = db.books.ToList();
        
            return View(books);
        }
        public ActionResult createAction(string name, string author,string p_summary)
        {
            using (db)
            {
                var bookss = new book()
                {
                    book_name = name,
                    book_author = author,
                    book_summary = p_summary
                };
                db.books.Add(bookss);
                db.SaveChanges();
                }
            return RedirectToAction("create");
        }
        public ActionResult update(int id)
        {
            var book = db.books.Find(id);
            if (book != null)
            {
                setId(id);
                return View(book); // Book nesnesini View'a gönder
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult UpdateAction(string name, string author, string p_summary)
        {
            int id = getId();
            using (db)
            {
                if (db.books.Find(id) != null)
                {
                    var book = db.books.Find(id);
                    book.book_name = name;
                    book.book_author = author;
                    book.book_summary = p_summary;
                    db.SaveChanges();

                    return RedirectToAction("Index"); // Başarılı güncelleme sonrası Index sayfasına yönlendirme
                }
                else
                {
                    return RedirectToAction("Update"); // Eğer id bulunamazsa güncelleme sayfasına yönlendirme
                }
            }
        }

        public ActionResult details(int id)
        {
            var book = db.books.Find(id);
            if (book != null)
            {
                return View(book); // Book nesnesini View'a gönder
            }
            else
            {
                return RedirectToAction("Index");
            }
        }   


        public ActionResult delete(int id)
        {
            using (db)
            {
                if (db.books.Find(id) != null)
                {
                    var bookss = db.books.Find(id);
                    db.books.Remove(bookss);
                    db.SaveChanges();
                }
                else
                {
                    return RedirectToAction("index");
                }
                return RedirectToAction("index");
            }

            return View();
        }

    }
}