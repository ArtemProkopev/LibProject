using System.Collections.Generic;
using LibProject.Models.Domain;

namespace LibProject.Models
{
    public class AdminDashboardViewModel
    {
        public List<Reader> Readers { get; set; } = new List<Reader>();
        public List<BorrowedBook> BorrowedBooks { get; set; } = new List<BorrowedBook>();
        public List<Book> AllBooks { get; set; } = new List<Book>();
    }
}