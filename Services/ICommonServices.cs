namespace SegurOsCar.Services
{
    public interface ICommonServices<T, TInsert, TUpdate>
    {
        public List<string> Errors { get; }
        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);
        Task Add(TInsert insertDto);
        Task Update(int id, TUpdate updateDto);
        Task Delete(int id);

        bool Validate(TInsert dto);

        bool Validate(TUpdate dto);
    }
}
