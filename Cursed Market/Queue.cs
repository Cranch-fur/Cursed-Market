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
            E_QueueStatus currentStatus = queueStatus;
            int currentPosition = queuePosition;

            /* Dispatch UI updates through Main form thread to avoid cross-thread access on unshown forms. */
            if (ProgramCache.Forms.Main != null && !ProgramCache.Forms.Main.IsDisposed && ProgramCache.Forms.Main.IsHandleCreated)
            {
                if (ProgramCache.Forms.Main.InvokeRequired)
                {
                    ProgramCache.Forms.Main.Invoke(new Action(() =>
                    {
                        ProgramCache.Forms.Main.LocalUpdateQueue(currentStatus, currentPosition);
                        if (ProgramCache.Forms.Queue != null && !ProgramCache.Forms.Queue.IsDisposed)
                        {
                            ProgramCache.Forms.Queue.LocalUpdateQueue(currentStatus, currentPosition);
                        }
                    }));
                }
                else
                {
                    ProgramCache.Forms.Main.LocalUpdateQueue(currentStatus, currentPosition);
                    if (ProgramCache.Forms.Queue != null && !ProgramCache.Forms.Queue.IsDisposed)
                    {
                        ProgramCache.Forms.Queue.LocalUpdateQueue(currentStatus, currentPosition);
                    }
                }
            }
        }


        public static void PlaySound()
        {
            switch (ProgramFeatures.QueueNotify.GetSelectedNotifySound())
            {
                case ProgramFeatures.QueueNotify.E_NotifySounds.none:
                    break;

                case ProgramFeatures.QueueNotify.E_NotifySounds.gongHit:
                    Media.PlaySoundFromStream(Properties.Resources.SFX_GongHit);
                    break;

                case ProgramFeatures.QueueNotify.E_NotifySounds.okLetsGo:
                    Media.PlaySoundFromStream(Properties.Resources.SFX_OKLetsGo);
                    break;

                case ProgramFeatures.QueueNotify.E_NotifySounds.NFLTheme:
                    Media.PlaySoundFromStream(Properties.Resources.SFX_NFLTheme);
                    break;

                case ProgramFeatures.QueueNotify.E_NotifySounds.gayEcho:
                    Media.PlaySoundFromStream(Properties.Resources.SFX_GayEcho);
                    break;

                case ProgramFeatures.QueueNotify.E_NotifySounds.rizzMelody:
                    Media.PlaySoundFromStream(Properties.Resources.SFX_Rizz);
                    break;

                case ProgramFeatures.QueueNotify.E_NotifySounds.runSong:
                    Media.PlaySoundFromStream(Properties.Resources.SFX_Run);
                    break;

                case ProgramFeatures.QueueNotify.E_NotifySounds.pedroSong:
                    Media.PlaySoundFromStream(Properties.Resources.SFX_PedroSong);
                    break;

                case ProgramFeatures.QueueNotify.E_NotifySounds.militaryHorn:
                    Media.PlaySoundFromStream(Properties.Resources.SFX_MilitaryHorn);
                    break;
            }
        }
        public static void StopSound()
        {
            Media.StopSound();
        }
    }
}