namespace IEduZimAPI.CoreClasses.BaseFiles
{
    public class GenericProp<T>
    {
        private T _value;

        public T Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public static implicit operator T(GenericProp<T> value)
        {
            return value.Value;
        }

        public static implicit operator GenericProp<T>(T value)
        {
            return new GenericProp<T> { Value = value };
        }
    }
}
