using System;
using NUnit.Framework;

namespace TestUtilities
{
    public static class AssertExtensions
    {
        public static void ShouldBe<T>(this T actual, T expected)
        {
            Assert.AreEqual(expected, actual);
        }

        public static void ShouldNotBe<T>(this T actual, T expected)
        {
            Assert.AreNotEqual(expected, actual);
        }

        public static void ShouldBeType<T>(this object actual)
        {
            Assert.IsInstanceOf(typeof (T), actual);
        }

        public static void ShouldBeLessThan<T>(this T actual, T expected)
            where T : IComparable
        {
            Assert.Less(expected, actual);
        }
    }
}