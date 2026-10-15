using System.IO;
using System.Media;






namespace Cursed_Market
{
    public static class Media
    {
        private static readonly SoundPlayer soundPlayer = new SoundPlayer();


        public static void PlaySoundFromStream(UnmanagedMemoryStream soundStream, bool soundLoop = false)
        {
            StopSound();

            if (soundStream == null)
                return;

            if (soundStream.CanSeek)
            {
                soundStream.Position = 0;
            }

            soundPlayer.Stream = soundStream;

            if (soundLoop == true)
                soundPlayer.PlayLooping();
            else
                soundPlayer.Play();
        }
        public static void StopSound()
        {
            soundPlayer.Stop();
            if (soundPlayer.Stream != null)
            {
                soundPlayer.Stream.Dispose();
                soundPlayer.Stream = null;
            }
        }
    }
}