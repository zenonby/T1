using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1Test
{
    public static class AssertEx
    {
        public static async Task ThrowsAsync(Func<Task> action)
        {
            bool ok = false;
            try
            {
                await action();

                ok = true;
            }
            catch (Exception) { }

            if (ok)
                Assert.Fail();
        }
    }
}
