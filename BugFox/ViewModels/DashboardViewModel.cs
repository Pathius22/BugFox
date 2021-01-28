using BugFox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugFox.ViewModels
{
    public class DashboardViewModel
    {
        public List<Bug> Bugs { get; set; }
        public List<User> Users { get; set; }

        public List<Bug> BugsForUser { get { return GetUserBugs(SessionUser); } }

        public int ReportCount { get { return Bugs.Count(); } }

        public User SessionUser { get; set; }

        public int JanCount { get { return MonthCount(1); } }
        public int FebCount { get { return MonthCount(2); } }
        public int MarCount { get { return MonthCount(3); } }
        public int AprCount { get { return MonthCount(4); } }
        public int MayCount { get { return MonthCount(5); } }
        public int JunCount { get { return MonthCount(6); } }
        public int JulCount { get { return MonthCount(7); } }
        public int AugCount { get { return MonthCount(8); } }
        public int SepCount { get { return MonthCount(9); } }
        public int OctCount { get { return MonthCount(10); } }
        public int NovCount { get { return MonthCount(11); } }
        public int DecCount { get { return MonthCount(12); } }

        public int CriticalCount { get { return PriorityCount(0); } }
        public int HighCount { get { return PriorityCount(1); } }
        public int NormalCount { get { return PriorityCount(2); } }
        public int LowCount { get { return PriorityCount(3); } }

        public int ResolvedCount { get { return GetResolvedCount(); } }
        public int UnresolvedCount { get { return GetUnresolvedCount(); } }

        /// <summary>
        /// Gets all of the bugs assigned to a particular user
        /// </summary>
        /// <param name="user">User assigned by SessionManager</param>
        /// <returns>List of type 'Bug'</returns>
        private List<Bug> GetUserBugs(User user)
        {
            List<Bug> _bugs = new List<Bug>();
            foreach (Bug bug in Bugs)
            {
                string _assignee = user.FirstName + " " + user.LastName; 
                if(bug.AssignedTo == _assignee && !bug.isResolved)
                {
                    _bugs.Add(bug);
                }
            }
            return _bugs;
        }

        /// <summary>
        /// Counts the number of Bugs that were submitted in a particular month
        /// </summary>
        /// <param name="month">Month Integer as it refers to DateTime months</param>
        /// <returns>The number of Bugs as an Integer</returns>
        private int MonthCount(int month)
        {
            int count = 0;
            foreach(Bug bug in Bugs)
            {
                if (bug.CreatedOn.Month == month)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Counts the number of Bugs that have a particular Priority
        /// </summary>
        /// <param name="priority">Priority Integer as it refers to Bug.Priority</param>
        /// <returns>The number of Bugs as an Integer</returns>
        private int PriorityCount(int priority)
        {
            int count = 0;
            foreach(Bug bug in Bugs)
            {
                if (bug.Priority == priority)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Counts the number of Resolved Bugs
        /// </summary>
        /// <returns>An Integer that represents the number of Resolved bugs in the Database</returns>
        private int GetResolvedCount()
        {
            int count = 0;
            foreach (Bug bug in Bugs)
            {
                if (bug.isResolved)
                {
                    count++;
                }            
            }
            return count;
        }

        /// <summary>
        /// Counts the number of Unresolved Bugs
        /// </summary>
        /// <returns>An Integer that represents the number of Unresolved bugs in the Database</returns>
        private int GetUnresolvedCount()
        {
            int count = 0;
            foreach (Bug bug in Bugs)
            {
                if (!bug.isResolved)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
