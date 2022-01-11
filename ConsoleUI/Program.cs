using Business.Abstract;
using Business.Concreate;
using DataAccess.Concreate;
using Entities.Concreate;
using Entities.Enums;
using System;
using System.Linq;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            int secim;
            ICardService cardService = new CardManager(new CardDal());
            ITeamMemberService teamMemberService = new TeamMemberManager(new TeamMemberDal());
            TeamMember t1 = new TeamMember { FirstName = "Vahap", LastName="YILDIRIM" };
            TeamMember t2 = new TeamMember { FirstName = "Beyza", LastName="YILDIRIM" };
            TeamMember t3 = new TeamMember { FirstName = "Hamit", LastName="YILDIRIM" };
            Card c1 = new Card
            {
                Title = "Some title",
                Description = "Lorem ipsum dolor sit amet",
                CardStatus = CardStatus.TODO,
                JobSize = JobSizeEnum.M,
                TeamMemberId=t1.Id
            };
            Card c2 = new Card
            {
                Title = "Some title3",
                Description = "Lorem ipsum dolor sit amet",
                CardStatus = CardStatus.IN_PROGRESS,
                JobSize = JobSizeEnum.M,
                TeamMemberId = t2.Id
            };
            Card c3 = new Card
            {
                Title = "Some title 2",
                Description = "Lorem ipsum dolor sit amet",
                CardStatus = CardStatus.DONE,
                JobSize = JobSizeEnum.M,
                TeamMemberId = t3.Id
            };
            teamMemberService.Add(t1);
            teamMemberService.Add(t2);
            teamMemberService.Add(t3);
            cardService.Add(c1);
            cardService.Add(c2);
            cardService.Add(c3);
            MainScreenWrite();

            do
            {
                secim = int.Parse(Console.ReadLine());
                switch (secim)
                {
                    case 1:
                        ListBoard(cardService, teamMemberService);
                        break;
                    case 2:
                        AddCard(cardService, teamMemberService);
                        break;
                    case 3:
                        DeleteCard(cardService);
                        break;
                    case 4:
                        ChangeCardStatus(cardService);
                        break;
                    default:
                        break;
                }
            } while (secim!=5);
        }
        static void MainScreenWrite()
        {
            Console.WriteLine("Lütfen yapmak istediğiniz işlemi seçiniz ");
            Console.WriteLine("*****************************************");
            Console.WriteLine("(1) Board Listelemek ");
            Console.WriteLine("(2) Board'a Kart Eklemek");
            Console.WriteLine("(3) Board'dan Kart Silmek");
            Console.WriteLine("(4) Kart Taşımak");
            Console.WriteLine("(5) Çıkış");
        }
        static void ListBoard(ICardService cardService,ITeamMemberService teamMemberService)
        {
            var todo = cardService.GetByStatus(CardStatus.TODO);
            var in_progress = cardService.GetByStatus(CardStatus.IN_PROGRESS);
            var done = cardService.GetByStatus(CardStatus.DONE);
            Console.WriteLine("TODO Line");
            Console.WriteLine("*********");
            foreach (var item in todo)
            {
                Console.WriteLine("Başlık: "+item.Title);
                Console.WriteLine("İçerik: "+item.Description);
                Console.WriteLine("Atanan Kişi: "+teamMemberService.GetById(item.TeamMemberId).FirstName+ " "+teamMemberService.GetById(item.TeamMemberId).LastName);
                Console.WriteLine("-");
            }
            if (todo.Count==0)
            {
                Console.WriteLine("-BOŞ-");
            }

            Console.WriteLine("IN PROGRESS Line");
            Console.WriteLine("*********");
            foreach (var item in in_progress )
            {
                Console.WriteLine("Başlık: " + item.Title);
                Console.WriteLine("İçerik: " + item.Description);
                Console.WriteLine("Atanan Kişi: " + teamMemberService.GetById(item.TeamMemberId).FirstName + " " + teamMemberService.GetById(item.TeamMemberId).LastName);
                Console.WriteLine("-");
            }
            if (in_progress.Count == 0)
            {
                Console.WriteLine("-BOŞ-");
            }

            Console.WriteLine("DONE Line");
            Console.WriteLine("*********");
            foreach (var item in done )
            {
                Console.WriteLine("Başlık: " + item.Title);
                Console.WriteLine("İçerik: " + item.Description);
                Console.WriteLine("Atanan Kişi: " + teamMemberService.GetById(item.TeamMemberId).FirstName + " " + teamMemberService.GetById(item.TeamMemberId).LastName);
                Console.WriteLine("-");
            }
            if (done.Count == 0)
            {
                Console.WriteLine("-BOŞ-");
            }
            MainScreenWrite();
        }
        static void AddCard(ICardService cardService,ITeamMemberService teamMemberService)
        {
            Console.WriteLine("Başlık Giriniz : ");
            string title = Console.ReadLine();
            Console.WriteLine("İçerik Giriniz : ");
            string description = Console.ReadLine();

            Console.WriteLine("Büyüklük Seçiniz XS(1),S(2),M(3),L(4),XL(5):");
            JobSizeEnum jobSize = (JobSizeEnum)int.Parse(Console.ReadLine());
            Console.WriteLine("Kişi Id'si giriniz:");
            foreach (var item in teamMemberService.GetAll())
            {
                Console.WriteLine(item.FirstName + " "+item.LastName +"--Id:"+item.Id);
            }
            int personId = int.Parse(Console.ReadLine());
            if (teamMemberService.GetById(personId) == null)
            {
                Console.WriteLine("Hatalı giriş yaptınız");
                Console.WriteLine("Yeniden dene");
                AddCard(cardService, teamMemberService);
            }
            Card cardToAdd = new Card
            {
                CardStatus = CardStatus.TODO,
                Description = description,
                Title = title,
                JobSize = jobSize,
                TeamMemberId = personId
            };
            cardService.Add(cardToAdd);
            Console.WriteLine("Card eklendi.");
            MainScreenWrite();
        }
        static void DeleteCard(ICardService cardService)
        {
            Console.WriteLine("Silinecek kartın başlığını giriniz");
            string title = Console.ReadLine();
            var cardToDelete = cardService.GetByCardTitle(title);
            if (cardToDelete.Count ==0)
            {
                Console.WriteLine("Aradığınız kriterlere uygun kart bulunamadı. Lütfen seçim yapınız.");
                Console.WriteLine("Silmeyi sonlandırmak için (1)");
                Console.WriteLine("Yeniden denemek için (2)");
                int giris =int.Parse (Console.ReadLine());
                if (giris ==1)
                {
                    MainScreenWrite();
                }
                if (giris==2)
                {
                    DeleteCard(cardService);
                }
            }
            else
            {
                foreach (var item in cardToDelete)
                {
                    cardService.Delete(item);
                    Console.WriteLine("Cart silindi");
                }
                MainScreenWrite();
            }
        }

        static void ChangeCardStatus(ICardService cardService)
        {
            Console.WriteLine("Öncelikle silmek istediğiniz kartı seçmeniz gerekiyor.");
            Console.WriteLine("Lütfen kart başlığını yazınız:  ");
            string title = Console.ReadLine();
            var card = cardService.GetByCardTitle(title).FirstOrDefault();
            if (card == null)
            {
                Console.WriteLine(" Aradığınız krtiterlere uygun kart board'da bulunamadı. Lütfen bir seçim yapınız.\n İşlemi sonlandırmak için: (1)\nYeniden denemek için: (2)");
                int secim = int.Parse(Console.ReadLine());
                if (secim == 1)
                {
                    MainScreenWrite();
                }
                if (secim == 2)
                {
                    ChangeCardStatus(cardService);
                }
            }
            else
            {
                Console.WriteLine("Bulunan Kart Bilgisi:");
                Console.WriteLine("*********************");
                Console.WriteLine("Başlık: "+card.Title);
                Console.WriteLine("İçerik: "+card.Description);
                Console.WriteLine("Atanan kişi: "+card.TeamMemberId);
                Console.WriteLine("Büyüklük: " + card.JobSize);
                Console.WriteLine("Line: "+card.CardStatus);
                Console.WriteLine("Lütfen taşımak istediğiniz Line'ı Seçiniz ");
                Console.WriteLine("(1) TODO\n(2) IN PROGRESS\n(3) DONE");
                CardStatus line = (CardStatus)int.Parse(Console.ReadLine());
                card.CardStatus = line;
            }
         


        }
    }
}
