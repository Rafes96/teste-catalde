using Business.Interfaces.Repositorys;
using Business.Models;
using Data.Context;

namespace Data.Repository
{
    public class OcorrenciaRepository : Repository<Ocorrencia>, IOcorrenciaRepository
    {
        public OcorrenciaRepository(ApiDbContext db) : base(db)
        {
        }
    }
}
