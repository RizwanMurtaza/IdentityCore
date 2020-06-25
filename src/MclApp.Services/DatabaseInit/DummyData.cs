using System;
using System.Collections.Generic;
using MclApp.Core.Domain;

namespace MclApp.Services.DatabaseInit
{
    public static class DummyData
    {

        public static Guid UserId = new Guid("d66ae70b-1f2c-4ea4-a4d7-0644ad1053f3");
        
        public static List<CyberScore> ScoreData()
        {
            var scores = new List<double>() {4.7,
                4.7,
                4.7,
                4.8,
                5.1,
                3.2,
                3.5,
                3.6,
                3.4,
                4,
                4,
                4,
                4.1,
                4.2,
                3.2,
                5.4,
                5.6,
                5.4,
                5.4,
                5.4  };

            var list = new List<CyberScore>();
            var i = -30;
            foreach (var score in scores)
            {
                
                list.Add( new CyberScore()
                 {
                     Score = score,
                     Date = DateTime.Now.AddDays(i++),
                     UserId = UserId,
                     CreatedDate = DateTime.Now.AddDays(-2),
                     TargetScore = 6.7
                });    
            }

            return list;

        }

        public static List<ImprovementTask> GetTasksData()
        {

            var tasks = new Dictionary<string, string>();
            tasks.Add("Upgrade Firewall firmware", "Open");
            tasks.Add("Install Windows updates on PC-DCCSS", "Completed");
            tasks.Add("Update website wordpress version to 6.2", "Completed");
            tasks.Add("Update to windows 10", "Open");
            tasks.Add("Disable rexec Service", "Completed");
            tasks.Add("Change ssh password", "Open");
            var list = new List<ImprovementTask>();
            int i = -100;
            foreach (var task in tasks)
            {
                list.Add(new ImprovementTask()
                {
                    TaskDescription = task.Key,
                    Status = (ImprovementTaskStatus)Enum.Parse(typeof(ImprovementTaskStatus), task.Value),
                    UserId = UserId,
                    Date = DateTime.Now.AddDays(i+=5),
                    CreatedDate = DateTime.Now.AddDays(-1),
                });
            }
            return list;
        }


        public static List<CyberVulnerability> GetVulnerabilities()
        {

            var list = new List<CyberVulnerability>()
            {
                new CyberVulnerability()
                {
                    Score = 3.4, Risk = VulnerabilityRisk.High, Description = "PostgreSQL weak password (port 5432/tcp)",
                    Date = DateTime.Now.AddDays(-4), UserId = UserId
                },

                new CyberVulnerability()
                {
                    Score = 2.3, Risk = VulnerabilityRisk.High, Description = "Distributed Ruby (dRuby/DRb) Multiple Remote Code Execution Vulnerabilities (port 8787/tcp)",
                    Date = DateTime.Now.AddDays(-4), UserId = UserId
                },
                new CyberVulnerability()
                {
                    Score = 6.7, Risk = VulnerabilityRisk.High, Description = "vsftpd Compromised Source Packages Backdoor Vulnerability (port 6200/tcp)",
                    Date = DateTime.Now.AddDays(-4), UserId = UserId
                },
                new CyberVulnerability()
                {
                    Score = 4.5, Risk = VulnerabilityRisk.Medium, Description = "vsftpd Compromised Source Packages Backdoor Vulnerability (port 21/tcp)",
                    Date = DateTime.Now.AddDays(-4), UserId = UserId
                },
                new CyberVulnerability()
                {
                    Score = 5.6, Risk = VulnerabilityRisk.High, Description = "SSH Brute Force Logins With Default Credentials Reporting (port 22/tcp)",
                    Date = DateTime.Now.AddDays(-4), UserId = UserId
                },
                new CyberVulnerability()
                {
                    Score = 4.3, Risk = VulnerabilityRisk.Low, Description = "SSL/TLS: Diffie-Hellman Key Exchange Insufficient DH Group Strength Vulnerability (port 5432/tcp)",
                    Date = DateTime.Now.AddDays(-4), UserId = UserId
                },

            };
            
            return list;
        }


        public static List<UserTask> GetUserTasksData()
        {

            var tasks = new Dictionary<string, string>();
            tasks.Add("Upgrade firmware on firewall	01/07/2020", "Yes");
            tasks.Add("Remove access to external ports", "No");
            tasks.Add("Replace remove access solution", "Yes");
            var list = new List<UserTask>();
            int i = -100;
            foreach (var task in tasks)
            {
                list.Add(new UserTask()
                {
                    TaskDescription = task.Key,
                    Status = (UserTaskStatus)Enum.Parse(typeof(UserTaskStatus), task.Value),
                    UserId = UserId,
                    Date = DateTime.Now.AddDays(i += 5),
                    DueDate = DateTime.Now.AddDays(20),
                    CreatedDate = DateTime.Now.AddDays(-1),
                });
            }

            list.Add(new UserTask()
            {
                TaskDescription ="Over Due testing task",
                Status = UserTaskStatus.No,
                UserId = UserId,
                Date = DateTime.Now.AddDays(-100),
                DueDate = DateTime.Now.AddDays(-20),
                CreatedDate = DateTime.Now.AddDays(-101),
            });

            return list;
        }

        public static List<Narrative> GetNarratives()
        {

            var list = new List<Narrative>();
            list.Add(new Narrative()
            {
                UserId = UserId,
                Description = "This is just test the narratives number 1",
            });

            list.Add(new Narrative()
            {
                UserId = UserId,
                Description = "This is just test the narratives number 2",
            });

            list.Add(new Narrative()
            {
                UserId = UserId,
                Description = "This is just test the narratives number 4",
            });


            return list;
        }

    }

    
}
