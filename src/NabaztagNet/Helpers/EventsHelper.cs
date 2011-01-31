using System;

namespace NabaztagNet.Helpers
{
    public static class EventsHelper
    {
        public static void DoEvent<TEventArgs>(this EventHandler<TEventArgs> handler, object sender, TEventArgs e)
            where TEventArgs : EventArgs
        {
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        public static void DoEvent(this EventHandler handler, object sender, EventArgs e)
        {
            if (handler != null)
            {
                handler(sender, e);
            }
        }
    }
}