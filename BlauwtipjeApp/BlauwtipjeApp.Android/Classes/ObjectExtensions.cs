using System;

namespace BlauwtipjeApp.Droid.Classes
{
    /// <summary>
    /// This class is for converting .Net objects to and from Java objects,
    /// and is needed for creating a custom filter for a recyclerview
    /// because of this problem: https://forums.xamarin.com/discussion/46051/custom-filter-problem
    /// Code in this file is from this gist: https://gist.github.com/Cheesebaron/9876783
    /// </summary>
    public static class ObjectExtensions
    {
        public static TObject ToNetObject<TObject>(this Java.Lang.Object value)
        {
            if (value == null)
                return default(TObject);

            if (!(value is JavaHolder))
                throw new InvalidOperationException("Unable to convert to .NET object. Only Java.Lang.Object created with .ToJavaObject() can be converted.");

            TObject returnVal;
            try { returnVal = (TObject)((JavaHolder)value).Instance; }
            finally { value.Dispose(); }
            return returnVal;
        }

        public static Java.Lang.Object ToJavaObject<TObject>(this TObject value)
        {
            if (Equals(value, default(TObject)) && !typeof(TObject).IsValueType)
                return null;

            var holder = new JavaHolder(value);

            return holder;
        }

        public class JavaHolder : Java.Lang.Object
        {
            public readonly object Instance;

            public JavaHolder(object instance)
            {
                Instance = instance;
            }
        }
    }
}
