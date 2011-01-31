namespace NabaztagNet
{
    public class TextToSpeech
    {
        public TextToSpeech(string text, Voice voice, int speed, int pitch)
        {
            this.Voice = voice;
            this.Text = text;
            this.Speed = speed;
            this.Pitch = pitch;
        }

        public string Text { get; set; }

        public Voice Voice { get; set; }

        public int Speed { get; set; }

        public int Pitch { get; set; }
    }
}
