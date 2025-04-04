using System.IO;
using System.Media;

namespace Cursed_Market
{
    public static class Media
    {
        private static SoundPlayer soundPlayer = new SoundPlayer();


        public static void PlaySoundFromStream(UnmanagedMemoryStream soundStream, bool soundLoop = false)
        {
            StopSound(); // It's likely that System.Media already has safety measures in place, keep it here just in case.


            soundPlayer.Stream = soundStream;

            if (soundLoop == true)
                soundPlayer.PlayLooping();
            else
                soundPlayer.Play();
        }
        public static void StopSound()
        {
            soundPlayer.Stop();
            if (soundPlayer.Stream != null) // Make sure there's a stream to flush before attempting to. Either way we would face an instant crash.
            {
                soundPlayer.Stream.Flush();
            }
        }
    }
}
