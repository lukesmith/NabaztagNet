namespace NabaztagNet
{
    public class EarChoreography : Choreography
    {
        public EarChoreography(int after, Ear ear, byte angle, EarDirection direction)
        {
            this.After = after;
            this.Ear = ear;
            this.Angle = angle;
            this.Direction = direction;
        }

        public Ear Ear { get; set; }

        public byte Angle { get; set; }

        public EarDirection Direction { get; set; }

        public override string ToString()
        {
            return string.Format("{0},motor,{1},{2},0,{3}", this.After, (int)this.Ear, this.Angle, (int)this.Direction);
        }
    }
}
