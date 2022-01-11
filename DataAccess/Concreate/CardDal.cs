using Core.DataAccess.Memory;
using DataAccess.Abstract;
using Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concreate
{
    public class CardDal:MemoryEntityRepository<Card>,ICardDal
    {
    }
}
