    using System.ComponentModel.DataAnnotations.Schema;

    namespace GADApplication.Models
    {
        public class Mandate
        {
            public int Id { get; set; }

            public string? RepublicAct { get; set; }
            public string? ChedMemo { get; set; }
            public string? Description { get; set; }

            public int? ActivityId { get; set; }
            public virtual Activity? Activity { get; set; }
            //migrate this if i have already populated the database of activities
        }
    }
