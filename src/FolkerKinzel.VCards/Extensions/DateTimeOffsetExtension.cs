﻿using System.ComponentModel;
using FolkerKinzel.VCards.Intls.Converters;

namespace FolkerKinzel.VCards.Extensions;


[Obsolete("This class is obsolete. Use DateAndOrTime instead.", true)]
[EditorBrowsable(EditorBrowsableState.Never)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public static class DateTimeOffsetExtension
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
{

    [Obsolete("This method is obsolete. Use DateAndOrTime instead.", true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [ExcludeFromCodeCoverage]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static bool HasYear(this DateTimeOffset value) => throw new NotImplementedException();
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member


    [Obsolete("This method is obsolete. Use DateAndOrTime instead.", true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [ExcludeFromCodeCoverage]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static bool HasDate(this DateTimeOffset value) => throw new NotImplementedException();
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
