using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Question1
{
    public class Project
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public int Score { get; set; }
        public int BestBefore { get; set; }
        public List<Skill> Roles { get; set; }

        public List<Contributor> Contributors { get; set; }

        public override string ToString()
        {
            return string.Join(" ", Contributors);
        }

        public bool IsFinished()
        {
            if (AllRolesAssigned())
            {
                ImproveSkills();
                return true;
            }
            return false;
        }

        private bool AllRolesAssigned()
        {
            return Roles.All(x => x.Contributor != null);
        } 

        private void ImproveSkills()
        {
            foreach(Contributor contributor in Contributors)
            {
                foreach (Skill skill in contributor.Skills)
                {
                    if (Roles.Count(x => x.Name == skill.Name && skill.Level <= x.Level) == 1) {
                        skill.Level++;
                    }
                }
            }
        }
    }
}
