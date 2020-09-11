using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FirstCodeDb
{
    public class Taxonomy : BaseModel
    {
        public Taxonomy()
        {
            Masters = new HashSet<Master>();
        }

        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }


        public ICollection<Master> Masters { get; set; }
    }
}
