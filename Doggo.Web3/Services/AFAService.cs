using System.Threading.Tasks;
using Doggo.API.Data;
using Doggo.API.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Doggo.API.Services
{
    public class AFAService : IAFAService
    {
        private IRescuesRepository _repo;
        private IMapper _mapper;
        public AFAService(IRescuesRepository repo)
        {
            _repo = repo;
            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<AFA, AFAResponse>()).CreateMapper();
        }

        public AFAResponse Add(AFARequest animal)
        {
            //TODO refactor. Use AutoMapper
            var savedEntity = _repo.Create(new AFA(){Name = animal.Name, Story = animal.Story, City = animal.City, 
                                                    Species = animal.Species, Gender = animal.Gender});
            return _mapper.Map<AFAResponse>(savedEntity);
        }

        public async Task Delete(string id)
        {
            _repo.Delete(id);
        }

        public async Task<IEnumerable<AFAResponse>> GetAll()
        {
            return _repo.GetAll().Select(entity => _mapper.Map<AFAResponse>(entity));
        }

        public async Task<IEnumerable<AFAResponse>> Filter(Filter filter)
        {
            return _repo.Filter(filter)
                        .Select(entity => _mapper.Map<AFAResponse>(entity));
        }

        public async Task<AFAResponse> GetOne(string id)
        {
            var dbEntity = _repo.GetOne(id);
            if (dbEntity == null) return null;
            var dto = _mapper.Map<AFAResponse>(dbEntity);
            return dto;
        }

        public string Ping()
        {
            return "OK, good to go.";
        }
    }
}