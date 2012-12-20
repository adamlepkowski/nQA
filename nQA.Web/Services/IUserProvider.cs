using nQA.Model.Entities;

namespace nQA.Web.Services
{
    public interface IUserProvider
    {
        User CurrentUser { get; set; }
    }
}