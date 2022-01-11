using Entities.Concreate;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICardService:ICrudBase<Card>
    {
        List<Card> GetByStatus(CardStatus cardStatus);
        List<Card> GetByCardTitle(string title);
    }
}
