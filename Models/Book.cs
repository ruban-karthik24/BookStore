namespace BookStore.Models
{
    public class Book
    {
        public int Id { get; set; } // Adding Id for primary key
        public string Publisher { get; set; }
        public string Title { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorFirstName { get; set; }
        public decimal Price { get; set; }

        public string Year { get; set; }
        public string City { get; set; }

        public string MLACitation
        {
            get
            {
                return $"{AuthorLastName}, {AuthorFirstName}. \"{Title}.\" {City}: {Publisher}, {Year}. Print.";
            }
        }

        public string ChicagoCitation
        {
            get
            {
                return $"{AuthorFirstName} {AuthorLastName}, {Title} ({City}: {Publisher}, {Year}).";
            }
        }
    }

}
