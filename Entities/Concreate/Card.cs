using Core.Entities.Abstract;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concreate
{
    public class Card:IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TeamMemberId { get; set; }
        public JobSizeEnum JobSize { get; set; }
        public CardStatus CardStatus { get; set; }
        private static int nextId;
        public Card()
        {
            Id = ++nextId;
        }

    }
}
