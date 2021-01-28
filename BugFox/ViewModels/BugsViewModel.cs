using BugFox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugFox.ViewModels
{
    public class BugsViewModel
    {
        public List<Bug> Bugs { get; set; }
        public List<User> Users { get; set; }
        public Bug Bug { get; set; }
        public User SessionUser { get; set; }
    }
}
