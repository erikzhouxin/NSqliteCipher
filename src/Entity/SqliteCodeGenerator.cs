using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Scaffolding;
using System;

namespace System.Data.SQLiteEFCore
{
    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public class SqliteCodeGenerator : ProviderCodeGenerator
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SqliteCodeGenerator" /> class.
        /// </summary>
        /// <param name="dependencies"> The dependencies. </param>
        public SqliteCodeGenerator([NotNull] ProviderCodeGeneratorDependencies dependencies)
            : base(dependencies)
        {
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public override MethodCallCodeFragment GenerateUseProvider(
            string connectionString,
            MethodCallCodeFragment providerOptions)
            => new MethodCallCodeFragment(
                nameof(SqliteDbContextOptionsBuilderExtensions.UseSqlite),
                providerOptions == null
                    ? new object[] { connectionString }
                    : new object[] { connectionString, new NestedClosureCodeFragment("x", providerOptions) });
    }
}
