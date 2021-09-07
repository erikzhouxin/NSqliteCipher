using System.Reflection;
using System.Resources;

namespace System.Data.SQLiteCipher
{
    internal static class Resources
    {
        /// <summary>
        /// {methodName} can only be called when the connection is open.
        /// </summary>
        public static string CallRequiresOpenConnection(object methodName)
            => $"{methodName} can only be called when the connection is open.";

        /// <summary>
        /// CommandText must be set before {methodName} can be called.
        /// </summary>
        public static string CallRequiresSetCommandText(object methodName)
            => $"CommandText must be set before {methodName} can be called.";
        public static string AmbiguousParameterName(string parameterName) => $"Cannot bind the value for parameter '{parameterName}' because multiple matching parameters were found in the public static string command text. Specify the parameter name with the symbol prefix, e.g. '@{parameterName}'.";
        public static string CalledOnNullValue(int ordinal) => $"The data is NULL at ordinal {ordinal}. This method can't be called on NULL values. Check using IsDBNull before public static string calling.";
        public static string CannotStoreNaN { get; private set; } = "Cannot store 'NaN' values.";
        public static string ConnectionStringRequiresClosedConnection { get; private set; } = "ConnectionString cannot be set when the connection is open.";
        public static string ConvertFailed (System.Type sourceType, System.Type targetType) => $"Cannot convert object of type '{sourceType}' to object of type '{targetType}'.";
        public static string DataReaderClosed(string operation) => $"Invalid attempt to call {operation} when reader is closed.";
        public static string DataReaderOpen { get; private set; } = "An open reader is already associated with this command. Close it before opening a new one.";
        public static string DefaultNativeError { get; private set; } = "For more information on this error code see http://sqlite.org/rescode.html";
        public static string EncryptionNotSupported { get; private set; } = "You specified a password in the connection string, but the native SQLite library you're using doesn't support public static string encryption.";
        public static string InvalidCommandType(object commandType) => $"The CommandType '{commandType}' is invalid.";
        public static string InvalidEnumValue(System.Type enumType, object value) => $"The {enumType} enumeration value, {value}, is invalid.";
        public static string InvalidIsolationLevel(object isolationLevel) => $"The IsolationLevel '{isolationLevel}' is invalid.";
        public static string InvalidOffsetAndCount { get; private set; } = "Offset and count were out of bounds for the buffer.";
        public static string InvalidParameterDirection(object direction) => $"The ParameterDirection '{direction}' is invalid.";
        public static string KeywordNotSupported(object keyword) => $"Keyword not supported: '{keyword}'.";
        public static string MissingParameters(object parameters) => $"Must add values for the following parameters: {parameters}";
        public static string NoData { get; private set; } = "No data exists for the row/column.";
        public static string OpenRequiresSetConnectionString { get; private set; } = "ConnectionString must be set before Open can be called.";
        public static string ParallelTransactionsNotSupported { get; private set; } = "SqliteConnection does not support nested transactions.";
        public static string ParameterNotFound(string parameterName) => $"A SqliteParameter with ParameterName '{parameterName}' is not contained by this SqliteParameterCollection.";
        public static string RequiresSet(string propertyName) => $"{propertyName} must be set.";
        public static string ResizeNotSupported { get; private set; } = "The size of a blob may not be changed by the SqliteBlob API. Use an UPDATE command instead.";
        public static string SeekBeforeBegin { get; private set; } = "An attempt was made to move the position before the beginning of the stream.";
        public static string SetRequiresNoOpenReader(string propertyName) => $"An open reader is associated with this command. Close it before changing the {propertyName} property.";
        public static string SqlBlobRequiresOpenConnection { get; private set; } = "SqliteBlob can only be used when the connection is open.";
        public static string SqliteNativeError(int errorCode,string message) => $"SQLite Error {errorCode}: '{message}'.";
        public static string TransactionCompleted { get; private set; } = "This SqliteTransaction has completed; it is no longer usable.";
        public static string TransactionConnectionMismatch { get; private set; } = "The transaction object is not associated with the connection object.";
        public static string TransactionRequired { get; private set; } = "Execute requires the command to have a transaction object when the connection assigned to the command is in a pending public static string local transaction.  The Transaction property of the command has not been initialized.";
        public static string UDFCalledWithNull(string function, int ordinal) => $"The SQL function '{function}' was called with a NULL argument at ordinal {ordinal}. Create the function using a public static string Nullable parameter or rewrite your query to avoid passing NULL.";
        public static string UnknownDataType(System.Type typeName) => $"No mapping exists from object type {typeName} to a known managed provider native type.";
        public static string WriteNotSupported { get; private set; } = "Stream does not support writing.";
    }
}
