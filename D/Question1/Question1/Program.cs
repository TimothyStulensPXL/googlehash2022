using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Question1
{
    class Program
    {
        private static TextReader stdin = System.Console.In;
        private static TextWriter stdout = System.Console.Out;

        static void Main(string[] args)
        {
            List<Contributor> contributors = new List<Contributor>();
            List<Project> projects = new List<Project>();

            string[] start = stdin.ReadLine().Split(" ");
            int contributorCount = int.Parse(start[0]);
            int projectCount = int.Parse(start[1]);

            for (int i = 0; i < contributorCount; i++)
            {
                string[] contributorString = stdin.ReadLine().Split(" ");
                List<Skill> skills = new List<Skill>();

                // create skills of contributor
                for (int j = 0; j < int.Parse(contributorString[1]); j++)
                {
                    string[] skillString = stdin.ReadLine().Split(" ");
                    Skill skill = new Skill()
                    {
                        Name = skillString[0],
                        Level = int.Parse(skillString[1])
                    };

                    skills.Add(skill);
                }

                Contributor contributor = new Contributor()
                {
                    Name = contributorString[0],
                    Skills = skills
                };

                contributors.Add(contributor);
            }

            for(int i = 0; i < projectCount; i++)
            {
                string[] projectString = stdin.ReadLine().Split(" ");
                List<Skill> roles = new List<Skill>();

                // create skills of project
                for (int j = 0; j < int.Parse(projectString[4]); j++)
                {
                    string[] skillString = stdin.ReadLine().Split(" ");
                    Skill role = new Skill()
                    {
                        Name = skillString[0],
                        Level = int.Parse(skillString[1])
                    };

                    roles.Add(role);
                }

                Project project = new Project()
                {
                    Name = projectString[0],
                    Duration = int.Parse(projectString[1]),
                    Score = int.Parse(projectString[2]),
                    BestBefore = int.Parse(projectString[3]),
                    Roles = roles,
                    Contributors = new List<Contributor>()
                };

                projects.Add(project);

            }

            List<Project> unfinishedProjects = projects.OrderByDescending(x => x.Score).ToList();
            List<Project> finishedProjects = new List<Project>();

            // set contributors on projects
            int lastUnfinishedProjects = -1;
            while(unfinishedProjects.Count > 0 && unfinishedProjects.Count != lastUnfinishedProjects)
            {
                lastUnfinishedProjects = unfinishedProjects.Count;
                for (int i = 0; i < unfinishedProjects.Count; i++)
                {
                    foreach(Skill role in unfinishedProjects[i].Roles)
                    {
                        role.Contributor = null;
                    }
                    for (int j = 0; j < unfinishedProjects[i].Roles.Count; j++)
                    {
                        if (unfinishedProjects[i].Roles[j].Contributor == null)
                        {
                            Contributor contributor = contributors.FirstOrDefault(x => 
                                !unfinishedProjects[i].Contributors.Contains(x) && 
                                x.Skills.Any(y => y.Name == unfinishedProjects[i].Roles[j].Name && y.Level >= unfinishedProjects[i].Roles[j].Level));

                            if (contributor != null)
                            {
                                unfinishedProjects[i].Contributors.Add(contributor);
                                unfinishedProjects[i].Roles[j].Contributor = contributor;
                            }
                        }
                    }
                    if (unfinishedProjects[i].IsFinished())
                    {
                        finishedProjects.Add(unfinishedProjects[i]);
                    }
                }
                unfinishedProjects = unfinishedProjects.Where(x => x.Roles.Count > x.Contributors.Count).ToList();
            }




            using (StreamWriter writer = File.CreateText("newfile.txt"))
            {
                writer.WriteLine(finishedProjects.Count);
                for (int i = 0; i < finishedProjects.Count; i++)
                {
                    writer.WriteLine(finishedProjects[i].Name);
                    writer.WriteLine(finishedProjects[i].ToString());
                }
            }
        }
    }
}
