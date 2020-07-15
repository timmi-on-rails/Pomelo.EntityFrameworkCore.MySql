using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Pomelo.EntityFrameworkCore.MySql.Extensions;
using Pomelo.EntityFrameworkCore.MySql.Metadata.Internal;

namespace Pomelo.EntityFrameworkCore.MySql.Migrations.Internal
{
    public class MySqlRelationalAnnotationProvider : RelationalAnnotationProvider
    {
        public MySqlRelationalAnnotationProvider([NotNull] RelationalAnnotationProviderDependencies dependencies)
            : base(dependencies)
        {
        }

        public override IEnumerable<IAnnotation> For(IColumn column)
        {
            IProperty property = column.PropertyMappings.Single().Property;

            if (property.GetValueGenerationStrategy() == MySqlValueGenerationStrategy.IdentityColumn)
            {
                yield return new Annotation(
                    MySqlAnnotationNames.ValueGenerationStrategy,
                    MySqlValueGenerationStrategy.IdentityColumn);
            }

            if (property.GetValueGenerationStrategy() == MySqlValueGenerationStrategy.ComputedColumn)
            {
                yield return new Annotation(
                    MySqlAnnotationNames.ValueGenerationStrategy,
                    MySqlValueGenerationStrategy.ComputedColumn);
            }

            var charset = property.GetCharSet();
            if (charset != null)
            {
                yield return new Annotation(
                    MySqlAnnotationNames.CharSet,
                    charset);
            }

            var collation = property.GetCollation();
            if (collation != null)
            {
                yield return new Annotation(
                    MySqlAnnotationNames.Collation,
                    collation);
            }
        }
    }
}
