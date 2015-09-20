using System.Collections.Generic;

namespace MapEditor.Extension
{
    public static class ListExtension
    {
        public static IList<T> Swap<T> (this IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
            return list;
        }
    }
}
