using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;






namespace Cursed_Market
{
    public static class Timer
    {
        private static readonly object _lock = new object();
        private static readonly Dictionary<Label, CancellationTokenSource> _tokens = new Dictionary<Label, CancellationTokenSource>();

        private static void DelayedLabelUpdateImplementation(Label labelReference, string newLabelText)
        {
            if (labelReference == null || labelReference.IsDisposed) return;

            if (labelReference.InvokeRequired)
            {
                labelReference.Invoke(new Action(() =>
                {
                    if (!labelReference.IsDisposed)
                        labelReference.Text = newLabelText;
                }));
            }
            else
            {
                labelReference.Text = newLabelText;
            }
        }

        public static async void DelayedLabelUpdate(Label labelReference, string newLabelText, int updateDelaySeconds)
        {
            if (labelReference == null) return;

            CancellationTokenSource cts = new CancellationTokenSource();

            lock (_lock)
            {
                /* Cancel previous timer for same Label. */
                if (_tokens.TryGetValue(labelReference, out var existingCts))
                {
                    existingCts.Cancel();
                    existingCts.Dispose();
                }

                _tokens[labelReference] = cts;
            }

            try
            {
                await Task.Delay(updateDelaySeconds * 1000, cts.Token);
                DelayedLabelUpdateImplementation(labelReference, newLabelText);
            }
            catch (TaskCanceledException) // Task was canceled by a new status - do nothing.
            { }
            finally
            {
                lock (_lock)
                {
                    if (_tokens.TryGetValue(labelReference, out var currentCts) && currentCts == cts)
                    {
                        _tokens.Remove(labelReference);
                        cts.Dispose();
                    }
                }
            }
        }
    }
}