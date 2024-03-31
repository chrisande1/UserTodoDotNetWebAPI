namespace UserTodoDotNetWebAPI.Services.Interface
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        public Task<TModel> Create(TModel model);
        public Task<IEnumerable<TModel>> GetAll();
        public Task<TModel?> GetById(Guid Id);
        public Task<TModel?> DeleteById(Guid Id);
    }
}
