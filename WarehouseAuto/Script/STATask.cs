using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WarehouseAuto.Script
{
    public static class STATask
    {

        public static Task<TResult> Run<TResult>(Func<TResult> function)
        {
            var tcs = new TaskCompletionSource<TResult>();

            var thread = new Thread(() =>
            {
                try
                {
                    // Most usages will require a message pump, which can be
                    // started by calling Application.Run() at an appropriate point.

                    tcs.SetResult(function());
                }

                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            return tcs.Task;
        }

        public static Task Run(Action action)
        {
            var tcs = new TaskCompletionSource<object>(); // Return type is irrelevant for an Action.

            var thread = new Thread(() =>
            {
                try
                {
                    action();
                    tcs.SetResult(null); // Irrelevant.
                }

                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            return tcs.Task;
        }

        public static Task Run(Action action, CancellationToken token)
        {
            var tcs = new TaskCompletionSource<object>(); // Return type is irrelevant for an Action.

            var thread = new Thread(() =>
            {
                try
                {
                    action();
                    tcs.SetResult(null); // Irrelevant.
                }

                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            return tcs.Task;
        }
    }
}
