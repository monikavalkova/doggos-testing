using System.Threading.Tasks;
using Doggo.API.Data;
using Doggo.API.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Doggo.API.Services
{
    public class AFAService : IAFAService
    {
        private IRescuesRepository _repo;
        private Mapper _mapper;
        public AFAService(IRescuesRepository repo)
        {
            _repo = repo;
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<AFA, AFAResponse>());
            _mapper = new Mapper(mapperConfiguration);
        }

        public AFAResponse Add(AFARequest animal)
        { 
            //TODO add an in-memory database
            throw new System.NotImplementedException();
        }

        public async Task<AFAResponse> GetOne(string id)
        {
            var dbEntity = _repo.Find(id);
            if(dbEntity == null) return null;
            var dto = _mapper.Map<AFAResponse>(dbEntity);
            return dto;
        }

        public string Ping()
        {
            return "OK, good to go.";
        }
    }
}