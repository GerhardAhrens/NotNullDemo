namespace NotNullDemo
{
    public static class ReturnExtensions
    {
        public static TResult Match<T, TResult>(this Return<T> option, Func<T, TResult> some, Func<TResult> none)
            => option.IsSuccess ? some(option.Value!) : none();

        public static Return<TResult> Map<T, TResult>(this Return<T> option, Func<T, TResult> mapper)
            => option.IsSuccess ? Return<TResult>.Success(mapper(option.Value!)) : Return<TResult>.Fail();

        public static Return<TResult> Bind<T, TResult>(this Return<T> option, Func<T, Return<TResult>> binder)
            => option.IsSuccess ? binder(option.Value!) : Return<TResult>.Fail();

        public static Return<TResult> Select<T, TResult>(this Return<T> option, Func<T, TResult> selector)
            => option.Map(selector);

        public static Return<TResult> SelectMany<T, U, TResult>(this Return<T> option, Func<T, Return<U>> bind, Func<T, U, TResult> project)
            => option.IsSuccess ? bind(option.Value!).Map(u => project(option.Value!, u)) : Return<TResult>.Fail();
    }
}