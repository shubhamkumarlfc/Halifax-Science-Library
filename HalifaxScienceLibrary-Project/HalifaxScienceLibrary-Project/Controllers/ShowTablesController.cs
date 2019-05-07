using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace HalifaxScienceLibrary_Project.Controllers
{
    public class ShowTablesController : Controller
    {

        private HSLEntities db = new HSLEntities();
        // GET: ShowTables
        public ActionResult Index()
        {

            return View();
        }


        public ActionResult DisplayTables(string index)
        {
            List<String> Tablenames = new List<String>();

            using (MySqlConnection connection = new MySqlConnection("server=Localhost;user id=root;password=root;database=project"))
            {
                string query = "show tables from project";
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Tablenames.Add(reader.GetString(0));
                    }
                }
            }

            ViewBag.Tableslist = Tablenames;

            return View(ViewBag.Tableslist);
        }

        public async Task<ActionResult> author_table_view()
        {
            return View(await db.authors.ToListAsync());
        }
        public async Task<ActionResult> author_articles_table_view()
        {
            return View(await db.author_articles.ToListAsync());
        }
        public async Task<ActionResult> articles_table_view()
        {
            return View(await db.articles.ToListAsync());
        }
        public async Task<ActionResult> customer_table_view()
        {
            return View(await db.customers.ToListAsync());
        }

        public async Task<ActionResult> transaction_table_view()
        {
            return View(await db.transactions.ToListAsync());
        }

        public async Task<ActionResult> item_table_view()
        {
            return View(await db.items.ToListAsync());
        }
     
        public async Task<ActionResult> rent_table_view()
        {
            return View(await db.rents.ToListAsync());
        }

        public async Task<ActionResult> employee_table_view()
        {
            return View(await db.employees.ToListAsync());
        }

        public async Task<ActionResult> magazine_table_view()
        {
            return View(await db.magazines.ToListAsync());
        }

        public async Task<ActionResult> volume_table_view()
        {
            return View(await db.volumes.ToListAsync());
        }

        public async Task<ActionResult> monthly_expense_table_view()
        {
            return View(await db.monthly_expense.ToListAsync());
        }

        public async Task<ActionResult> mothlyexpense_employee_table_view()
        {
            return View(await db.mothlyexpense_employee.ToListAsync());
        }

        public async Task<ActionResult> buy_items_table_view()
        {
            return View(await db.buy_items.ToListAsync());
        }

        public async Task<ActionResult> book_table_view()
        {
            return View(await db.books.ToListAsync());
        }

        public async Task<ActionResult> book_author_table_view()
        {
            return View(await db.book_author.ToListAsync());
        }
    }
}