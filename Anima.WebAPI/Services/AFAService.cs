using System.Threading.Tasks;
using Anima.WebAPI.Data;
using Anima.WebAPI.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Anima.WebAPI.Services
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
            var savedEntity = _repo.Create(MapToEntity(animal)); //TODO use auto-mapper
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

        public async Task<AFAResponse> Replace(string id, AFARequest dto)
        {
            var dbEntity = _repo.GetOne(id);
            if(dbEntity == null) return null;

            var newEntity = MapToEntity(dto); //TODO use auto-mapper!!
            newEntity.Id = dbEntity.Id;
            
            _repo.Delete(id);
            _repo.Create(newEntity);

            return _mapper.Map<AFAResponse>(newEntity);
        }

        public async Task<AFAResponse> PartialUpdate(string id, AFAPatchRequest animal)
        {
            var dbEntity = _repo.GetOne(id);
            if(dbEntity == null) return null;
            //TODO make this smarter.
            if(animal.Name != null) dbEntity.Name = animal.Name;
            if(animal.Age != null) dbEntity.Age = animal.Age;
            if(animal.City != null) dbEntity.City = animal.City;
            if(animal.ContactNumber != null) dbEntity.ContactNumber = animal.ContactNumber;
            if(animal.Country != null) dbEntity.Country = animal.Country;
            if(animal.Remarks != null) dbEntity.Remarks = animal.Remarks;
            if(animal.Story != null) dbEntity.Story = animal.Story;

            return _mapper.Map<AFAResponse>(_repo.UpdatePartial(dbEntity)); 
        }

        private AFA MapToEntity(AFARequest dto)
        {
            return new AFA()
            {
                Name = dto.Name,
                Age = dto.Age,
                Story = dto.Story,
                ContactNumber = dto.ContactNumber,
                City = dto.City,
                Country = dto.Country,
                Gender = dto.Gender,
                Species = dto.Species,
                Remarks = dto.Remarks
            };
        }
    }
}