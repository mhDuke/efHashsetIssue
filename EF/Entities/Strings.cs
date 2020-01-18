using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Entities
{
    public class Strings
    {
        private HashSet<string> _stringies;

        #region Constructors
        public Strings(int id)
        {
            Id = id;
            _stringies = new HashSet<string>();
        }
        private Strings() { }
        #endregion

        public int Id { get; set; }
        public IEnumerable<string> Stringies { get => _stringies; private set => _stringies = new HashSet<string>(value); }


        public void AddString(string s) => _stringies.Add(s);

    }
}
