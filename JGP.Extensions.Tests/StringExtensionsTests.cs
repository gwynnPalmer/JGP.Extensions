// ***********************************************************************
// Assembly         : JGP.Packages.Tests
// Author           : Joshua Gwynn-Palmer
// Created          : 06-26-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 07-04-2022
// ***********************************************************************
// <copyright file="StringExtensionsTests.cs" company="Joshua Gwynn-Palmer">
//     Joshua Gwynn-Palmer
// </copyright>
// <summary></summary>
// ***********************************************************************

using Bogus;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using JGP.Extensions.Strings;

namespace JGP.Extensions.Tests;

/// <summary>
///     Class StringExtensionsTests.
/// </summary>
public class StringExtensionsTests
{
    /// <summary>
    ///     Defines the test method AppendPrefixIfMissing_MissingPrefix.
    /// </summary>
    [Test]
    public void AppendPrefixIfMissing_MissingPrefix()
    {
        var value = "value";
        var prefix = "prefix";

        var appendedValue = value.AppendPrefixIfMissing(prefix);

        appendedValue.Should().Be($"{prefix}{value}");
    }

    /// <summary>
    ///     Defines the test method AppendPrefixIfMissing_PrefixPresent.
    /// </summary>
    [Test]
    public void AppendPrefixIfMissing_PrefixPresent()
    {
        var value = "value";
        var prefix = "Prefix";

        var test = $"prefix{value}";

        var appendedValue = test.AppendPrefixIfMissing(prefix);

        appendedValue.Should().Be(test);
    }

    /// <summary>
    ///     Defines the test method AppendPrefixIfMissing_PrefixPresent_NotIgnoreCase.
    /// </summary>
    [Test]
    public void AppendPrefixIfMissing_PrefixPresent_NotIgnoreCase()
    {
        var value = "value";
        var prefix = "prefix";

        var test = prefix += value;

        var appendedValue = test.AppendPrefixIfMissing(prefix, false);

        appendedValue.Should().Be(test);
    }

    /// <summary>
    ///     Defines the test method AppendPrefixIfMissing_ValueNull.
    /// </summary>
    [Test]
    public void AppendPrefixIfMissing_ValueNull()
    {
        string? value = null;
        var prefix = "prefix";
        var appendedValue = value.AppendPrefixIfMissing(prefix);

        appendedValue.Should().BeNull();
    }

    /// <summary>
    ///     Defines the test method AppendSuffixIfMissing_MissingSuffix.
    /// </summary>
    [Test]
    public void AppendSuffixIfMissing_MissingSuffix()
    {
        var value = "value";
        var suffix = "suffix";

        var appendedValue = value.AppendSuffixIfMissing(suffix);

        appendedValue.Should().Be($"{value}{suffix}");
    }

    /// <summary>
    ///     Defines the test method AppendSuffixIfMissing_SuffixPresent.
    /// </summary>
    [Test]
    public void AppendSuffixIfMissing_SuffixPresent()
    {
        var value = "value";
        var suffix = "Suffix";

        var test = $"{value}suffix";

        var appendedValue = test.AppendSuffixIfMissing(suffix);

        appendedValue.Should().Be(test);
    }

    /// <summary>
    ///     Defines the test method AppendSuffixIfMissing_SuffixPresent_NotIgnoreCase.
    /// </summary>
    [Test]
    public void AppendSuffixIfMissing_SuffixPresent_NotIgnoreCase()
    {
        const string value = "value";
        const string suffix = "suffix";
        const string test = value + suffix;

        var appendedValue = test.AppendSuffixIfMissing(suffix, false);

        appendedValue.Should().Be(test);
    }

    /// <summary>
    ///     Defines the test method AppendSuffixIfMissing_ValueNull.
    /// </summary>
    [Test]
    public void AppendSuffixIfMissing_ValueNull()
    {
        string? value = null;
        var suffix = "suffix";
        var appendedValue = value.AppendSuffixIfMissing(suffix);

        appendedValue.Should().BeNull();
    }

    /// <summary>
    ///     Defines the test method Capitalize.
    /// </summary>
    [Test]
    public void Capitalize()
    {
        var value = "value";
        var cap = value.Capitalize();

        cap.Should().Be("Value");
    }

    /// <summary>
    ///     Defines the test method Capitalize_EmptyString.
    /// </summary>
    [Test]
    public void Capitalize_EmptyString()
    {
        var value = string.Empty;
        var cap = value.Capitalize();

        cap.Should().Be(value);
    }

    /// <summary>
    ///     Defines the test method CountOccurences.
    /// </summary>
    [Test]
    public void CountOccurences()
    {
        var occ = "a";
        var value = "abacad";

        var count = value.CountOccurrences(occ);
        count.Should().Be(3);
    }

    /// <summary>
    ///     Defines the test method DoesNotEndWith_False.
    /// </summary>
    [Test]
    public void DoesNotEndWith_False()
    {
        var value = "valuesuffix";
        var suffix = "suffix";

        var ends = value.DoesNotEndWith(suffix);
        ends.Should().BeFalse();
    }

    /// <summary>
    ///     Defines the test method DoesNotEndWith_SuffixNull.
    /// </summary>
    [Test]
    public void DoesNotEndWith_SuffixNull()
    {
        const string value = "value";
        string? suffix = null;

        var ends = value.DoesNotEndWith(suffix);
        ends.Should().BeTrue();
    }

    /// <summary>
    ///     Defines the test method DoesNotEndWith_True.
    /// </summary>
    [Test]
    public void DoesNotEndWith_True()
    {
        var value = "value";
        var suffix = "suffix";

        var ends = value.DoesNotEndWith(suffix);
        ends.Should().BeTrue();
    }

    /// <summary>
    ///     Defines the test method DoesNotEndWith_ValueNull.
    /// </summary>
    [Test]
    public void DoesNotEndWith_ValueNull()
    {
        string? value = null;
        var suffix = "suffix";

        var ends = value.DoesNotEndWith(suffix);
        ends.Should().BeTrue();
    }

    /// <summary>
    ///     Defines the test method DoesNotStartWith_False.
    /// </summary>
    [Test]
    public void DoesNotStartWith_False()
    {
        var value = "prefixvalue";
        var prefix = "prefix";

        var starts = value.DoesNotStartWith(prefix);
        starts.Should().BeFalse();
    }

    /// <summary>
    ///     Defines the test method DoesNotStartWith_PrefixNull.
    /// </summary>
    [Test]
    public void DoesNotStartWith_PrefixNull()
    {
        var value = "value";
        string? prefix = null;

        var starts = value.DoesNotStartWith(prefix);
        starts.Should().BeTrue();
    }

    /// <summary>
    ///     Defines the test method DoesNotStartWith_True.
    /// </summary>
    [Test]
    public void DoesNotStartWith_True()
    {
        var value = "value";
        var prefix = "prefix";

        var starts = value.DoesNotStartWith(prefix);
        starts.Should().BeTrue();
    }

    /// <summary>
    ///     Defines the test method DoesNotStartWith_ValueNull.
    /// </summary>
    [Test]
    public void DoesNotStartWith_ValueNull()
    {
        string? value = null;
        var prefix = "prefix";

        var starts = value.DoesNotStartWith(prefix);
        starts.Should().BeTrue();
    }

    /// <summary>
    ///     Defines the test method EndsWithIgnoreCase_False.
    /// </summary>
    [Test]
    public void EndsWithIgnoreCase_False()
    {
        var value = "value";
        var suffix = "suffix";

        var ends = value.EndsWithIgnoreCase(suffix);
        ends.Should().BeFalse();
    }

    /// <summary>
    ///     Defines the test method EndsWithIgnoreCase_SuffixNull.
    /// </summary>
    [Test]
    public void EndsWithIgnoreCase_SuffixNull()
    {
        var value = "value";
        string? suffix = null;

        Action act = () => value.EndsWithIgnoreCase(suffix);
        act.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    ///     Defines the test method EndsWithIgnoreCase_True.
    /// </summary>
    [Test]
    public void EndsWithIgnoreCase_True()
    {
        var value = "valuesuffix";
        var suffix = "suffix";

        var ends = value.EndsWithIgnoreCase(suffix);
        ends.Should().BeTrue();
    }

    /// <summary>
    ///     Defines the test method EndsWithIgnoreCase_ValueNull.
    /// </summary>
    [Test]
    public void EndsWithIgnoreCase_ValueNull()
    {
        string? value = null;
        var suffix = "suffix";

        Action act = () => value.EndsWithIgnoreCase(suffix);
        act.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    ///     Defines the test method FirstCharacter.
    /// </summary>
    [Test]
    public void FirstCharacter()
    {
        var value = "value";

        var first = value.FirstCharacter();
        first.Should().Be("v");
    }

    /// <summary>
    ///     Defines the test method FirstCharacter_EmptyString.
    /// </summary>
    [Test]
    public void FirstCharacter_EmptyString()
    {
        var value = string.Empty;

        var first = value.FirstCharacter();
        first.Should().BeNull();
    }

    /// <summary>
    ///     Defines the test method FirstCharacter_ValueNull.
    /// </summary>
    [Test]
    public void FirstCharacter_ValueNull()
    {
        string? value = null;

        var first = value.FirstCharacter();
        first.Should().BeNull();
    }

    /// <summary>
    ///     Defines the test method GenerateRandomString.
    /// </summary>
    [Test]
    public void GenerateRandomString()
    {
        var random = new Random();
        var length = random.Next(1, 100);

        var value = StringExtensions.GenerateRandomString(length);
        value.Length.Should().Be(length);
    }

    /// <summary>
    ///     Defines the test method GenerateRandomString_LengthNegative.
    /// </summary>
    [Test]
    public void GenerateRandomString_LengthNegative()
    {
        var value = string.Empty;
        var length = -1;

        var random = StringExtensions.GenerateRandomString(length);
        random.Should().Be(value);
    }

    /// <summary>
    ///     Defines the test method GenerateRandomString_LengthZero.
    /// </summary>
    [Test]
    public void GenerateRandomString_LengthZero()
    {
        var value = string.Empty;
        var length = 0;

        var random = StringExtensions.GenerateRandomString(length);
        random.Should().Be(value);
    }

    /// <summary>
    ///     Defines the test method GetDefaultIfEmpty_TrimWhiteSpace.
    /// </summary>
    [Test]
    public void GetDefaultIfEmpty_TrimWhiteSpace()
    {
        var value = "value";
        var test = $" {value} ";
        var defaultValue = "default";

        var defaulted = test.GetDefaultIfEmpty(defaultValue);
        defaulted.Should().Be(value);
    }

    /// <summary>
    ///     Defines the test method GetDefaultIfEmpty_ValueEmpty.
    /// </summary>
    [Test]
    public void GetDefaultIfEmpty_ValueEmpty()
    {
        var value = string.Empty;
        var defaultValue = "default";

        var defaulted = value.GetDefaultIfEmpty(defaultValue);
        defaulted.Should().Be(defaultValue);
    }

    /// <summary>
    ///     Defines the test method GetDefaultIfEmpty_ValueNotEmpty.
    /// </summary>
    [Test]
    public void GetDefaultIfEmpty_ValueNotEmpty()
    {
        var value = "value";
        var defaultValue = "default";

        var defaulted = value.GetDefaultIfEmpty(defaultValue);
        defaulted.Should().Be(value);
    }

    /// <summary>
    ///     Defines the test method GetDefaultIfEmpty_ValueNull.
    /// </summary>
    [Test]
    public void GetDefaultIfEmpty_ValueNull()
    {
        string? value = null;
        var defaultValue = "default";

        var defaulted = value.GetDefaultIfEmpty(defaultValue);
        defaulted.Should().Be(defaultValue);
    }

    /// <summary>
    ///     Defines the test method GetEmptyStringIfNull_TrimWhiteSpace.
    /// </summary>
    [Test]
    public void GetEmptyStringIfNull_TrimWhiteSpace()
    {
        var value = "value";
        var test = $" {value} ";

        var empty = test.GetEmptyStringIfNull();
        empty.Should().Be(value);
    }

    /// <summary>
    ///     Defines the test method GetEmptyStringIfNull_ValueNotNull.
    /// </summary>
    [Test]
    public void GetEmptyStringIfNull_ValueNotNull()
    {
        var value = "value";

        var empty = value.GetEmptyStringIfNull();
        empty.Should().Be(value);
    }

    /// <summary>
    ///     Defines the test method GetEmptyStringIfNull_ValueNull.
    /// </summary>
    [Test]
    public void GetEmptyStringIfNull_ValueNull()
    {
        string? value = null;

        var empty = value.GetEmptyStringIfNull();
        empty.Should().Be(string.Empty);
    }

    /// <summary>
    ///     Defines the test method GetLength_ValueEmpty.
    /// </summary>
    [Test]
    public void GetLength_ValueEmpty()
    {
        var value = string.Empty;

        var length = value.GetLength();
        length.Should().Be(0);
    }

    /// <summary>
    ///     Defines the test method GetLength_ValueNotEmpty.
    /// </summary>
    [Test]
    public void GetLength_ValueNotEmpty()
    {
        var random = new Random();
        var stringLength = random.Next(1, 100);
        var value = StringExtensions.GenerateRandomString(stringLength);

        var length = value.GetLength();
        length.Should().Be(stringLength);
    }

    /// <summary>
    ///     Defines the test method GetLength_ValueNull.
    /// </summary>
    [Test]
    public void GetLength_ValueNull()
    {
        string? value = null;

        var length = value.GetLength();
        length.Should().BeNull();
    }

    /// <summary>
    ///     Defines the test method GetNullIfEmptyString_TrimWhiteSpace.
    /// </summary>
    [Test]
    public void GetNullIfEmptyString_TrimWhiteSpace()
    {
        var value = "value";
        var test = $" {value} ";

        var nulled = test.GetNullIfEmptyString();
        nulled.Should().Be(value);
    }

    /// <summary>
    ///     Defines the test method GetNullIfEmptyString_ValueEmpty.
    /// </summary>
    [Test]
    public void GetNullIfEmptyString_ValueEmpty()
    {
        var value = string.Empty;

        var nulled = value.GetNullIfEmptyString();
        nulled.Should().BeNull();
    }

    /// <summary>
    ///     Defines the test method GetNullIfEmptyString_ValueNotEmpty.
    /// </summary>
    [Test]
    public void GetNullIfEmptyString_ValueNotEmpty()
    {
        var value = "value";

        var nulled = value.GetNullIfEmptyString();
        nulled.Should().Be(value);
    }

    /// <summary>
    ///     Defines the test method GetNullIfEmptyString_ValueNull.
    /// </summary>
    [Test]
    public void GetNullIfEmptyString_ValueNull()
    {
        string? value = null;

        var nulled = value.GetNullIfEmptyString();
        nulled.Should().BeNull();
    }

    /// <summary>
    ///     Defines the test method IsAlpha_ValueAlpha.
    /// </summary>
    [Test]
    public void IsAlpha_ValueAlpha()
    {
        var value = "value";

        var isAlpha = value.IsAlpha();
        isAlpha.Should().BeTrue();
    }

    /// <summary>
    ///     Defines the test method IsAlpha_ValueEmpty.
    /// </summary>
    [Test]
    public void IsAlpha_ValueEmpty()
    {
        var value = string.Empty;

        var isAlpha = value.IsAlpha();
        isAlpha.Should().BeFalse();
    }

    /// <summary>
    ///     Defines the test method IsAlpha_ValueNotAlpha.
    /// </summary>
    [Test]
    public void IsAlpha_ValueNotAlpha()
    {
        var value = "value123";

        var isAlpha = value.IsAlpha();
        isAlpha.Should().BeFalse();
    }

    /// <summary>
    ///     Defines the test method IsAlpha_ValueNull.
    /// </summary>
    [Test]
    public void IsAlpha_ValueNull()
    {
        string? value = null;

        var isAlpha = value.IsAlpha();
        isAlpha.Should().BeFalse();
    }

    /// <summary>
    ///     Defines the test method IsAlphaNumeric_ValueAlphaNumeric.
    /// </summary>
    [Test]
    public void IsAlphaNumeric_ValueAlphaNumeric()
    {
        var random = new Random();
        var stringLength = random.Next(1, 100);
        var value = StringExtensions.GenerateRandomString(stringLength);

        var isAlphaNumeric = value.IsAlphaNumeric();
        isAlphaNumeric.Should().BeTrue();
    }

    /// <summary>
    ///     Defines the test method IsAlphaNumeric_ValueEmpty.
    /// </summary>
    [Test]
    public void IsAlphaNumeric_ValueEmpty()
    {
        var value = string.Empty;

        var isAlphaNumeric = value.IsAlphaNumeric();
        isAlphaNumeric.Should().BeFalse();
    }

    /// <summary>
    ///     Defines the test method IsAlphaNumeric_ValueNotAlphaNumeric.
    /// </summary>
    [Test]
    public void IsAlphaNumeric_ValueNotAlphaNumeric()
    {
        var value = "value123!?";

        var isAlphaNumeric = value.IsAlphaNumeric();
        isAlphaNumeric.Should().BeFalse();
    }

    /// <summary>
    ///     Defines the test method IsAlphaNumeric_ValueNull.
    /// </summary>
    [Test]
    public void IsAlphaNumeric_ValueNull()
    {
        string? value = null;

        var isAlphaNumeric = value.IsAlphaNumeric();
        isAlphaNumeric.Should().BeFalse();
    }

    /// <summary>
    ///     Defines the test method IsDate_ValueDate.
    /// </summary>
    [Test]
    public void IsDate_ValueDate()
    {
        var value = "01/01/2000";

        var isDate = value.IsDate();
        isDate.Should().BeTrue();
    }

    /// <summary>
    ///     Defines the test method IsDate_ValueEmpty.
    /// </summary>
    [Test]
    public void IsDate_ValueEmpty()
    {
        var value = string.Empty;

        var isDate = value.IsDate();
        isDate.Should().BeFalse();
    }

    /// <summary>
    ///     Defines the test method IsDate_ValueNotDate.
    /// </summary>
    [Test]
    public void IsDate_ValueNotDate()
    {
        var value = "0101200";

        var isDate = value.IsDate();
        isDate.Should().BeFalse();
    }

    /// <summary>
    ///     Defines the test method IsDate_ValueNull.
    /// </summary>
    [Test]
    public void IsDate_ValueNull()
    {
        string? value = null;

        var isDate = value.IsDate();
        isDate.Should().BeFalse();
    }

    /// <summary>
    ///     Defines the test method IsDateTime_DateTime.
    /// </summary>
    [Test]
    public void IsDateTime_DateTime()
    {
        var value = DateTime.UtcNow.ToString("ddMMyyyy hh:MM:ssz");
        var isDateTime = value.IsDateTime("ddMMyyyy hh:MM:ssz");
        isDateTime.Should().BeTrue();
    }

    /// <summary>
    ///     Defines the test method IsDateTime_NotDateTime.
    /// </summary>
    [Test]
    public void IsDateTime_NotDateTime()
    {
        var value = "value";
        var isDateTime = value.IsDateTime("ddMMyyyy hh:MM:ssz");
        isDateTime.Should().BeFalse();
    }

    /// <summary>
    ///     Defines the test method IsEmailAddress_IsEmail.
    /// </summary>
    [Test]
    public void IsEmailAddress_IsEmail()
    {
        var value = new Faker().Person.Email;
        var isEmail = value.IsEmailAddress();
        isEmail.Should().BeTrue();
    }

    /// <summary>
    ///     Defines the test method IsEmailAddress_NotEmail.
    /// </summary>
    [Test]
    public void IsEmailAddress_NotEmail()
    {
        var random = new Random();
        var stringLength = random.Next(1, 100);
        var value = StringExtensions.GenerateRandomString(stringLength);

        var isEmail = value.IsEmailAddress();
        isEmail.Should().BeFalse();
    }

    /// <summary>
    ///     Defines the test method IsInt_ValueEmpty.
    /// </summary>
    [Test]
    public void IsInt_ValueEmpty()
    {
        var value = string.Empty;

        var isInt = value.IsInt();
        isInt.Should().BeFalse();
    }

    /// <summary>
    ///     Defines the test method IsInt_ValueInt.
    /// </summary>
    [Test]
    public void IsInt_ValueInt()
    {
        var value = "123";

        var isInt = value.IsInt();
        isInt.Should().BeTrue();
    }

    /// <summary>
    ///     Defines the test method IsInt_ValueNotInt.
    /// </summary>
    [Test]
    public void IsInt_ValueNotInt()
    {
        var value = "value";

        var isInt = value.IsInt();
        isInt.Should().BeFalse();
    }

    /// <summary>
    ///     Defines the test method IsInt_ValueNull.
    /// </summary>
    [Test]
    public void IsInt_ValueNull()
    {
        string? value = null;

        var isInt = value.IsInt();
        isInt.Should().BeFalse();
    }

    /// <summary>
    ///     Defines the test method IsLength_False.
    /// </summary>
    [Test]
    public void IsLength_False()
    {
        var random = new Random();
        var stringLength = random.Next(1, 100);
        var value = StringExtensions.GenerateRandomString(stringLength);

        var isLength = value.IsLength(stringLength + 1, stringLength + 2);
        isLength.Should().BeFalse();
    }

    /// <summary>
    ///     Defines the test method IsLength_True.
    /// </summary>
    [Test]
    public void IsLength_True()
    {
        var random = new Random();
        var stringLength = random.Next(1, 100);
        var value = StringExtensions.GenerateRandomString(stringLength);

        var isLength = value.IsLength(stringLength - 1, stringLength + 1);
        isLength.Should().BeTrue();
    }

    /// <summary>
    ///     Defines the test method IsLength_ValueEmpty.
    /// </summary>
    [Test]
    public void IsLength_ValueEmpty()
    {
        var value = string.Empty;

        var isLength = value.IsLength(1, 2);
        isLength.Should().BeFalse();
    }

    /// <summary>
    ///     Defines the test method IsLength_ValueNull.
    /// </summary>
    [Test]
    public void IsLength_ValueNull()
    {
        string? value = null;

        var isLength = value.IsLength(1, 2);
        isLength.Should().BeFalse();
    }

    /// <summary>
    ///     Defines the test method IsMaxLength_False.
    /// </summary>
    [Test]
    public void IsMaxLength_False()
    {
        var random = new Random();
        var stringLength = random.Next(1, 100);
        var value = StringExtensions.GenerateRandomString(stringLength);

        var isMaxLength = value.IsMaxLength(stringLength - 1);
        isMaxLength.Should().BeFalse();
    }

    /// <summary>
    ///     Defines the test method IsMaxLength_True.
    /// </summary>
    [Test]
    public void IsMaxLength_True()
    {
        var random = new Random();
        var stringLength = random.Next(1, 100);
        var value = StringExtensions.GenerateRandomString(stringLength);

        var isMaxLength = value.IsMaxLength(stringLength);
        isMaxLength.Should().BeTrue();
    }

    /// <summary>
    ///     Defines the test method IsMaxLength_ValueEmpty.
    /// </summary>
    [Test]
    public void IsMaxLength_ValueEmpty()
    {
        var value = string.Empty;

        var isMaxLength = value.IsMaxLength(1);
        isMaxLength.Should().BeTrue();
    }

    /// <summary>
    ///     Defines the test method IsMaxLength_ValueNull.
    /// </summary>
    [Test]
    public void IsMaxLength_ValueNull()
    {
        string? value = null;

        var isMaxLength = value.IsMaxLength(1);
        isMaxLength.Should().BeFalse();
    }

    /// <summary>
    ///     Defines the test method IsMinLength_False.
    /// </summary>
    [Test]
    public void IsMinLength_False()
    {
        var random = new Random();
        var stringLength = random.Next(1, 100);
        var value = StringExtensions.GenerateRandomString(stringLength);

        var isMinLength = value.IsMinLength(stringLength + 1);
        isMinLength.Should().BeFalse();
    }

    /// <summary>
    ///     Defines the test method IsMinLength_True.
    /// </summary>
    [Test]
    public void IsMinLength_True()
    {
        var random = new Random();
        var stringLength = random.Next(1, 100);
        var value = StringExtensions.GenerateRandomString(stringLength);

        var isMinLength = value.IsMinLength(stringLength);
        isMinLength.Should().BeTrue();
    }

    /// <summary>
    ///     Defines the test method IsMinLength_ValueEmpty.
    /// </summary>
    [Test]
    public void IsMinLength_ValueEmpty()
    {
        var value = string.Empty;

        var isMinLength = value.IsMinLength(1);
        isMinLength.Should().BeFalse();
    }

    /// <summary>
    ///     Defines the test method IsMinLength_ValueNull.
    /// </summary>
    [Test]
    public void IsMinLength_ValueNull()
    {
        string? value = null;

        var isMinLength = value.IsMinLength(1);
        isMinLength.Should().BeFalse();
    }

    /// <summary>
    ///     Defines the test method IsNull_ValueNotNull.
    /// </summary>
    [Test]
    public void IsNull_ValueNotNull()
    {
        var value = "value";

        var isNull = value.IsNull();
        isNull.Should().BeFalse();
    }

    /// <summary>
    ///     Defines the test method IsNull_ValueNull.
    /// </summary>
    [Test]
    public void IsNull_ValueNull()
    {
        string? value = null;

        var isNull = value.IsNull();
        isNull.Should().BeTrue();
    }

    /// <summary>
    ///     Defines the test method IsNullOrEmpty_ValueEmpty.
    /// </summary>
    [Test]
    public void IsNullOrEmpty_ValueEmpty()
    {
        var value = string.Empty;

        var isNullOrEmpty = value.IsNullOrEmpty();
        isNullOrEmpty.Should().BeTrue();
    }

    /// <summary>
    ///     Defines the test method IsNullOrEmpty_ValueNotNull.
    /// </summary>
    [Test]
    public void IsNullOrEmpty_ValueNotNull()
    {
        var value = "value";

        var isNullOrEmpty = value.IsNullOrEmpty();
        isNullOrEmpty.Should().BeFalse();
    }

    /// <summary>
    ///     Defines the test method IsNullOrEmpty_ValueNull.
    /// </summary>
    [Test]
    public void IsNullOrEmpty_ValueNull()
    {
        string? value = null;

        var isNullOrEmpty = value.IsNullOrEmpty();
        isNullOrEmpty.Should().BeTrue();
    }

    /// <summary>
    ///     Defines the test method IsNullOrWhiteSpace_ValueEmpty.
    /// </summary>
    [Test]
    public void IsNullOrWhiteSpace_ValueEmpty()
    {
        var value = string.Empty;

        var isNullOrWhiteSpace = value.IsNullOrWhiteSpace();
        isNullOrWhiteSpace.Should().BeTrue();
    }

    /// <summary>
    ///     Defines the test method IsNullOrWhiteSpace_ValueNotWhiteSpace.
    /// </summary>
    [Test]
    public void IsNullOrWhiteSpace_ValueNotWhiteSpace()
    {
        var value = "value";

        var isNullOrWhiteSpace = value.IsNullOrWhiteSpace();
        isNullOrWhiteSpace.Should().BeFalse();
    }

    /// <summary>
    ///     Defines the test method IsNullOrWhiteSpace_ValueNull.
    /// </summary>
    [Test]
    public void IsNullOrWhiteSpace_ValueNull()
    {
        string? value = null;

        var isNullOrWhiteSpace = value.IsNullOrWhiteSpace();
        isNullOrWhiteSpace.Should().BeTrue();
    }

    /// <summary>
    ///     Defines the test method IsNullOrWhiteSpace_ValueWhiteSpace.
    /// </summary>
    [Test]
    public void IsNullOrWhiteSpace_ValueWhiteSpace()
    {
        var value = " ";

        var isNullOrWhiteSpace = value.IsNullOrWhiteSpace();
        isNullOrWhiteSpace.Should().BeTrue();
    }

    /// <summary>
    ///     Defines the test method IsNumeric_False.
    /// </summary>
    [Test]
    public void IsNumeric_False()
    {
        var value = "abc";

        var isNumeric = value.IsNumeric();
        isNumeric.Should().BeFalse();
    }

    /// <summary>
    ///     Defines the test method IsNumeric_True.
    /// </summary>
    [Test]
    public void IsNumeric_True()
    {
        var value = "123";

        var isNumeric = value.IsNumeric();
        isNumeric.Should().BeTrue();
    }

    /// <summary>
    ///     Defines the test method IsNumeric_ValueEmpty.
    /// </summary>
    [Test]
    public void IsNumeric_ValueEmpty()
    {
        var value = string.Empty;

        var isNumeric = value.IsNumeric();
        isNumeric.Should().BeFalse();
    }

    /// <summary>
    ///     Defines the test method IsNumeric_ValueNull.
    /// </summary>
    [Test]
    public void IsNumeric_ValueNull()
    {
        string? value = null;

        var isNumeric = value.IsNumeric();
        isNumeric.Should().BeFalse();
    }

    /// <summary>
    ///     Defines the test method LastCharacter_True.
    /// </summary>
    [Test]
    public void LastCharacter_True()
    {
        var random = new Random();
        var stringLength = random.Next(1, 100);
        var value = StringExtensions.GenerateRandomString(stringLength);

        var lastCharacter = value.LastCharacter();
        lastCharacter.Should().Be(value[stringLength - 1].ToString());
    }

    /// <summary>
    ///     Defines the test method LastCharacter_ValueEmpty.
    /// </summary>
    [Test]
    public void LastCharacter_ValueEmpty()
    {
        var value = string.Empty;

        var lastCharacter = value.LastCharacter();
        lastCharacter.Should().BeNull();
    }

    /// <summary>
    ///     Defines the test method LastCharacter_ValueNull.
    /// </summary>
    [Test]
    public void LastCharacter_ValueNull()
    {
        string? value = null;

        var lastCharacter = value.LastCharacter();
        lastCharacter.Should().BeNull();
    }

    /// <summary>
    ///     Defines the test method Left_LengthLessThanZero.
    /// </summary>
    [Test]
    public void Left_LengthLessThanZero()
    {
        var value = "value";
        Action action = () => value.Left(-1);
        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    /// <summary>
    ///     Defines the test method Left_LengthMoreThanValueLength.
    /// </summary>
    [Test]
    public void Left_LengthMoreThanValueLength()
    {
        var value = "value";
        Action action = () => value.Left(value.Length + 1);
        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    /// <summary>
    ///     Defines the test method Left_ValidLength.
    /// </summary>
    [Test]
    public void Left_ValidLength()
    {
        var random = new Random();
        var stringLength = random.Next(1, 100);
        var value = StringExtensions.GenerateRandomString(stringLength);

        var left = value.Left(1);
        left.Should().Be(value.Substring(0, 1));
    }

    /// <summary>
    ///     Defines the test method Left_ValueEmpty.
    /// </summary>
    [Test]
    public void Left_ValueEmpty()
    {
        var value = string.Empty;
        Action action = () => value.Left(1);
        action.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    ///     Defines the test method Left_ValueNull.
    /// </summary>
    [Test]
    public void Left_ValueNull()
    {
        string? value = null;
        Action action = () => value.Left(1);
        action.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    ///     Defines the test method RemoveChars_CharsEmpty.
    /// </summary>
    [Test]
    public void RemoveChars_CharsEmpty()
    {
        var value = "value";
        var chars = Array.Empty<char>();
        var result = value.RemoveCharacters(chars);
        result.Should().Be(value);
    }

    /// <summary>
    ///     Defines the test method RemoveChars_Valid.
    /// </summary>
    [Test]
    public void RemoveChars_Valid()
    {
        var random = new Random();
        var stringLength = random.Next(1, 100);
        var value = StringExtensions.GenerateRandomString(stringLength);
        var chars = new[] { 'a', 'b', 'c' };
        var result = value.RemoveCharacters(chars);
        result.Should().Be(value.Replace("a", "").Replace("b", "").Replace("c", ""));
    }

    /// <summary>
    ///     Defines the test method RemoveChars_ValueEmpty.
    /// </summary>
    [Test]
    public void RemoveChars_ValueEmpty()
    {
        var value = string.Empty;
        var test = value.RemoveCharacters('a');
        test.Should().Be(value);
    }

    /// <summary>
    ///     Defines the test method RemoveChars_ValueNull.
    /// </summary>
    [Test]
    public void RemoveChars_ValueNull()
    {
        string? value = null;
        var test = value.RemoveCharacters('a');
        test.Should().BeNull();
    }

    /// <summary>
    ///     Defines the test method RemoveDiacritics_Valid.
    /// </summary>
    [Test]
    public void RemoveDiacritics_Valid()
    {
        var input = "Příliš žluťoučký kůň úpěl ďábelské ódy.";
        var expected = "Prilis zlutoucky kun upel dabelske ody.";

        var result = input.RemoveDiacritics();
        result.Should().Be(expected);
    }

    /// <summary>
    ///     Defines the test method RemovePrefix_PrefixMissing_IgnoreCaseFalse.
    /// </summary>
    [Test]
    public void RemovePrefix_PrefixMissing_IgnoreCaseFalse()
    {
        var value = "value";
        var test = value.RemovePrefix("prefix", false);
        test.Should().Be(value);
    }

    /// <summary>
    ///     Defines the test method RemovePrefix_PrefixMissing_IgnoreCaseTrue.
    /// </summary>
    [Test]
    public void RemovePrefix_PrefixMissing_IgnoreCaseTrue()
    {
        var value = "value";
        var test = value.RemovePrefix("prefix");
        test.Should().Be(value);
    }

    /// <summary>
    ///     Defines the test method RemovePrefix_PrefixPresent_IgnoreCaseFalse.
    /// </summary>
    [Test]
    public void RemovePrefix_PrefixPresent_IgnoreCaseFalse()
    {
        var value = "prefixvalue";
        var test = value.RemovePrefix("Prefix", false);
        test.Should().Be(value);
    }

    /// <summary>
    ///     Defines the test method RemovePrefix_PrefixPresent_IgnoreCaseTrue.
    /// </summary>
    [Test]
    public void RemovePrefix_PrefixPresent_IgnoreCaseTrue()
    {
        var value = "prefixvalue";
        var test = value.RemovePrefix("Prefix");
        test.Should().Be("value");
    }

    /// <summary>
    ///     Defines the test method RemovePrefix_ValueEmpty.
    /// </summary>
    [Test]
    public void RemovePrefix_ValueEmpty()
    {
        var value = string.Empty;
        var test = value.RemovePrefix("prefix");
        test.Should().Be(value);
    }

    /// <summary>
    ///     Defines the test method RemovePrefix_ValueNull.
    /// </summary>
    [Test]
    public void RemovePrefix_ValueNull()
    {
        string? value = null;
        var test = value.RemovePrefix("prefix");
        test.Should().BeNull();
    }

    /// <summary>
    ///     Defines the test method RemoveSuffix_SuffixMissing_IgnoreCaseFalse.
    /// </summary>
    [Test]
    public void RemoveSuffix_SuffixMissing_IgnoreCaseFalse()
    {
        var value = "value";
        var test = value.RemoveSuffix("suffix", false);
        test.Should().Be(value);
    }

    /// <summary>
    ///     Defines the test method RemoveSuffix_SuffixMissing_IgnoreCaseTrue.
    /// </summary>
    [Test]
    public void RemoveSuffix_SuffixMissing_IgnoreCaseTrue()
    {
        var value = "value";
        var test = value.RemoveSuffix("suffix");
        test.Should().Be(value);
    }

    /// <summary>
    ///     Defines the test method RemoveSuffix_SuffixPresent_IgnoreCaseFalse.
    /// </summary>
    [Test]
    public void RemoveSuffix_SuffixPresent_IgnoreCaseFalse()
    {
        var value = "valuesuffix";
        var test = value.RemoveSuffix("Suffix", false);
        test.Should().Be(value);
    }

    /// <summary>
    ///     Defines the test method RemoveSuffix_SuffixPresent_IgnoreCaseTrue.
    /// </summary>
    [Test]
    public void RemoveSuffix_SuffixPresent_IgnoreCaseTrue()
    {
        var value = "valuesuffix";
        var test = value.RemoveSuffix("Suffix");
        test.Should().Be("value");
    }

    /// <summary>
    ///     Defines the test method RemoveSuffix_ValueEmpty.
    /// </summary>
    [Test]
    public void RemoveSuffix_ValueEmpty()
    {
        var value = string.Empty;
        var test = value.RemoveSuffix("suffix");
        test.Should().Be(value);
    }

    /// <summary>
    ///     Defines the test method RemoveSuffix_ValueNull.
    /// </summary>
    [Test]
    public void RemoveSuffix_ValueNull()
    {
        string? value = null;
        var test = value.RemoveSuffix("suffix");
        test.Should().BeNull();
    }

    /// <summary>
    ///     Defines the test method Replace.
    /// </summary>
    [Test]
    public void Replace()
    {
        var value = "friends";
        var test = value.Replace('f', 'r', 'i');
        test.Should().Be("ends");
    }

    /// <summary>
    ///     Defines the test method Reverse.
    /// </summary>
    [Test]
    public void Reverse()
    {
        var value = "value";
        var test = value.Reverse();
        test.Should().Be("eulav");
    }

    /// <summary>
    ///     Defines the test method Right_LengthLessThanZero.
    /// </summary>
    [Test]
    public void Right_LengthLessThanZero()
    {
        var value = "value";
        Action action = () => value.Right(-1);
        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    /// <summary>
    ///     Defines the test method Right_LengthMoreThanValueLength.
    /// </summary>
    [Test]
    public void Right_LengthMoreThanValueLength()
    {
        var value = "value";
        Action action = () => value.Right(value.Length + 1);
        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    /// <summary>
    ///     Defines the test method Right_ValidLength.
    /// </summary>
    [Test]
    public void Right_ValidLength()
    {
        var random = new Random();
        var stringLength = random.Next(1, 100);
        var value = StringExtensions.GenerateRandomString(stringLength);

        var Right = value.Right(1);
        Right.Should().Be(value.Substring(stringLength - 1));
    }

    /// <summary>
    ///     Defines the test method Right_ValueEmpty.
    /// </summary>
    [Test]
    public void Right_ValueEmpty()
    {
        var value = string.Empty;
        Action action = () => value.Right(1);
        action.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    ///     Defines the test method Right_ValueNull.
    /// </summary>
    [Test]
    public void Right_ValueNull()
    {
        string? value = null;
        Action action = () => value.Right(1);
        action.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    ///     Defines the test method StartsWithIgnoreCase_PrefixNull.
    /// </summary>
    [Test]
    public void StartsWithIgnoreCase_PrefixNull()
    {
        var value = "value";
        string? prefix = null;
        var action = () => value.StartsWithIgnoreCase(prefix);
        action.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    ///     Defines the test method StartsWithIgnoreCase_ValueNull.
    /// </summary>
    [Test]
    public void StartsWithIgnoreCase_ValueNull()
    {
        string? value = null;
        var action = () => value.StartsWithIgnoreCase("prefix");
        action.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    ///     Defines the test method ToSpacedWords.
    /// </summary>
    [Test]
    public void ToSpacedWords()
    {
        var value = "OutOfVindaloo";
        var expected = "Out Of Vindaloo";

        var test = value.ToSpacedWords();
        test.Should().Be(expected);
    }

    /// <summary>
    ///     Defines the test method Truncate_MaxLengthLessThanZero.
    /// </summary>
    [Test]
    public void Truncate_MaxLengthLessThanZero()
    {
        var value = "value";
        var test = value.Truncate(-1);
        test.Should().BeEmpty();
    }

    /// <summary>
    ///     Defines the test method Truncate_ValueEmpty.
    /// </summary>
    [Test]
    public void Truncate_ValueEmpty()
    {
        var value = string.Empty;
        var test = value.Truncate(10);
        test.Should().BeEmpty();
    }

    /// <summary>
    ///     Defines the test method Truncate_ValueLengthGreaterThanMaxLength.
    /// </summary>
    [Test]
    public void Truncate_ValueLengthGreaterThanMaxLength()
    {
        var value = "value";
        var test = value.Truncate(value.Length - 1);
        test.Should().Be(value.Substring(0, value.Length - 1));
    }

    /// <summary>
    ///     Defines the test method Truncate_ValueLengthLessThanMaxLength.
    /// </summary>
    [Test]
    public void Truncate_ValueLengthLessThanMaxLength()
    {
        var value = "value";
        var test = value.Truncate(value.Length + 1);
        test.Should().Be(value);
    }

    /// <summary>
    ///     Defines the test method Truncate_ValueNull.
    /// </summary>
    [Test]
    public void Truncate_ValueNull()
    {
        string? value = null;
        var test = value.Truncate(10);
        test.Should().BeEmpty();
    }
    /// <summary>
    ///     Defines the test method TryGetDateTime_InvalidDateTime.
    /// </summary>
    [Test]
    public void TryGetDateTime_InvalidDateTime()
    {
        var value = "abc";
        var format = "ddMMyyyy hhMMssz";
        var test = value.TryGetDateTime(format, out var dateTime);

        using (new AssertionScope())
        {
            test.Should().BeFalse();
            dateTime.Should().BeNull();
        }
    }

    /// <summary>
    ///     Defines the test method TryGetDateTime_ValidDateTime.
    /// </summary>
    [Test]
    public void TryGetDateTime_ValidDateTime()
    {
        var value = "2018-01-01";
        var format = "ddMMyyyy hhMMssz";
        var test = value.TryGetDateTime(format, out var dateTime);

        using (new AssertionScope())
        {
            test.Should().BeTrue();
            dateTime.Should().Be(new DateTime(2018, 1, 1, 0, 0, 0).ToString(format));
        }
    }
}