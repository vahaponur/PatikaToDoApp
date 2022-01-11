using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concreate;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concreate
{
    public class CardManager : ICardService
    {
        ICardDal _cardDal;
        public CardManager(ICardDal cardDal)
        {
            _cardDal = cardDal;
        }
        public void Add(Card entity)
        {
            _cardDal.Add(entity);
        }

        public void Delete(Card entity)
        {
            _cardDal.Delete(entity);
        }

        public List<Card> GetAll()
        {
            return _cardDal.GetAll();
        }

        public List<Card> GetByCardTitle(string title)
        {
            return _cardDal.GetAll(e => e.Title == title);
        }

        public Card GetById(int id)
        {
            return _cardDal.Get(e => e.Id == id);
        }

        public List<Card> GetByStatus(CardStatus cardStatus)
        {
            return _cardDal.GetAll(e => e.CardStatus == cardStatus);

        }

        public void Update(Card entity)
        {
            _cardDal.Update(entity);
        }
    }
}
