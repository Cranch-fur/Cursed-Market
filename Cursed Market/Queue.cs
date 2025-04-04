using System;

namespace Cursed_Market
{
    public static class Queue
    {
        public enum E_QueueStatus
        {
            None,
            Searching,
            LobbyFound,
            LobbyIdle,
            MatchStarting
        }


        private static E_QueueStatus queueStatus = E_QueueStatus.None;
        private static int queuePosition = 0;


        public static void SetQueueStatus(E_QueueStatus newStatus)
        {
            queueStatus = newStatus;

            if (newStatus != E_QueueStatus.Searching)
                queuePosition = 0; // Queue position can not be set if we're not queing up.
        }
        public static void SetQueuePosition(int newPosition)
        {
            queuePosition = newPosition;
        }
        public static void UpdateQueue()
        {
            // Threading can f#ck things up, we need to check if Invoking is required.
            if (Globals_Cache.Forms.Main.InvokeRequired)
            {
                Globals_Cache.Forms.Main.Invoke(new Action(() =>
                    Globals_Cache.Forms.Main.LocalUpdateQueue(queueStatus, queuePosition)));
            }
            else
            {
                Globals_Cache.Forms.Main.LocalUpdateQueue(queueStatus, queuePosition);
            }


            // Threading can f#ck things up, we need to check if Invoking is required.
            if (Globals_Cache.Forms.Queue.InvokeRequired)
            {
                Globals_Cache.Forms.Queue.Invoke(new Action(() =>
                    Globals_Cache.Forms.Queue.LocalUpdateQueue(queueStatus, queuePosition)));
            }
            else
            {
                Globals_Cache.Forms.Queue.LocalUpdateQueue(queueStatus, queuePosition);
            }
        }

        
        public static void PlaySound()
        {
            switch (Globals.Queue.selectedNotifySound)
            {
                case Globals.Queue.E_NotifySounds.none:
                    break;

                case Globals.Queue.E_NotifySounds.gongHit:
                    Media.PlaySoundFromStream(Properties.Resources.SFX_GongHit);
                    break;

                case Globals.Queue.E_NotifySounds.okLetsGo:
                    Media.PlaySoundFromStream(Properties.Resources.SFX_OKLetsGo);
                    break;

                case Globals.Queue.E_NotifySounds.NFLTheme:
                    Media.PlaySoundFromStream(Properties.Resources.SFX_NFLTheme);
                    break;

                case Globals.Queue.E_NotifySounds.gayEcho:
                    Media.PlaySoundFromStream(Properties.Resources.SFX_GayEcho);
                    break;

                case Globals.Queue.E_NotifySounds.rizzMelody:
                    Media.PlaySoundFromStream(Properties.Resources.SFX_Rizz);
                    break;

                case Globals.Queue.E_NotifySounds.runSong:
                    Media.PlaySoundFromStream(Properties.Resources.SFX_Run);
                    break;

                case Globals.Queue.E_NotifySounds.pedroSong:
                    Media.PlaySoundFromStream(Properties.Resources.SFX_PedroSong);
                    break;

                case Globals.Queue.E_NotifySounds.militaryHorn:
                    Media.PlaySoundFromStream(Properties.Resources.SFX_MilitaryHorn);
                    break;
            }
        }
    }
}
