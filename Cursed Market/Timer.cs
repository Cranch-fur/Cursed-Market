using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cursed_Market
{
    public static class Timer
    {
        private static void DelayedLabelUpdateImplemintation(Label labelReference, string newLabelText)
        {
            if (labelReference.InvokeRequired == true)
            {
                labelReference.Invoke(new Action(() => labelReference.Text = newLabelText));
            }
            else
            {
                labelReference.Text = newLabelText;
            }
        }
        public static async void DelayedLabelUpdate(Label labelReference, string newLabelText, int updateDelay)
        {
            updateDelay = updateDelay * 1000; // milliseconds to seconds | 1 * 1000 = 1000, 1000ms = 1s
            await Task.Delay(updateDelay);

            DelayedLabelUpdateImplemintation(labelReference, newLabelText);
        }
    }
}
