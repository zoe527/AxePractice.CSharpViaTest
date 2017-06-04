using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CSharpViaTest.Collections.Annotations;
using Xunit;

namespace CSharpViaTest.Collections._10_EnumerablePractices
{
    [SuperEasy]
    public class SkippedEnumeratorPractice
    {
        class SkippedEnumerable<T> : IEnumerable<T>
        {
            readonly ICollection<T> collection;

            public SkippedEnumerable(ICollection<T> collection)
            {
                this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
            }

            public IEnumerator<T> GetEnumerator()
            {
                return new SkippedEnumerator<T>(collection);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        #region Please modifies the code to pass the test

        // Attention
        // 
        // * No LINQ method is allowed to use.
        // * The memory efficiency should be O(1)

        class SkippedEnumerator<T> : IEnumerator<T>
        {
            private readonly IEnumerator<T> collection;
            public SkippedEnumerator(IEnumerable<T> collection)
            {
                this.collection = collection.GetEnumerator();
            }

            public bool MoveNext()
            {
                collection.MoveNext();
                return collection.MoveNext();
            }

            public void Reset()
            {
                collection.Reset();
            }

            public T Current => collection.Current;

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                collection.Dispose();
            }
        }

        #endregion

        [Fact]
        public void should_visit_elements_in_skipped_manner()
        {
            int[] sequence = {1, 2, 3, 4, 5, 6};
            int[] resolved = new SkippedEnumerable<int>(sequence).ToArray();

            Assert.Equal(new [] {2, 4, 6}, resolved);
        }
    }
}