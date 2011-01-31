namespace NabaztagNet
{
    public class LightChoreography : Choreography
    {
        public LightChoreography(int after, NabaztagLed light, byte red, byte green, byte blue)
        {
            this.After = after;
            this.Light = light;
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }

        public NabaztagLed Light { get; set; }

        public byte Red { get; set; }

        public byte Green { get; set; }

        public byte Blue { get; set; }

        public override string ToString()
        {
            return string.Format("{0},led,{1},{2},{3},{4}", this.After, (int)this.Light, this.Red, this.Green, this.Blue);
        }
    }
}
