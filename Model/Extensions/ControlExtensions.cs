using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;

namespace Model.Extensions
{
    /// <remarks>Ian Kemp from http://stackoverflow.com/questions/661561/how-to-update-gui-from-another-thread-in-c </remarks>
    public static class ControlExtensions
    {
        private delegate void SetDelegate<TResult>(Control @this, Expression<Func<TResult>> property, TResult value);

        public static void Set<TResult>(this Control @this, Expression<Func<TResult>> property, TResult value)
        {
            if (property.Body == null)
                throw new ArgumentNullException("property.Body");

            var propertyInfo = (property.Body as MemberExpression).Member as PropertyInfo;

            if (propertyInfo == null ||
                !@this.GetType().IsSubclassOf(propertyInfo.ReflectedType) ||
                @this.GetType().GetProperty(propertyInfo.Name, propertyInfo.PropertyType) == null)
            {
                throw new ArgumentException("The lambda expression 'property' must reference a valid property on this Control.");
            }

            if (@this.InvokeRequired)
            {
                @this.Invoke(new SetDelegate<TResult>(Set), new object[] {@this, property, value});
            }

            else
            {
                @this.GetType().InvokeMember(propertyInfo.Name, BindingFlags.SetProperty, null, @this, new object[] {value});
            }
        }
    }
}