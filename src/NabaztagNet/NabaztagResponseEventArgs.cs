using System;

namespace NabaztagNet
{
    public class NabaztagResponseEventArgs : EventArgs
    {
        private readonly EarPosition? leftEar;
        private readonly EarPosition? rightEar;
        private readonly string message;
        private readonly bool notAvailable;

        public NabaztagResponseEventArgs(string message)
            : this(message, null, null)
        {
        }

        public NabaztagResponseEventArgs(string message, bool notAvailable)
        {
            this.message = message;
            this.notAvailable = notAvailable;
        }

        public NabaztagResponseEventArgs(string message, EarPosition? leftEar, EarPosition? rightEar)
        {
            this.message = message;
            this.leftEar = leftEar;
            this.rightEar = rightEar;
        }
        
        public EarPosition? LeftEar
        {
            get { return this.leftEar; }
        }
        
        public EarPosition? RightEar
        {
            get { return this.rightEar; }
        }

        public string Message
        {
            get { return this.message; }
        }

        public bool NotAvailable
        {
            get { return this.notAvailable; }
        }
    }
}
