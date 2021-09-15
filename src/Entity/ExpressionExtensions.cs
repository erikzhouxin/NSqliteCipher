using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace System.Data.SQLiteEFCore
{
    [DebuggerStepThrough]
    internal static class ExpressionExtensions
    {
        public static bool IsNullConstantExpression([NotNull] this Expression expression)
            => RemoveConvert(expression) is ConstantExpression constantExpression
                && constantExpression.Value == null;

        public static LambdaExpression UnwrapLambdaFromQuote(this Expression expression)
            => (LambdaExpression)(expression is UnaryExpression unary && expression.NodeType == ExpressionType.Quote
                ? unary.Operand
                : expression);

        public static Expression UnwrapTypeConversion(this Expression expression, out Type convertedType)
        {
            convertedType = null;
            while (expression is UnaryExpression unaryExpression
                && (unaryExpression.NodeType == ExpressionType.Convert
                    || unaryExpression.NodeType == ExpressionType.ConvertChecked
                    || unaryExpression.NodeType == ExpressionType.TypeAs))
            {
                expression = unaryExpression.Operand;
                if (unaryExpression.Type != typeof(object) // Ignore object conversion
                    && !unaryExpression.Type.IsAssignableFrom(expression.Type)) // Ignore casting to base type/interface
                {
                    convertedType = unaryExpression.Type;
                }
            }

            return expression;
        }

        private static Expression RemoveConvert(Expression expression)
        {
            if (expression is UnaryExpression unaryExpression
                && (expression.NodeType == ExpressionType.Convert
                    || expression.NodeType == ExpressionType.ConvertChecked))
            {
                return RemoveConvert(unaryExpression.Operand);
            }

            return expression;
        }
    }
}
