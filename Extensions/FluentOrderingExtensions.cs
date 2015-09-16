#region
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace bscheiman.Common.Extensions {
    public static class FluentOrderingExtensions {
        public static FluentOrderer<T> OrderFluentlyBy<T, TResult>(this IEnumerable<T> source, Func<T, TResult> predicate)
            where TResult : IComparable<TResult> {
            var result = new FluentOrderer<T>(source);
            result.Comparers.Add((a, b) => predicate(a).CompareTo(predicate(b)));

            return result;
        }

        public static FluentOrderer<T> OrderFluentlyByDescending<T, TResult>(this IEnumerable<T> source, Func<T, TResult> predicate)
            where TResult : IComparable<TResult> {
            var result = new FluentOrderer<T>(source);
            result.Comparers.Add((a, b) => predicate(a).CompareTo(predicate(b)) * -1);

            return result;
        }

        public static FluentOrderer<T> ThenBy<T, TResult>(this FluentOrderer<T> source, Func<T, TResult> predicate)
            where TResult : IComparable<TResult> {
            source.Comparers.Add((a, b) => predicate(a).CompareTo(predicate(b)));

            return source;
        }

        public static FluentOrderer<T> ThenByDescending<T, TResult>(this FluentOrderer<T> source, Func<T, TResult> predicate)
            where TResult : IComparable<TResult> {
            source.Comparers.Add((a, b) => predicate(a).CompareTo(predicate(b)) * -1);

            return source;
        }

        public class FluentOrderer<T> : IEnumerable<T> {
            internal List<Comparison<T>> Comparers = new List<Comparison<T>>();
            internal IEnumerable<T> Source;

            public FluentOrderer(IEnumerable<T> source) {
                Source = source;
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return GetEnumerator();
            }

            public IEnumerator<T> GetEnumerator() {
                var workingArray = Source.ToArray();
                Array.Sort(workingArray, IterativeComparison);

                return ((IEnumerable<T>) workingArray).GetEnumerator();
            }

            private int IterativeComparison(T a, T b) {
                return Comparers.Select(comparer => comparer(a, b)).FirstOrDefault(result => result != 0);
            }
        }
    }
}
