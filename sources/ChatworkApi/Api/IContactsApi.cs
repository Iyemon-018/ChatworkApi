namespace ChatworkApi.Api
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IContactsApi
    {
        Task<IEnumerable<ContactModel>> GetAsync();
    }
}