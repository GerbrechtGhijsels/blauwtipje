namespace BlauwtipjeApp.Core.Interfaces
{
	// https://stackoverflow.com/a/9752155/8633753
    public interface IEquivalence<T>
    {
        bool IsEquivalentTo(T other);
    }
}
