﻿using FolkerKinzel.VCards.Intls.Converters;
using System.Text;

namespace FolkerKinzel.VCards.Intls.Deserializers
{
    internal sealed class VCardDeserializationInfo
    {
        internal const int INITIAL_STRINGBUILDER_CAPACITY = 1024;
        internal const int MAX_STRINGBUILDER_CAPACITY = 4096;

        private DateAndOrTimeConverter? _dateAndOrTimeConverter;
        private TimeConverter? _timeConverter;


        internal StringBuilder Builder { get; } = new StringBuilder(INITIAL_STRINGBUILDER_CAPACITY);


        internal readonly char[] AllQuotes = new char[] { '\"', '\'' };


        internal DateAndOrTimeConverter DateAndOrTimeConverter
        {
            get
            {
                this._dateAndOrTimeConverter ??= new DateAndOrTimeConverter();
                return this._dateAndOrTimeConverter;
            }
        }


        internal TimeConverter TimeConverter
        {
            get
            {
                this._timeConverter ??= new TimeConverter();
                return this._timeConverter;
            }
        }

    }
}
