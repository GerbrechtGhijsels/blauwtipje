using SQLite;

namespace BlauwtipjeApp.Core.Models
{
    [Table("determinationInProgress")]
    public class DeterminationInProgress
    {
        [PrimaryKey]
        public int Id { get; set; }

        public int QuestionID { get; set; }

        public byte[] DeterminationPicture { get; set; }
    }
}
