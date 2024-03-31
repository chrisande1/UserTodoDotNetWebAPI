using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using UserTodoDotNetWebAPI.Data;
using UserTodoDotNetWebAPI.Model;
using UserTodoDotNetWebAPI.Services.Interface;

namespace UserTodoDotNetWebAPI.Services.Repository
{
    public abstract class GenericRepository<TModel> where TModel : Entity
    {
        protected readonly ApplicationDBContext _dBContext;

        protected GenericRepository(ApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async virtual Task<TModel> Create(TModel model)
        {
            await _dBContext.Set<TModel>().AddAsync(model);

            model.CreatedAt = DateTime.Now.ToString();
            await _dBContext.SaveChangesAsync();
            return model;
        }

        public async virtual Task<IEnumerable<TModel>> GetAll()
        {
            return await _dBContext.Set<TModel>()
                .Where(model => model.DeletedAt == null)
                .ToListAsync();
        }

        public async virtual Task<TModel?> GetById(Guid Id)
        {
            return await _dBContext.Set<TModel>()
                .FindAsync(Id);
                

        }

        public async virtual Task<TModel?> DeleteById(Guid Id)
        {
            var targetModel = await GetById(Id);

            targetModel.DeletedAt = DateTime.Now.ToString();

            await _dBContext.SaveChangesAsync();
            return targetModel;
        }
    }   
}
