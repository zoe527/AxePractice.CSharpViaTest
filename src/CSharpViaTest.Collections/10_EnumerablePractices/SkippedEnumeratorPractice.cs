﻿using System;
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
            readonly IEnumerator<T> cursor;

            public SkippedEnumerator(IEnumerable<T> collection)
            {
                cursor = collection.GetEnumerator();
            }

            public bool MoveNext()
            {
                cursor.MoveNext();
                return cursor.MoveNext();
            }

            public void Reset()
            {
                cursor.Reset();
            }

            public T Current => cursor.Current;

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                cursor.Dispose();
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