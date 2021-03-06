using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace System.Data.SQLiteEFCore
{
    /// <summary>
    ///     SQLite-specific extension methods for <see cref="PropertyBuilder" />.
    /// </summary>
    public static class SqlitePropertyBuilderExtensions
    {
        /// <summary>
        ///     Configures the SRID of the column that the property maps to when targeting SQLite.
        /// </summary>
        /// <param name="propertyBuilder"> The builder for the property being configured. </param>
        /// <param name="srid"> The SRID. </param>
        /// <returns> The same builder instance so that multiple calls can be chained. </returns>
        public static PropertyBuilder HasSrid([NotNull] this PropertyBuilder propertyBuilder, int srid)
        {
            Check.NotNull(propertyBuilder, nameof(propertyBuilder));

            propertyBuilder.Metadata.SetSrid(srid);

            return propertyBuilder;
        }

        /// <summary>
        ///     Configures the SRID of the column that the property maps to when targeting SQLite.
        /// </summary>
        /// <param name="propertyBuilder"> The builder for the property being configured. </param>
        /// <param name="srid"> The SRID. </param>
        /// <returns> The same builder instance so that multiple calls can be chained. </returns>
        public static PropertyBuilder<TProperty> HasSrid<TProperty>(
            [NotNull] this PropertyBuilder<TProperty> propertyBuilder,
            int srid)
            => (PropertyBuilder<TProperty>)HasSrid((PropertyBuilder)propertyBuilder, srid);

        /// <summary>
        ///     Configures the SRID of the column that the property maps to when targeting SQLite.
        /// </summary>
        /// <param name="propertyBuilder"> The builder for the property being configured. </param>
        /// <param name="srid"> The SRID. </param>
        /// <param name="fromDataAnnotation"> Indicates whether the configuration was specified using a data annotation. </param>
        /// <returns>
        ///     The same builder instance if the configuration was applied,
        ///     <see langword="null" /> otherwise.
        /// </returns>
        public static IConventionPropertyBuilder HasSrid(
            [NotNull] this IConventionPropertyBuilder propertyBuilder,
            int? srid,
            bool fromDataAnnotation = false)
        {
            if (propertyBuilder.CanSetSrid(srid, fromDataAnnotation))
            {
                propertyBuilder.Metadata.SetSrid(srid, fromDataAnnotation);

                return propertyBuilder;
            }

            return null;
        }

        /// <summary>
        ///     Returns a value indicating whether the given value can be set as the SRID for the column.
        /// </summary>
        /// <param name="propertyBuilder"> The builder for the property being configured. </param>
        /// <param name="srid"> The SRID. </param>
        /// <param name="fromDataAnnotation"> Indicates whether the configuration was specified using a data annotation. </param>
        /// <returns> <see langword="true" /> if the given value can be set as the SRID for the column. </returns>
        public static bool CanSetSrid(
            [NotNull] this IConventionPropertyBuilder propertyBuilder,
            int? srid,
            bool fromDataAnnotation = false)
            => Check.NotNull(propertyBuilder, nameof(propertyBuilder)).CanSetAnnotation(
                SqliteAnnotationNames.Srid,
                srid,
                fromDataAnnotation);
    }
}
