using System.Linq.Expressions;

namespace UniStaj.veri
{
    public class Vt
    {

        public static Expression<Func<T, bool>> Birlestir<T>(params Expression<Func<T, bool>>[] kosullar)
        {
            if (kosullar == null || kosullar.Length == 0)
                return x => true;

            Expression<Func<T, bool>> sonuc = kosullar[0];

            foreach (var kosul in kosullar.Skip(1))
            {
                var param = sonuc.Parameters[0];
                var body = Expression.AndAlso(
                    sonuc.Body,
                    Expression.Invoke(kosul, param)
                );
                sonuc = Expression.Lambda<Func<T, bool>>(body, param);
            }

            return sonuc;
        }
        public static Predicate<T> birlestir<T>(params Predicate<T>[] predicates)
        {
            return delegate (T item)
            {
                foreach (Predicate<T> predicate in predicates)
                {
                    if (!predicate(item))
                    {
                        return false;
                    }
                }
                return true;
            };
        }
    }
}
