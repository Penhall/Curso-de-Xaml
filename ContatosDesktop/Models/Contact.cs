using SQLite;

namespace ContatosDesktop.Models
{
    public class Contact
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        [MaxLength(12)]
        public string Fone { get; set; }

    }
}

