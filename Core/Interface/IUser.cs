using CRUD.Core.Model;

namespace CRUD.Core.Interface
{
    public interface IUser
    {
         public Task<UserModel> UpdateLogin(UserModel model);

         public Task<UserModel> AddUpdate(UserModel model);

         public Task<UserModel> GetItem(int Id);

         public Task<UserModel> GetLogin(string Username);

         public Task<List<UserModel>> GetAll();

         public Task<UserModel> Delete(int Id);
    }
}