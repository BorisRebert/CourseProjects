using System.Data;
using DataBaseProject.DBModels;
using Microsoft.Data.SqlClient;

namespace DataBaseProject
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Browser chromeBrowser = new Browser {BrowserName = "Chrome"};
                Browser firefoxBrowser = new Browser { BrowserName = "Firefox" };
                Browser edgeBrowser = new Browser { BrowserName = "Edge" };

                User userAdmin = new User { Role = "admin", Password = "12345", Login = "administrator" };
                User userEditor = new User { Role = "editor", Password = "678910", Login = "user1" };

                TestData test = new TestData { TestName = "LoginTest" };
                
                userAdmin.Tests.Add(test);
                userEditor.Tests.Add(test);
                
                chromeBrowser.Users.Add(userAdmin);
                chromeBrowser.Users.Add(userEditor);
                
                firefoxBrowser.Users.Add(userAdmin);
                firefoxBrowser.Users.Add(userEditor);
                
                edgeBrowser.Users.Add(userAdmin);
                edgeBrowser.Users.Add(userEditor);
                
                db.Browsers.Add(chromeBrowser);
                db.Browsers.Add(firefoxBrowser);
                db.Browsers.Add(edgeBrowser);
                db.SaveChanges();

                var users = db.Browsers.SelectMany(x => x.Users).ToList();
            }
            
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = @"Server=(localdb)\mssqllocaldb; Database=Config;Trusted_Connection=True;";
                
                SqlCommand sqlCommand = new SqlCommand("SELECT * from dbo.Users", conn);

                conn.Open();
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        IDataReader item = reader;
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.WriteLine(item[i].ToString() + " " + item.GetName(i));
                        }
                    }
                }
            }
        }
    }
}