using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xave.web.generator.helper.Util
{
    public static class ArrayHandler
    {
        public static T[] Add<T>(this T[] target, T item)
        {
            if (item == null) return target;
            if (target == null) target = new T[] { };

            T[] result = new T[target.Length + 1];
            target.CopyTo(result, 0);
            result[target.Length] = item;
            return result;
        }
    }
}
