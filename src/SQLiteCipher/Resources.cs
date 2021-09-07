using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;

namespace System.Data.SQLiteCipher
{
    internal static class Resources
    {
        public static String AmbiguousParameterName(object parameterName) => $"Cannot bind the value for parameter '{parameterName}' because multiple matching parameters were found in the command text. Specify the parameter name with the symbol prefix, e.g. '@{parameterName}'.";
        public static String CalledOnNullValue(object ordinal) => $"The data is NULL at ordinal {ordinal}. This method can't be called on NULL values. Check using IsDBNull before calling.";
        public static String CallRequiresOpenConnection(object methodName) => $"{methodName} can only be called when the connection is open.";
        public static String CallRequiresSetCommandText(object methodName) => $"CommandText must be set before {methodName} can be called.";
        public static String CannotStoreNaN => $"Cannot store 'NaN' values.";
        public static String ConnectionStringRequiresClosedConnection => $"ConnectionString cannot be set when the connection is open.";
        public static String ConvertFailed(object sourceType, object targetType) => $"Cannot convert object of type '{sourceType}' to object of type '{targetType}'.";
        public static String DataReaderClosed(object operation) => $"Invalid attempt to call {operation} when reader is closed.";
        public static String DataReaderOpen => $"An open reader is already associated with this command. Close it before opening a new one.";
        public static String DefaultNativeError => $"For more information on this error code see https://www.sqlite.org/rescode.html";
        public static String EncryptionNotSupported(object libraryName) => $"You specified a password in the connection string, but the native SQLite library '{libraryName}' doesn't support encryption.";
        public static String InvalidCommandType(object commandType) => $"The CommandType '{commandType}' is not supported.";
        public static String InvalidEnumValue(object enumType, object value) => $"The {enumType} enumeration value, {value}, is invalid.";
        public static String InvalidIsolationLevel(object isolationLevel) => $"The IsolationLevel '{isolationLevel}' is not supported.";
        public static String InvalidOffsetAndCount => $"Offset and count were out of bounds for the buffer.";
        public static String InvalidParameterDirection(object direction) => $"The ParameterDirection '{direction}' is not supported.";
        public static String KeywordNotSupported(object keyword) => $"Connection string keyword '{keyword}' is not supported. For a possible alternative, see https://go.microsoft.com/fwlink/?linkid=2142181.";
        public static String MissingParameters(object parameters) => $"Must add values for the following parameters: {parameters}";
        public static String NoData => $"No data exists for the row/column.";
        public static String OpenRequiresSetConnectionString => $"ConnectionString must be set before Open can be called.";
        public static String ParallelTransactionsNotSupported => $"SqliteConnection does not support nested transactions.";
        public static String ParameterNotFound(object parameterName) => $"A SqliteParameter with ParameterName '{parameterName}' is not contained by this SqliteParameterCollection.";
        public static String RequiresSet(object propertyName) => $"{propertyName} must be set.";
        public static String ResizeNotSupported => $"The size of a blob may not be changed by the SqliteBlob API. Use an UPDATE command instead.";
        public static String SeekBeforeBegin => $"An attempt was made to move the position before the beginning of the stream.";
        public static String SetRequiresNoOpenReader(object propertyName) => $"An open reader is associated with this command. Close it before changing the {propertyName} property.";
        public static String SqlBlobRequiresOpenConnection => $"SqliteBlob can only be used when the connection is open.";
        public static String SqliteNativeError(object errorCode, object message) => $"SQLite Error {errorCode}: '{message}'.";
        public static String TransactionCompleted => $"This SqliteTransaction has completed; it is no longer usable.";
        public static String TransactionConnectionMismatch => $"The transaction object is not associated with the same connection object as this command.";
        public static String TransactionRequired => $"Execute requires the command to have a transaction object when the connection assigned to the command is in a pending local transaction.  The Transaction property of the command has not been initialized.";
        public static String UDFCalledWithNull(object function, object ordinal) => $"The SQL function '{function}' was called with a NULL argument at ordinal {ordinal}. Create the function using a Nullable parameter or rewrite your query to avoid passing NULL.";
        public static String UnknownDataType(object typeName) => $"No mapping exists from object type {typeName} to a known managed provider native type.";
        public static String WriteNotSupported => $"Stream does not support writing.";
    }
}
