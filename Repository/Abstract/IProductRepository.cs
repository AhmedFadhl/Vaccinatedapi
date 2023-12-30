using Vaccinatedapi.models;

namespace Vaccinatedapi.Repository.Abstract
{
    public interface IProductRepository
    {
        bool Add(parents model);
       Task< bool> Edit(parents model);

    }
}
