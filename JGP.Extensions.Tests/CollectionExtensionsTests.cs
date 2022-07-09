using FluentAssertions;
using FluentAssertions.Execution;
using JGP.Extensions.Collections;
using NUnit.Framework;

namespace JGP.Extensions.Tests
{
    public class CollectionExtensionsTests
    {
        #region EnumerableExtensions

        internal class TestClass
        {
            public string Name { get; set; }
        }

        [Test]
        public void SafeAdd_CollectionNull()
        {
            List<string> list = null;
            var value = "test";
            var result = list.SafeAdd(value);
            using (new AssertionScope())
            {
                result.Count().Should().Be(1);
                result.ToList()[0].Should().Be(value);
            }
        }

        [Test]
        public void ToGroups()
        {
            var list = new List<TestClass>()
            {
                new(){Name = "A"},
                new(){Name = "A"},
                new(){Name = "B"}
            };

            var groups = list.ToGroups(nameof(TestClass.Name));

            using (new AssertionScope())
            {
                groups.Count().Should().Be(2);
                groups.First().Count().Should().Be(2);
                groups.Last().Count().Should().Be(1);
            }
        }

        #endregion

        #region DictionaryExtensions

        [Test]
        public void AddIfNotContains_DictionaryNull()
        {
            Dictionary<string, string>? dictionary = null;

            var act = () => dictionary.AddIfNotContains("key", "value");
            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void AddIfNotContains_Contains()
        {
            var key = "key";
            var value = "value";
            var dictionary = new Dictionary<string, string> { { key, value } };

            var result = dictionary.AddIfNotContains(key, value);

            using (new AssertionScope())
            {
                dictionary.Keys.Count.Should().Be(1);
                dictionary.Values.Count.Should().Be(1);
                result.Keys.Count.Should().Be(dictionary.Keys.Count);
                result.Values.Count.Should().Be(dictionary.Values.Count);

                result.Keys.Should().BeEquivalentTo(dictionary.Keys);
                result.Values.Should().BeEquivalentTo(dictionary.Values);
            }
        }

        [Test]
        public void AddIfNotContains_NotContains()
        {
            var key = "key";
            var value = "value";
            var dictionary = new Dictionary<string, string>();

            var result = dictionary.AddIfNotContains(key, value);

            using (new AssertionScope())
            {
                result.Keys.Count.Should().Be(1);
                result.Values.Count.Should().Be(1);
                result.ContainsKey(key).Should().BeTrue();
                result.Values.Contains(value).Should().BeTrue();
            }
        }

        [Test]
        public void AddOrUpdate_DictionaryNull()
        {
            Dictionary<string, string>? dictionary = null;

            var act = () => dictionary.AddOrUpdate("key", "value");
            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void AddOrUpdate_Contains()
        {
            var key = "key";
            var value = "value";
            var value2 = "value2";
            var dictionary = new Dictionary<string, string> { { key, value } };

            var result = dictionary.AddOrUpdate(key, value2);

            using (new AssertionScope())
            {
                dictionary.Keys.Count.Should().Be(1);
                dictionary.Values.Count.Should().Be(1);
                result.Keys.Count.Should().Be(dictionary.Keys.Count);
                result.Values.Count.Should().Be(dictionary.Values.Count);
                result.ContainsKey(key).Should().BeTrue();
                result.Values.Contains(value).Should().BeFalse();
                result.Values.Contains(value2).Should().BeTrue();
            }
        }

        [Test]
        public void AddOrUpdate_NotContains()
        {
            var key = "key";
            var value = "value";
            var dictionary = new Dictionary<string, string>();

            var result = dictionary.AddOrUpdate(key, value);

            using (new AssertionScope())
            {
                result.Keys.Count.Should().Be(1);
                result.Values.Count.Should().Be(1);
                result.ContainsKey(key).Should().BeTrue();
                result.Values.Contains(value).Should().BeTrue();
            }
        }

        [Test]
        public void Find_DictionaryNull()
        {
            Dictionary<string, string>? dictionary = null;
            var act = () => dictionary.Find("key");
            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Find_NotContains()
        {
            var key = "key";
            var dictionary = new Dictionary<string, string>();

            var result = dictionary.Find(key);

            using (new AssertionScope())
            {
                result.Should().BeNull();
            }
        }

        [Test]
        public void Find_Contains()
        {
            var key = "key";
            var value = "value";
            var dictionary = new Dictionary<string, string> { { key, value } };

            var result = dictionary.Find(key);

            using (new AssertionScope())
            {
                result.Should().Be(value);
            }
        }

        [Test]
        public void Find_Func_DictionaryNull()
        {
            Dictionary<string, string>? dictionary = null;
            var act = () => dictionary.Find(x => true);
            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Find_Func_NotContains()
        {
            var dictionary = new Dictionary<string, string>();

            var result = dictionary.Find(x => true);

            using (new AssertionScope())
            {
                result.Should().BeNull();
            }
        }

        [Test]
        public void Find_Func_Contains()
        {
            var key = "key";
            var value = "value";
            var dictionary = new Dictionary<string, string> { { key, value } };

            var result = dictionary.Find(x => x == key);

            using (new AssertionScope())
            {
                result.Should().Be(value);
            }
        }

        [Test]
        public void FindOrAdd_DictionaryNull()
        {
            Dictionary<string, string>? dictionary = null;
            var act = () => dictionary.FindOrAdd("key", () => "value");
            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void FindOrAdd_NotContains()
        {
            var key = "key";
            var value = "value";
            var dictionary = new Dictionary<string, string>();

            var result = dictionary.FindOrAdd(key, () => value);

            using (new AssertionScope())
            {
                result.Should().Be(value);
                dictionary.Keys.Count.Should().Be(1);
                dictionary.Values.Count.Should().Be(1);
                dictionary.ContainsKey(key).Should().BeTrue();
                dictionary.Values.Contains(value).Should().BeTrue();
            }
        }

        [Test]
        public void FindOrAdd_Contains()
        {
            var key = "key";
            var value = "value";
            var value2 = "value2";
            var dictionary = new Dictionary<string, string> { { key, value } };

            var result = dictionary.FindOrAdd(key, () => value2);

            using (new AssertionScope())
            {
                result.Should().Be(value);
                dictionary.Keys.Count.Should().Be(1);
                dictionary.Values.Count.Should().Be(1);
                dictionary.ContainsKey(key).Should().BeTrue();
                dictionary.Values.Contains(value).Should().BeTrue();
            }
        }

        [Test]
        public void FindOrAdd_Func_DictionaryNull()
        {
            Func<string, string> func = str => str;
            Dictionary<string, string>? dictionary = null;
            var act = () => dictionary.FindOrAdd("key", func);
            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void FindOrAdd_Func_NotContains()
        {
            var key = "key";
            var value = "value";
            Func<string, string> func = str => str = value;
            var dictionary = new Dictionary<string, string>();

            var result = dictionary.FindOrAdd(key, func);

            using (new AssertionScope())
            {
                result.Should().Be(value);
                dictionary.Keys.Count.Should().Be(1);
                dictionary.Values.Count.Should().Be(1);
                dictionary.ContainsKey(key).Should().BeTrue();
                dictionary.Values.Contains(value).Should().BeTrue();
            }
        }

        [Test]
        public void FindOrAdd_Func_Contains()
        {
            Func<string, string> func = str => str;
            var key = "key";
            var value = "value";
            var value2 = "value2";
            var dictionary = new Dictionary<string, string> { { key, value } };

            var result = dictionary.FindOrAdd(key, func);

            using (new AssertionScope())
            {
                result.Should().Be(value);
                dictionary.Keys.Count.Should().Be(1);
                dictionary.Values.Count.Should().Be(1);
                dictionary.ContainsKey(key).Should().BeTrue();
                dictionary.Values.Contains(value).Should().BeTrue();
            }
        }

        [Test]
        public void GetBooleanValue_NotBoolean()
        {
            var key = "key";
            var value = "value";
            var dictionary = new Dictionary<string, string> { { key, value } };
            var boolValue = dictionary.GetBooleanValue(key);
            boolValue.Should().BeNull();
        }

        [Test]
        public void GetBooleanValue_Boolean()
        {
            var key = "key";
            var value = "true";
            var dictionary = new Dictionary<string, string> { { key, value } };
            var boolValue = dictionary.GetBooleanValue(key);
            boolValue.Should().BeTrue();
        }

        [Test]
        public void GetDateTimeValue_NotDateTime()
        {
            var key = "key";
            var value = "value";
            var dictionary = new Dictionary<string, string> { { key, value } };
            var dateTimeValue = dictionary.GetDateTimeValue(key);
            dateTimeValue.Should().BeNull();
        }

        [Test]
        public void GetDateTimeValue_DateTime()
        {
            var key = "key";
            var value = "2018-01-01";
            var dictionary = new Dictionary<string, string> { { key, value } };
            var dateTimeValue = dictionary.GetDateTimeValue(key);
            dateTimeValue.Should().Be(new DateTime(2018, 1, 1));
        }

        [Test]
        public void GetIntegerValue_NotInteger()
        {
            var key = "key";
            var value = "value";
            var dictionary = new Dictionary<string, string> { { key, value } };
            var integerValue = dictionary.GetIntegerValue(key);
            integerValue.Should().BeNull();
        }

        [Test]
        public void GetIntegerValue_Integer()
        {
            var key = "key";
            var value = "1";
            var dictionary = new Dictionary<string, string> { { key, value } };
            var integerValue = dictionary.GetIntegerValue(key);
            integerValue.Should().Be(1);
        }

        [Test]
        public void GetStringValue()
        {
            var key = "key";
            var value = "value";
            var dictionary = new Dictionary<string, string> { { key, value } };
            var stringValue = dictionary.GetStringValue(key);
            stringValue.Should().Be(value);
        }

        [Test]
        public void GetStringValue_Default()
        {
            var key = "key";
            var value = "value";
            var dictionary = new Dictionary<string, string>();
            var stringValue = dictionary.GetStringValue(key, value);
            stringValue.Should().Be(value);
        }

        [Test]
        public void GetValue_Integer()
        {
            var key = 1;
            var value = 2;
            var dictionary = new Dictionary<int, int> { { key, value } };
            var integerValue = dictionary.GetValue<int>(key, 0);
            integerValue.Should().Be(value);
        }

        [Test]
        public void GetValue_Integer_Default()
        {
            var key = 1;
            var dictionary = new Dictionary<int, int>();
            var integerValue = dictionary.GetValue<int>(key, 0);
            integerValue.Should().Be(0);
        }
        
        

        #endregion
    }
}
