using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DbSchemaAttribute : Attribute
    {
        public string Schema { get; }
        public DbSchemaAttribute(string schema)
        {
            Schema = schema;
        }

    }
}
