using Doggo.API.Models;

namespace Doggo.API.Data
{
    public interface IRescuesRepository
    {
        AFA Find(string id);
    }
}