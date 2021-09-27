using System;

namespace Voice.Picking.Test.Pages.Extensions
{
    public static class TapLock
    {
        private static object tapLockObject = new object();
        private static bool tapLockAdquirido = false;
        private static int tapLockObjectHash = -1;
        private static DateTime tapLockExpires = DateTime.Now;
        private static TimeSpan tapLockExpiresTime = TimeSpan.FromSeconds(2);

        public static bool Adquirir(object objetoOrigem)
        {
            lock (tapLockObject)
            {
                int objetoOrigemHash = objetoOrigem?.GetHashCode() ?? 0;

                if (tapLockAdquirido && tapLockExpires > DateTime.Now && tapLockObjectHash == objetoOrigemHash)
                    return false;

                tapLockAdquirido = true;
                tapLockObjectHash = objetoOrigemHash;
                tapLockExpires = DateTime.Now.Add(tapLockExpiresTime);
                return true;
            }
        }

        public static void Liberar()
        {
            tapLockAdquirido = false;
            tapLockObjectHash = -1;
        }
    }
}
