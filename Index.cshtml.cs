using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
       public DataSet Persons { get; private set; }

        public void OnGet()
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb; Integrated Security=True; DATABASE = PERSONDB";

            string sqlQuery = "select *from PERSON"; 
            
            SqlConnection con = new SqlConnection(connectionString);

            con.Open();
            
            SqlCommand sc = new SqlCommand(sqlQuery,con);

            SqlDataAdapter sda= new SqlDataAdapter(sc);

            Persons = new DataSet();

            sda.Fill(Persons);

            con.Close();
        }
        public void OnPost()
        {
            string Name = Request.Form["Name"];
            string Mobile = Request.Form["Mobile"];
            string Address = Request.Form["Address"];

            string connectionString = "Server=(localdb)\\mssqllocaldb; Integrated Security=True; DATABASE = PERSONDB";
            
            string sqlQuery = "INSERT INTO PERSON (Name,Mobile,Address) VALUES(@Name, @Mobile, @Address)";
            
            SqlConnection con = new SqlConnection(connectionString);
           
            con.Open();
            
            SqlCommand sc = new SqlCommand(sqlQuery, con);

            sc.Parameters.AddWithValue("@Name", Name);
            sc.Parameters.AddWithValue("@Mobile", Mobile);
            sc.Parameters.AddWithValue("@Address", Address);

            _ = sc.ExecuteNonQuery();
            
            con.Close();
        }
    }
}
