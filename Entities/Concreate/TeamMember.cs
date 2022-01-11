using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concreate
{
    public class TeamMember:Person
    {
        private static int nextId;
        public TeamMember()
        {
            Id = ++nextId;
        }
    }
}
