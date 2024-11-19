using System.ComponentModel.DataAnnotations;

namespace LazyLoadingEagerLoading.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Book> Books { get; set; }
    }
}

//what virtual does like->
/*
it tells entity framework that this property books can be overridden and these is necessary for ef lazy loading to work
how lazy loading works like-> when you fetch an author from database books property is not loaded immediately then
instead it stays empty until you specifically try to access books property and EF makes separate query to database to fetch
related books for that author and why we are using virtual-> without virtual EF cannot create proxy class to enable lazy loading
so you must include virtual if you want EF to load related data automatically when accessed
*/