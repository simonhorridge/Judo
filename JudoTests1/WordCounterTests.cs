using Microsoft.VisualStudio.TestTools.UnitTesting;
using Judo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Judo.Tests
{
    [TestClass()]
    public class WordCounterTests
    {

        [TestMethod()]
        public void AddCountsForLineTest()
        {
            var dic = new Dictionary<string, int>();
            var target = new WordCounter();

            var words = "line, no. one\ttwo \n";

            target.AddCountsForLine(dic, words);
            target.AddCountsForLine(dic, "\t'Line'");

            target.AddCountsForLine(dic, "");
            target.AddCountsForLine(dic, "\n");
            Assert.AreEqual(dic.Count, 4);
            Assert.AreEqual(dic["line"], 2);
            Assert.AreEqual(dic["no"], 1);
            Assert.AreEqual(dic["one"], 1);
            Assert.AreEqual(dic["two"], 1);
        }

        [TestMethod()]
        public void IncrementKeyTest()
        {
            var dic = new Dictionary<string, int>();
            var target = new WordCounter();
            PrivateObject obj = new PrivateObject(target);

            obj.Invoke("IncrementKey", dic, "a");
            obj.Invoke("IncrementKey", dic, "A");
            obj.Invoke("IncrementKey", dic, " A ");

            Assert.AreEqual(dic.Keys.Count, 1);
            Assert.AreEqual(dic["a"], 3);

            obj.Invoke("IncrementKey", dic, null);
            obj.Invoke("IncrementKey", dic, null);

            Assert.AreEqual(dic.Keys.Count, 1);
            Assert.AreEqual(dic["a"], 3);

            obj.Invoke("IncrementKey", dic, "ab");
            obj.Invoke("IncrementKey", dic, "\tAB\t");

            Assert.AreEqual(dic.Keys.Count, 2);
            Assert.AreEqual(dic["ab"], 2);
        }

        [TestMethod()]
        public void GetConcatenatedWordsTest()
        {
            var wc = new WordCounter();
            var words = new List<string> { "al", "albums", "aver", "bar", "barely", "be", "befoul", "bums", "by", "cat", "con", "convex", "ely", "foul", "here", "hereby", "jig", "jigsaw", "or", "saw", "tail", "tailor", "vex", "we", "weaver" };
            var expectedWords = new List<string> { "albums", "barely", "befoul", "convex", "hereby", "jigsaw", "tailor", "weaver" };
            var concatenatedWords = wc.GetConcatenatedWords(words, 6);

            CollectionAssert.AreEqual(concatenatedWords, expectedWords); 
        }
    }
}