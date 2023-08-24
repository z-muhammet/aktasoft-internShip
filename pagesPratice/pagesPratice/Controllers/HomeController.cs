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
using System.Collections;
using Microsoft.Ajax.Utilities;

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
        public ActionResult Index(List<book> books)
        {
            if (TempData["data"]!=null)
            {
                ViewBag.data = TempData["data"] as String;
            }else if (TempData["data1"] != null)
            {
                ViewBag.data1 = TempData["data1"] as String;
            }else if (TempData["dataf"] != null)
            {
                ViewBag.data2 = TempData["dataf"] as List<filter>;
            }
            else
            {
                ViewBag.data = null;
            }
            if (books == null)
            {
                var modelview = new MyViewModel
                {
                    Books = db.books.ToList(),
                    Categories = db.book_category.ToList()
                };
                return View(modelview);
            }
            else
            {
                var modelview = new MyViewModel
                {
                    Books = books,
                    Categories = db.book_category.ToList()
                };
                return View(modelview);
            }
        }

        public ActionResult create()
        {
            books = db.books.ToList();

            return View(books);
        }

        public class filter
        {
            public int category { get; set; }
            public int page { get; set; }
            public int order { get; set; }
            public Boolean sc { get; set; }
        }

        public ActionResult Filter(int category = 0, int page = 0, int order = 0, Boolean sc = true)
        {
            var data = new filter { 
            category = category,
            page = page,
            order = order,
            sc = sc
            };

            List<book> filterBooks = new List<book>();
            filterBooks = db.books.ToList();
           
            if(category == -1){
                filterBooks = filterBooks.Where(m => m.book_page >= page).ToList();
            }
            else{
                filterBooks = filterBooks.Where(m => m.book_page >= page && m.book_category == category).ToList();
            }
            switch (order)
            {
                //sc de asc desc kontrolu yapıyor

                case 0:
                    if (sc)
                    {
                        filterBooks = filterBooks.OrderBy(m => m.id).ToList();
                    }
                    else
                    {
                        filterBooks = filterBooks.OrderByDescending(m => m.id).ToList();
                    }
                    break;
                case 1:
                    if (sc)
                    {
                        filterBooks = filterBooks.OrderBy(m => m.book_name).ToList();
                    }
                    else
                    {
                        filterBooks = filterBooks.OrderByDescending(m => m.book_name).ToList();
                    }
                    break;
                case 2:
                    if (sc)
                    {
                        filterBooks = filterBooks.OrderBy(m => m.book_author).ToList();
                    }
                    else
                    {
                        filterBooks = filterBooks.OrderByDescending(m => m.book_author).ToList();
                    }
                    break;
                case 3:
                    if (sc)
                    {
                        filterBooks = filterBooks.OrderBy(m => m.book_page).ToList();
                    }
                    else
                    {
                        filterBooks = filterBooks.OrderByDescending(m => m.book_page).ToList();
                    }
                    break;
                case 4:
                    if (sc)
                    {
                        filterBooks = filterBooks.OrderBy(m => m.book_category).ToList();
                    }
                    else
                    {
                        filterBooks = filterBooks.OrderByDescending(m => m.book_category).ToList();
                    }
                    break;
                case 5:
                    if (sc)
                    {
                        filterBooks = filterBooks.OrderBy(m => m.book_summary).ToList();
                    }
                    else
                    {
                        filterBooks = filterBooks.OrderByDescending(m => m.book_summary).ToList();
                    }
                    break;
                default:
                    break;
            }

            var modelview = new MyViewModel
            {
                Books = filterBooks,
                Categories = db.book_category.ToList()
            };
            ViewBag.dataf = data;

            return View("index", modelview);

        }
        public ActionResult createAction(string name, string author, string p_summary, int b_page, int cat, DateTime release_date)
        {
            book bookss;
            using (db)
            {
                bookss = new book()
                {
                    book_name = name,
                    book_author = author,
                    book_summary = p_summary,
                    book_page = b_page,
                    book_category = cat,
                    release_date = release_date,
                };
                db.books.Add(bookss);
                db.SaveChanges();
            }


            return RedirectToAction("index");
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
        public ActionResult UpdateAction(string name, string author, string p_summary, int b_page, int cat, DateTime release_date)
        {
            int id = getId();
            if (db.books.Find(id) == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                using (db)
                {
                    if (db.books.Find(id) != null)
                    {
                        var book = db.books.Find(id);
                        var data = (book.book_name + " başarı ile ");
                        book.book_name = name;
                        data += (book.book_name + " olarak güncellendi");
                        book.book_author = author;
                        book.book_summary = p_summary;
                        book.book_page = b_page;
                        book.book_category = cat;
                        book.release_date = release_date;
                        TempData["data1"] = data;
                        db.SaveChanges();
                        return RedirectToAction("Index"); // Başarılı güncelleme sonrası Index sayfasına yönlendirme
                    }
                    else
                    {
                        return RedirectToAction("Update"); // Eğer id bulunamazsa güncelleme sayfasına yönlendirme
                    }
                }

            }
        }

        public ActionResult details(int id)
        {
            if (id != null)
            {
                book bookss = db.books.Find(id);
                if (bookss != null)
                {
                    return View(bookss);
                }
                else
                {
                    return RedirectToAction("Index");
                }
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
                    var data = (bookss.book_name +" başarı ile silindi");
                    db.books.Remove(bookss);
                    db.SaveChanges();
                    TempData["data"] = data;
                    return RedirectToAction("index");
                }
                else
                {
                    return View();
                }
            }
        }

    }
}