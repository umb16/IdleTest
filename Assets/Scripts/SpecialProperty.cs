using System;

public class SpecialProperty<T>
{
    public event Action<T> Changed;
    private T _value;

    public SpecialProperty(T value)
    {
        _value = value;
    }

    public T Value
    {
        get => _value;
        set
        {
            _value = value;
            Changed?.Invoke(value);
        }
    }
}