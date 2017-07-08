using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Logic.Infrastructure
{
    static class ExceptionMessageBuilder
    {
        public static string Build(Exception ex)
        {
            StringBuilder builder = new StringBuilder();

            do
            {
                builder.AppendFormat("{0};", ex.Message);
                ex = ex.InnerException;
            } while (ex != null);

            return builder.ToString();
        }
    }
}
