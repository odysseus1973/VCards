using FolkerKinzel.VCards.Models.Enums;

namespace FolkerKinzel.VCards.Extensions;

/// <summary>Extension methods that make working with the <see cref="PhoneTypes" />
/// enum easier.</summary>
public static class PhoneTypesExtension
{
    /// <summary>Sets all the flags set in <paramref name="flags" /> in 
    /// <paramref name="value" />.</summary>
    /// <param name="value">The value to which all the flags set in <paramref name="flags"
    /// /> are added.</param>
    /// <param name="flags">A single <see cref="PhoneTypes" /> value or a combination
    /// of several <see cref="PhoneTypes" /> values.</param>
    /// <returns>A value, which has all flags set that are set in <paramref name="value"
    /// /> and <paramref name="flags" />. If <paramref name="value" /> is <c>null</c>,
    /// <paramref name="flags" /> is returned.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNull]
    public static PhoneTypes? Set(this PhoneTypes? value, PhoneTypes flags) 
        => value.HasValue ? (value.Value | flags) : flags;

    /// <summary>Checks whether all flags set in <paramref name="flags" /> are also
    /// set in <paramref name="value" />.</summary>
    /// <param name="value">The value, which is checked to see whether all flags set
    /// in <paramref name="flags" /> are set on it.</param>
    /// <param name="flags">A single <see cref="PhoneTypes" /> value or a combination
    /// of several <see cref="PhoneTypes" /> values.</param>
    /// <returns>Returns <c>true</c>, if all flags set in <paramref name="flags" />
    /// are also set in <paramref name="value" />. If <paramref name="value" /> is 
    /// <c>null</c>, <c>false</c> is returned.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsSet(this PhoneTypes? value, PhoneTypes flags) 
        => (value & flags) == flags;

    /// <summary>Unsets all flags set in <paramref name="flags" /> in <paramref name="value"
    /// />.</summary>
    /// <param name="value">The value from which the flags set in <paramref name="flags"
    /// /> are removed.</param>
    /// <param name="flags">A single <see cref="PhoneTypes" /> value or a combination
    /// of several <see cref="PhoneTypes" /> values.</param>
    /// <returns>A value from which all the flags set in <paramref name="flags" /> are
    /// removed, respectively <c>null</c> if all flags are unset. If <paramref name="value"
    /// /> is <c>null</c>, <c>null</c> is returned.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PhoneTypes? Unset(this PhoneTypes? value, PhoneTypes flags)
    {
        value &= ~flags;
        return value == (PhoneTypes)0 ? null : value;
    }
}
