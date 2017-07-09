using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Logic.Infrastructure
{
    static class ExceptionMessageBuilder
    {
        public static void FillErrors(Exception ex, ICollection<string> errors)
        {
            do
            {
                errors.Add(ex.Message);
                ex = ex.InnerException;
            } while (ex != null);
        }
    }
}
