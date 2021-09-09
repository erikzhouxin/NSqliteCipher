using SQLitePCL.Raw.Core;
using static SQLitePCL.Raw.Core.RawCore;

namespace System.Data.SQLiteCipher
{
    internal class SqliteParameterReader : SqliteValueReader
    {
        private readonly string _function;
        private readonly sqlite3_value[] _values;

        public SqliteParameterReader(string function, sqlite3_value[] values)
        {
            _function = function;
            _values = values;
        }

        public override int FieldCount
            => _values.Length;

        protected override string GetOnNullErrorMsg(int ordinal)
            => Resources.UDFCalledWithNull(_function, ordinal);

        protected override double GetDoubleCore(int ordinal)
            => sqlite3_value_double(_values[ordinal]);

        protected override long GetInt64Core(int ordinal)
            => sqlite3_value_int64(_values[ordinal]);

        protected override string GetStringCore(int ordinal)
            => sqlite3_value_text(_values[ordinal]).utf8_to_string();

        protected override byte[] GetBlobCore(int ordinal)
            => sqlite3_value_blob(_values[ordinal]).ToArray();

        protected override int GetSqliteType(int ordinal)
            => sqlite3_value_type(_values[ordinal]);
    }
}
