using System;
using System.Collections.Generic;
using System.Text;

namespace Question1
{
    public class Contributor
    {
        public string Name { get; set; }
        public List<Skill> Skills { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
