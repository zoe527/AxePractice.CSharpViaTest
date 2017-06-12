﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CSharpViaTest.Collections.Helpers;
using Xunit;

namespace CSharpViaTest.Collections._30_MapReducePractices
{
    /* 
     * Description
     * ===========
     * 
     * This test will try count number of words in multiple files. The files are provided
     * in a stream manner. Please note that you have to be aware that the file may be
     * very large.
     * 
     * The words in the file streams will be divided by a white space character. You can 
     * create additional method in the practice region to improve readablility. 
     * 
     * Difficulty: Medium
     * 
     * Knowledge Point
     * ===============
     * 
     * - LINQ Query: Sum
     * - Using sequencial access to evaluate word count.
     * 
     * Requirement
     * ===========
     * 
     * - No `for`, `foreach` or other loop keywords are allowed to use.
     */
    public class CountNumberOfWordsInMultipleTextFiles
    {
        #region Please modifies the code to pass the test
        
        static int CountNumberOfWords(IEnumerable<Stream> streams)
        {
            var sum = 0;
            foreach(var stream in streams){
                var stringReader = new StreamReader(stream);
                var str = stringReader.ReadToEnd().Split(' ');
                sum += str.Length - 1;
            }

            return sum;
        }

        #endregion

        [Fact]
        public void should_count_number_of_words()
        {
            const int fileCount = 5;
            const int wordsInEachFile = 10;

            Stream[] streams = Enumerable
                .Repeat(0, fileCount)
                .Select(_ => TextStreamFactory.Create(wordsInEachFile))
                .ToArray();

            int count = CountNumberOfWords(streams);

            Assert.Equal(fileCount * wordsInEachFile, count);

            foreach (Stream stream in streams) { stream.Dispose(); }
        }
    }
}