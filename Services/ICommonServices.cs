namespace SegurOsCar.Services
{
    public interface ICommonServices<T, TInsert, TUpdate>
    {
        public List<string> Errors { get; }
        Task<IEnumerable<T>> Get();
        Task<T?> GetById(string id);
        Task Add(TInsert insertDto);
        Task <T?> Update(string id, TUpdate updateDto);
        Task <T?> Delete(string id);

        bool Validate(TInsert dto);

        bool Validate(TUpdate dto);
    }
}
