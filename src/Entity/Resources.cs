using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.SQLiteEFCore
{
    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public static class SqliteStrings
    {
        /// <summary>
        /// $"SQLite cannot apply aggregate operator '{aggregateOperator}' on expression of type '{type}'. Convert the values to a supported type or use LINQ to Objects to aggregate the results."
        /// </summary>
        /// <param name="aggregateOperator"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static String AggregateOperationNotSupported(object aggregateOperator, object type) => $"SQLite cannot apply aggregate operator '{aggregateOperator}' on expression of type '{type}'. Convert the values to a supported type or use LINQ to Objects to aggregate the results.";
        /// <summary>
        /// $"Translating this query requires APPLY operation in SQL which is not supported on SQLite."
        /// </summary>
        public static String ApplyNotSupported => $"Translating this query requires APPLY operation in SQL which is not supported on SQLite.";
        /// <summary>
        /// $"'{entityType1}.{property1}' and '{entityType2}.{property2}' are both mapped to column '{columnName}' in '{table}' but are configured with different SRIDs."
        /// </summary>
        /// <param name="entityType1"></param>
        /// <param name="property1"></param>
        /// <param name="entityType2"></param>
        /// <param name="property2"></param>
        /// <param name="columnName"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static String DuplicateColumnNameSridMismatch(object entityType1, object property1, object entityType2, object property2, object columnName, object table) => $"'{entityType1}.{property1}' and '{entityType2}.{property2}' are both mapped to column '{columnName}' in '{table}' but are configured with different SRIDs.";
        /// <summary>
        /// $"SQLite does not support this migration operation ('{operation}'). For more information, see http://go.microsoft.com/fwlink/?LinkId=723262."
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        public static String InvalidMigrationOperation(object operation) => $"SQLite does not support this migration operation ('{operation}'). For more information, see http://go.microsoft.com/fwlink/?LinkId=723262.";
        /// <summary>
        ///  $"Skipping foreign key with identity '{id}' on table '{tableName}' since principal table '{principalTableName}' was not found in the model. This usually happens if the principal table was not included in the selection set."
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tableName"></param>
        /// <param name="principalTableName"></param>
        /// <returns></returns>
        public static String LogForeignKeyScaffoldErrorPrincipalTableNotFound(object id, object tableName, object principalTableName) => $"Skipping foreign key with identity '{id}' on table '{tableName}' since principal table '{principalTableName}' was not found in the model. This usually happens if the principal table was not included in the selection set.";
        /// <summary>
        ///  $"Found column on table: {tableName}, column name: {columnName}, data type: {dataType}, not nullable: {notNullable}, default value: {defaultValue}."
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="dataType"></param>
        /// <param name="notNullable"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static String LogFoundColumn(object tableName, object columnName, object dataType, object notNullable, object defaultValue) => $"Found column on table: {tableName}, column name: {columnName}, data type: {dataType}, not nullable: {notNullable}, default value: {defaultValue}.";
        /// <summary>
        /// $"Found foreign key on table: {tableName}, id: {id}, principal table: {principalTableName}, delete action: {deleteAction}."
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="id"></param>
        /// <param name="principalTableName"></param>
        /// <param name="deleteAction"></param>
        /// <returns></returns>
        public static String LogFoundForeignKey(object tableName, object id, object principalTableName, object deleteAction) => $"Found foreign key on table: {tableName}, id: {id}, principal table: {principalTableName}, delete action: {deleteAction}.";
        /// <summary>
        /// $"Found index with name: {indexName}, table: {tableName}, is unique: {unique}.";
        /// </summary>
        /// <param name="indexName"></param>
        /// <param name="tableName"></param>
        /// <param name="unique"></param>
        /// <returns></returns>
        public static String LogFoundIndex(object indexName, object tableName, object unique) => $"Found index with name: {indexName}, table: {tableName}, is unique: {unique}.";
        /// <summary>
        /// $"Found primary key with name: {primaryKeyName}, table: {tableName}.";
        /// </summary>
        /// <param name="primaryKeyName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static String LogFoundPrimaryKey(object primaryKeyName, object tableName ) => $"Found primary key with name: {primaryKeyName}, table: {tableName}.";
        /// <summary>
        /// $"Found table with name: {name}.";
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static String LogFoundTable(object name) => $"Found table with name: {name}.";
        /// <summary>
        /// $"Found unique constraint with name: {uniqueConstraintName}, table: {tableName}.";
        /// </summary>
        /// <param name="uniqueConstraintName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static String LogFoundUniqueConstraint(object uniqueConstraintName,object  tableName) => $"Found unique constraint with name: {uniqueConstraintName}, table: {tableName}.";
        /// <summary>
        /// $"Unable to find a table in the database matching the selected table {table}.";
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static String LogMissingTable(object table) => $"Unable to find a table in the database matching the selected table {table}.";
        /// <summary>
        /// $"Skipping foreign key with identity '{id}' on table '{tableName}' since the principal column called '{principalColumnName}' on the foreign key's principal table, '{principalTableName}' was not found in the model.";
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tableName"></param>
        /// <param name="principalColumnName"></param>
        /// <param name="principalTableName"></param>
        /// <returns></returns>
        public static String LogPrincipalColumnNotFound(object id, object tableName, object principalColumnName, object principalTableName) => $"Skipping foreign key with identity '{id}' on table '{tableName}' since the principal column called '{principalColumnName}' on the foreign key's principal table, '{principalTableName}' was not found in the model.";
        /// <summary>
        /// $"The entity type '{entityType}' is configured to use schema '{schema}'. SQLite does not support schemas. This configuration will be ignored by the SQLite provider.";
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="schema"></param>
        /// <returns></returns>
        public static String LogSchemaConfigured(object entityType, object schema) => $"The entity type '{entityType}' is configured to use schema '{schema}'. SQLite does not support schemas. This configuration will be ignored by the SQLite provider.";
        /// <summary>
        /// $"The model was configured with the database sequence '{sequence}'. SQLite does not support sequences.";
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static String LogSequenceConfigured(object sequence) => $"The model was configured with the database sequence '{sequence}'. SQLite does not support sequences.";
        /// <summary>
        /// $"Warning, an operation of type '{operationType}' will be attempted while a rebuild of table '{tableName}' is pending. The database may not be in an expected state. Review the SQL generated by this migration to help diagnose any failures. Consider moving these operations to a subsequent migration.";
        /// </summary>
        /// <param name="operationType"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static String LogTableRebuildPendingWarning(object operationType, object tableName) => $"Warning, an operation of type '{operationType}' will be attempted while a rebuild of table '{tableName}' is pending. The database may not be in an expected state. Review the SQL generated by this migration to help diagnose any failures. Consider moving these operations to a subsequent migration.";
        /// <summary>
        /// $"A connection of an unexpected type ({type}) is being used. The SQL functions prefixed with 'ef_' could not be created automatically. Manually define them if you encounter errors while querying.";
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static String LogUnexpectedConnectionType(object type) => $"A connection of an unexpected type ({type}) is being used. The SQL functions prefixed with 'ef_' could not be created automatically. Manually define them if you encounter errors while querying.";
        /// <summary>
        /// $"SQLite doesn't support schemas. The specified schema selection arguments will be ignored.";
        /// </summary>
        public static String LogUsingSchemaSelectionsWarning => $"SQLite doesn't support schemas. The specified schema selection arguments will be ignored.";
        /// <summary>
        /// $"Generating idempotent scripts for migration is not currently supported by SQLite. For more information, see http://go.microsoft.com/fwlink/?LinkId=723262.";
        /// </summary>
        public static String MigrationScriptGenerationNotSupported => $"Generating idempotent scripts for migration is not currently supported by SQLite. For more information, see http://go.microsoft.com/fwlink/?LinkId=723262.";
        /// <summary>
        /// $"SQLite cannot order by expressions of type '{type}'. Convert the values to a supported type or use LINQ to Objects to order the results.";
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static String OrderByNotSupported(object type) => $"SQLite cannot order by expressions of type '{type}'. Convert the values to a supported type or use LINQ to Objects to order the results.";
        /// <summary>
        /// $"SQLite does not support sequences. For more information, see http://go.microsoft.com/fwlink/?LinkId=723262.";
        /// </summary>
        public static String SequencesNotSupported => $"SQLite does not support sequences. For more information, see http://go.microsoft.com/fwlink/?LinkId=723262.";

    }
    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public static class SqliteResources
    {
        /// <summary>
        ///     Skipping foreign key with identity '{id}' on table '{tableName}' since principal table '{principalTableName}' was not found in the model. This usually happens if the principal table was not included in the selection set.
        /// </summary>
        public static EventDefinition<string, string, string> LogForeignKeyScaffoldErrorPrincipalTableNotFound([NotNull] IDiagnosticsLogger logger)
        {
            var definition = ((SqliteLoggingDefinitions)logger.Definitions).LogForeignKeyScaffoldErrorPrincipalTableNotFound;
            if (definition == null)
            {
                definition = LazyInitializer.EnsureInitialized<EventDefinitionBase>(
                    ref ((SqliteLoggingDefinitions)logger.Definitions).LogForeignKeyScaffoldErrorPrincipalTableNotFound,
                    () => new EventDefinition<string, string, string>(
                        logger.Options,
                        SqliteEventId.ForeignKeyReferencesMissingTableWarning,
                        LogLevel.Warning,
                        "SqliteEventId.ForeignKeyReferencesMissingTableWarning",
                        level => LoggerMessage.Define<string, string, string>(
                            level,
                            SqliteEventId.ForeignKeyReferencesMissingTableWarning,
                            Resources.LogForeignKeyScaffoldErrorPrincipalTableNotFound)));
            }

            return (EventDefinition<string, string, string>)definition;
        }

        /// <summary>
        ///     Found column on table: {tableName}, column name: {columnName}, data type: {dataType}, not nullable: {notNullable}, default value: {defaultValue}.
        /// </summary>
        public static EventDefinition<string, string, string, bool, string> LogFoundColumn([NotNull] IDiagnosticsLogger logger)
        {
            var definition = ((SqliteLoggingDefinitions)logger.Definitions).LogFoundColumn;
            if (definition == null)
            {
                definition = LazyInitializer.EnsureInitialized<EventDefinitionBase>(
                    ref ((SqliteLoggingDefinitions)logger.Definitions).LogFoundColumn,
                    () => new EventDefinition<string, string, string, bool, string>(
                        logger.Options,
                        SqliteEventId.ColumnFound,
                        LogLevel.Debug,
                        "SqliteEventId.ColumnFound",
                        level => LoggerMessage.Define<string, string, string, bool, string>(
                            level,
                            SqliteEventId.ColumnFound,
                            Resources.LogFoundColumn)));
            }

            return (EventDefinition<string, string, string, bool, string>)definition;
        }

        /// <summary>
        ///     Found foreign key on table: {tableName}, id: {id}, principal table: {principalTableName}, delete action: {deleteAction}.
        /// </summary>
        public static EventDefinition<string, long, string, string> LogFoundForeignKey([NotNull] IDiagnosticsLogger logger)
        {
            var definition = ((SqliteLoggingDefinitions)logger.Definitions).LogFoundForeignKey;
            if (definition == null)
            {
                definition = LazyInitializer.EnsureInitialized<EventDefinitionBase>(
                    ref ((SqliteLoggingDefinitions)logger.Definitions).LogFoundForeignKey,
                    () => new EventDefinition<string, long, string, string>(
                        logger.Options,
                        SqliteEventId.ForeignKeyFound,
                        LogLevel.Debug,
                        "SqliteEventId.ForeignKeyFound",
                        level => LoggerMessage.Define<string, long, string, string>(
                            level,
                            SqliteEventId.ForeignKeyFound,
                            Resources.LogFoundForeignKey)));
            }

            return (EventDefinition<string, long, string, string>)definition;
        }

        /// <summary>
        ///     Found index with name: {indexName}, table: {tableName}, is unique: {unique}.
        /// </summary>
        public static EventDefinition<string, string, bool?> LogFoundIndex([NotNull] IDiagnosticsLogger logger)
        {
            var definition = ((SqliteLoggingDefinitions)logger.Definitions).LogFoundIndex;
            if (definition == null)
            {
                definition = LazyInitializer.EnsureInitialized<EventDefinitionBase>(
                    ref ((SqliteLoggingDefinitions)logger.Definitions).LogFoundIndex,
                    () => new EventDefinition<string, string, bool?>(
                        logger.Options,
                        SqliteEventId.IndexFound,
                        LogLevel.Debug,
                        "SqliteEventId.IndexFound",
                        level => LoggerMessage.Define<string, string, bool?>(
                            level,
                            SqliteEventId.IndexFound,
                            Resources.LogFoundIndex)));
            }

            return (EventDefinition<string, string, bool?>)definition;
        }

        /// <summary>
        ///     Found primary key with name: {primaryKeyName}, table: {tableName}.
        /// </summary>
        public static EventDefinition<string, string> LogFoundPrimaryKey([NotNull] IDiagnosticsLogger logger)
        {
            var definition = ((SqliteLoggingDefinitions)logger.Definitions).LogFoundPrimaryKey;
            if (definition == null)
            {
                definition = LazyInitializer.EnsureInitialized<EventDefinitionBase>(
                    ref ((SqliteLoggingDefinitions)logger.Definitions).LogFoundPrimaryKey,
                    () => new EventDefinition<string, string>(
                        logger.Options,
                        SqliteEventId.PrimaryKeyFound,
                        LogLevel.Debug,
                        "SqliteEventId.PrimaryKeyFound",
                        level => LoggerMessage.Define<string, string>(
                            level,
                            SqliteEventId.PrimaryKeyFound,
                            Resources.LogFoundPrimaryKey)));
            }

            return (EventDefinition<string, string>)definition;
        }

        /// <summary>
        ///     Found table with name: {name}.
        /// </summary>
        public static EventDefinition<string> LogFoundTable([NotNull] IDiagnosticsLogger logger)
        {
            var definition = ((SqliteLoggingDefinitions)logger.Definitions).LogFoundTable;
            if (definition == null)
            {
                definition = LazyInitializer.EnsureInitialized<EventDefinitionBase>(
                    ref ((SqliteLoggingDefinitions)logger.Definitions).LogFoundTable,
                    () => new EventDefinition<string>(
                        logger.Options,
                        SqliteEventId.TableFound,
                        LogLevel.Debug,
                        "SqliteEventId.TableFound",
                        level => LoggerMessage.Define<string>(
                            level,
                            SqliteEventId.TableFound,
                            Resources.LogFoundTable)));
            }

            return (EventDefinition<string>)definition;
        }

        /// <summary>
        ///     Found unique constraint with name: {uniqueConstraintName}, table: {tableName}.
        /// </summary>
        public static EventDefinition<string, string> LogFoundUniqueConstraint([NotNull] IDiagnosticsLogger logger)
        {
            var definition = ((SqliteLoggingDefinitions)logger.Definitions).LogFoundUniqueConstraint;
            if (definition == null)
            {
                definition = LazyInitializer.EnsureInitialized<EventDefinitionBase>(
                    ref ((SqliteLoggingDefinitions)logger.Definitions).LogFoundUniqueConstraint,
                    () => new EventDefinition<string, string>(
                        logger.Options,
                        SqliteEventId.UniqueConstraintFound,
                        LogLevel.Debug,
                        "SqliteEventId.UniqueConstraintFound",
                        level => LoggerMessage.Define<string, string>(
                            level,
                            SqliteEventId.UniqueConstraintFound,
                            Resources.LogFoundUniqueConstraint)));
            }

            return (EventDefinition<string, string>)definition;
        }

        /// <summary>
        ///     Unable to find a table in the database matching the selected table {table}.
        /// </summary>
        public static EventDefinition<string> LogMissingTable([NotNull] IDiagnosticsLogger logger)
        {
            var definition = ((SqliteLoggingDefinitions)logger.Definitions).LogMissingTable;
            if (definition == null)
            {
                definition = LazyInitializer.EnsureInitialized<EventDefinitionBase>(
                    ref ((SqliteLoggingDefinitions)logger.Definitions).LogMissingTable,
                    () => new EventDefinition<string>(
                        logger.Options,
                        SqliteEventId.MissingTableWarning,
                        LogLevel.Warning,
                        "SqliteEventId.MissingTableWarning",
                        level => LoggerMessage.Define<string>(
                            level,
                            SqliteEventId.MissingTableWarning,
                            Resources.LogMissingTable)));
            }

            return (EventDefinition<string>)definition;
        }

        /// <summary>
        ///     Skipping foreign key with identity '{id}' on table '{tableName}' since the principal column called '{principalColumnName}' on the foreign key's principal table, '{principalTableName}' was not found in the model.
        /// </summary>
        public static EventDefinition<string, string, string, string> LogPrincipalColumnNotFound([NotNull] IDiagnosticsLogger logger)
        {
            var definition = ((SqliteLoggingDefinitions)logger.Definitions).LogPrincipalColumnNotFound;
            if (definition == null)
            {
                definition = LazyInitializer.EnsureInitialized<EventDefinitionBase>(
                    ref ((SqliteLoggingDefinitions)logger.Definitions).LogPrincipalColumnNotFound,
                    () => new EventDefinition<string, string, string, string>(
                        logger.Options,
                        SqliteEventId.ForeignKeyPrincipalColumnMissingWarning,
                        LogLevel.Warning,
                        "SqliteEventId.ForeignKeyPrincipalColumnMissingWarning",
                        level => LoggerMessage.Define<string, string, string, string>(
                            level,
                            SqliteEventId.ForeignKeyPrincipalColumnMissingWarning,
                            Resources.LogPrincipalColumnNotFound)));
            }

            return (EventDefinition<string, string, string, string>)definition;
        }

        /// <summary>
        ///     The entity type '{entityType}' is configured to use schema '{schema}'. SQLite does not support schemas. This configuration will be ignored by the SQLite provider.
        /// </summary>
        public static EventDefinition<string, string> LogSchemaConfigured([NotNull] IDiagnosticsLogger logger)
        {
            var definition = ((SqliteLoggingDefinitions)logger.Definitions).LogSchemaConfigured;
            if (definition == null)
            {
                definition = LazyInitializer.EnsureInitialized<EventDefinitionBase>(
                    ref ((SqliteLoggingDefinitions)logger.Definitions).LogSchemaConfigured,
                    () => new EventDefinition<string, string>(
                        logger.Options,
                        SqliteEventId.SchemaConfiguredWarning,
                        LogLevel.Warning,
                        "SqliteEventId.SchemaConfiguredWarning",
                        level => LoggerMessage.Define<string, string>(
                            level,
                            SqliteEventId.SchemaConfiguredWarning,
                            Resources.LogSchemaConfigured)));
            }

            return (EventDefinition<string, string>)definition;
        }

        /// <summary>
        ///     The model was configured with the database sequence '{sequence}'. SQLite does not support sequences.
        /// </summary>
        public static EventDefinition<string> LogSequenceConfigured([NotNull] IDiagnosticsLogger logger)
        {
            var definition = ((SqliteLoggingDefinitions)logger.Definitions).LogSequenceConfigured;
            if (definition == null)
            {
                definition = LazyInitializer.EnsureInitialized<EventDefinitionBase>(
                    ref ((SqliteLoggingDefinitions)logger.Definitions).LogSequenceConfigured,
                    () => new EventDefinition<string>(
                        logger.Options,
                        SqliteEventId.SequenceConfiguredWarning,
                        LogLevel.Warning,
                        "SqliteEventId.SequenceConfiguredWarning",
                        level => LoggerMessage.Define<string>(
                            level,
                            SqliteEventId.SequenceConfiguredWarning,
                            Resources.LogSequenceConfigured)));
            }

            return (EventDefinition<string>)definition;
        }

        /// <summary>
        ///     Warning, an operation of type '{operationType}' will be attempted while a rebuild of table '{tableName}' is pending. The database may not be in an expected state. Review the SQL generated by this migration to help diagnose any failures. Consider moving these operations to a subsequent migration.
        /// </summary>
        public static EventDefinition<string, string> LogTableRebuildPendingWarning([NotNull] IDiagnosticsLogger logger)
        {
            var definition = ((SqliteLoggingDefinitions)logger.Definitions).LogTableRebuildPendingWarning;
            if (definition == null)
            {
                definition = LazyInitializer.EnsureInitialized<EventDefinitionBase>(
                    ref ((SqliteLoggingDefinitions)logger.Definitions).LogTableRebuildPendingWarning,
                    () => new EventDefinition<string, string>(
                        logger.Options,
                        SqliteEventId.TableRebuildPendingWarning,
                        LogLevel.Warning,
                        "SqliteEventId.TableRebuildPendingWarning",
                        level => LoggerMessage.Define<string, string>(
                            level,
                            SqliteEventId.TableRebuildPendingWarning,
                            Resources.LogTableRebuildPendingWarning)));
            }

            return (EventDefinition<string, string>)definition;
        }

        /// <summary>
        ///     A connection of an unexpected type ({type}) is being used. The SQL functions prefixed with 'ef_' could not be created automatically. Manually define them if you encounter errors while querying.
        /// </summary>
        public static EventDefinition<string> LogUnexpectedConnectionType([NotNull] IDiagnosticsLogger logger)
        {
            var definition = ((SqliteLoggingDefinitions)logger.Definitions).LogUnexpectedConnectionType;
            if (definition == null)
            {
                definition = LazyInitializer.EnsureInitialized<EventDefinitionBase>(
                    ref ((SqliteLoggingDefinitions)logger.Definitions).LogUnexpectedConnectionType,
                    () => new EventDefinition<string>(
                        logger.Options,
                        SqliteEventId.UnexpectedConnectionTypeWarning,
                        LogLevel.Warning,
                        "SqliteEventId.UnexpectedConnectionTypeWarning",
                        level => LoggerMessage.Define<string>(
                            level,
                            SqliteEventId.UnexpectedConnectionTypeWarning,
                            Resources.LogUnexpectedConnectionType)));
            }

            return (EventDefinition<string>)definition;
        }

        /// <summary>
        ///     SQLite doesn't support schemas. The specified schema selection arguments will be ignored.
        /// </summary>
        public static EventDefinition LogUsingSchemaSelectionsWarning([NotNull] IDiagnosticsLogger logger)
        {
            var definition = ((SqliteLoggingDefinitions)logger.Definitions).LogUsingSchemaSelectionsWarning;
            if (definition == null)
            {
                definition = LazyInitializer.EnsureInitialized<EventDefinitionBase>(
                    ref ((SqliteLoggingDefinitions)logger.Definitions).LogUsingSchemaSelectionsWarning,
                    () => new EventDefinition(
                        logger.Options,
                        SqliteEventId.SchemasNotSupportedWarning,
                        LogLevel.Warning,
                        "SqliteEventId.SchemasNotSupportedWarning",
                        level => LoggerMessage.Define(
                            level,
                            SqliteEventId.SchemasNotSupportedWarning,
                            Resources.LogUsingSchemaSelectionsWarning)));
            }

            return (EventDefinition)definition;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class Resources
    {
        /// <summary>
        /// 
        /// </summary>
        public static String AggregateOperationNotSupported => "SQLite cannot apply aggregate operator '{aggregateOperator}' on expression of type '{type}'. Convert the values to a supported type or use LINQ to Objects to aggregate the results.";
        /// <summary>
        /// 
        /// </summary>
        public static String ApplyNotSupported => "Translating this query requires APPLY operation in SQL which is not supported on SQLite.";
        /// <summary>
        /// 
        /// </summary>
        public static String DuplicateColumnNameSridMismatch => "'{entityType1}.{property1}' and '{entityType2}.{property2}' are both mapped to column '{columnName}' in '{table}' but are configured with different SRIDs.";
        /// <summary>
        /// 
        /// </summary>
        public static String InvalidMigrationOperation => "SQLite does not support this migration operation ('{operation}'). For more information, see http://go.microsoft.com/fwlink/?LinkId=723262.";
        /// <summary>
        /// 
        /// </summary>
        public static String LogForeignKeyScaffoldErrorPrincipalTableNotFound => "Skipping foreign key with identity '{id}' on table '{tableName}' since principal table '{principalTableName}' was not found in the model. This usually happens if the principal table was not included in the selection set.";
        /// <summary>
        /// 
        /// </summary>
        public static String LogFoundColumn => "Found column on table: {tableName}, column name: {columnName}, data type: {dataType}, not nullable: {notNullable}, default value: {defaultValue}.";
        /// <summary>
        /// 
        /// </summary>
        public static String LogFoundForeignKey => "Found foreign key on table: {tableName}, id: {id}, principal table: {principalTableName}, delete action: {deleteAction}.";
        /// <summary>
        /// 
        /// </summary>
        public static String LogFoundIndex => "Found index with name: {indexName}, table: {tableName}, is unique: {unique}.";
        /// <summary>
        /// 
        /// </summary>
        public static String LogFoundPrimaryKey => "Found primary key with name: {primaryKeyName}, table: {tableName}.";
        /// <summary>
        /// 
        /// </summary>
        public static String LogFoundTable => "Found table with name: {name}.";
        /// <summary>
        /// 
        /// </summary>
        public static String LogFoundUniqueConstraint => "Found unique constraint with name: {uniqueConstraintName}, table: {tableName}.";
        /// <summary>
        /// 
        /// </summary>
        public static String LogMissingTable => "Unable to find a table in the database matching the selected table {table}.";
        /// <summary>
        /// 
        /// </summary>
        public static String LogPrincipalColumnNotFound => "Skipping foreign key with identity '{id}' on table '{tableName}' since the principal column called '{principalColumnName}' on the foreign key's principal table, '{principalTableName}' was not found in the model.";
        /// <summary>
        /// 
        /// </summary>
        public static String LogSchemaConfigured => "The entity type '{entityType}' is configured to use schema '{schema}'. SQLite does not support schemas. This configuration will be ignored by the SQLite provider.";
        /// <summary>
        /// 
        /// </summary>
        public static String LogSequenceConfigured => "The model was configured with the database sequence '{sequence}'. SQLite does not support sequences.";
        /// <summary>
        /// 
        /// </summary>
        public static String LogTableRebuildPendingWarning => "Warning, an operation of type '{operationType}' will be attempted while a rebuild of table '{tableName}' is pending. The database may not be in an expected state. Review the SQL generated by this migration to help diagnose any failures. Consider moving these operations to a subsequent migration.";
        /// <summary>
        /// 
        /// </summary>
        public static String LogUnexpectedConnectionType => "A connection of an unexpected type ({type}) is being used. The SQL functions prefixed with 'ef_' could not be created automatically. Manually define them if you encounter errors while querying.";
        /// <summary>
        /// 
        /// </summary>
        public static String LogUsingSchemaSelectionsWarning => "SQLite doesn't support schemas. The specified schema selection arguments will be ignored.";
        /// <summary>
        /// 
        /// </summary>
        public static String MigrationScriptGenerationNotSupported => "Generating idempotent scripts for migration is not currently supported by SQLite. For more information, see http://go.microsoft.com/fwlink/?LinkId=723262.";
        /// <summary>
        /// 
        /// </summary>
        public static String OrderByNotSupported => "SQLite cannot order by expressions of type '{type}'. Convert the values to a supported type or use LINQ to Objects to order the results.";
        /// <summary>
        /// 
        /// </summary>
        public static String SequencesNotSupported => "SQLite does not support sequences. For more information, see http://go.microsoft.com/fwlink/?LinkId=723262.";
    }
}
