using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concreate
{
    public class TeamMemberManager : ITeamMemberService
    {
        ITeamMemberDal _teamMemberDal;

        public TeamMemberManager(ITeamMemberDal teamMemberDal)
        {
            _teamMemberDal = teamMemberDal;
        }

        public void Add(TeamMember entity)
        {
            _teamMemberDal.Add(entity);
        }

        public void Delete(TeamMember entity)
        {
            _teamMemberDal.Delete(entity);
        }

        public List<TeamMember> GetAll()
        {
            return _teamMemberDal.GetAll();
        }

        public TeamMember GetById(int id)
        {
            return _teamMemberDal.Get(e => e.Id == id);
        }

        public void Update(TeamMember entity)
        {
            _teamMemberDal.Update(entity);
        }
    }
}
