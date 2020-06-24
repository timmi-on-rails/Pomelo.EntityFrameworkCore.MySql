using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;

namespace Pomelo.EntityFrameworkCore.MySql.Query.ExpressionVisitors.Internal
{
    public class MySqlSqlTranslatingExpressionVisitorFactory : IRelationalSqlTranslatingExpressionVisitorFactory
    {
        private readonly RelationalSqlTranslatingExpressionVisitorDependencies _dependencies;

        public MySqlSqlTranslatingExpressionVisitorFactory(
            [NotNull] RelationalSqlTranslatingExpressionVisitorDependencies dependencies)
        {
            _dependencies = dependencies;
        }

        public virtual RelationalSqlTranslatingExpressionVisitor Create(
            QueryCompilationContext queryCompilationContext,
            QueryableMethodTranslatingExpressionVisitor queryableMethodTranslatingExpressionVisitor)
            => new MySqlSqlTranslatingExpressionVisitor(
                _dependencies,
                queryCompilationContext.Model,
                queryableMethodTranslatingExpressionVisitor);
    }
}
