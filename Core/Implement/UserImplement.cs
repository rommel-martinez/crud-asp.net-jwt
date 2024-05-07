using CRUD.Core.Interface;
using CRUD.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Core.Implement
{
    public class UserImplement : IUser
    {
        private readonly AppDbContext context;

        public UserImplement(AppDbContext _AppDbContext)
        {
            context = _AppDbContext;
        }

        public async Task<UserModel> UpdateLogin(UserModel model)
        {
            if(model.RecordId > 0){
                var mdl = await GetItem(model.RecordId);

                mdl.Password = model.Password;
            }

            await context.SaveChangesAsync();

            return await GetItem(model.RecordId);
        }

        public async Task<UserModel> AddUpdate(UserModel model)
        {
            if(model.RecordId == 0){
                context.Users.Add(model);
            }
            else{
                var mdl = await GetItem(model.RecordId);
                mdl.FirstName = model.FirstName;
                mdl.LastName = model.LastName;
                mdl.Age = model.Age;
                mdl.Gender = model.Gender;
                mdl.Mobile = model.Mobile;
                mdl.Email = model.Email;
                mdl.Active = model.Active;
            }

            await context.SaveChangesAsync();

            return await GetItem(model.RecordId);
        }

        public async Task<UserModel> Delete(int Id)
        {
            var mdl = await GetItem(Id);
            if (mdl != null){
                mdl.Active = false;
                await context.SaveChangesAsync();
            }

            return mdl;
        }

        public async Task<List<UserModel>> GetAll()
        {
            return await context.Users.Where(x => x.Active == true).ToListAsync();
        }

        public async Task<UserModel> GetItem(int Id)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.RecordId == Id);
        }

        public async Task<UserModel> GetLogin(string Username)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.Username == Username);
        }        

    }
}