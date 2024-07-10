namespace Radio.Model
{
    public class RadioModel
    {
        #region // Constructors
        public RadioModel() { }
        public RadioModel(double startingVolume, double startingFrequency)
        {
            this.Volume = startingVolume;
            this.Frequency = startingFrequency;
        }
        #endregion

        #region // Fields
        public bool IsTurnedOn = false;
        public double Volume = default;
        public double Frequency = default;
        #endregion

        #region // Methods
        /// <summary>
        /// Main function of the RadioModel, playing music based on its {Volume} and {Frequency} 
        /// while it {IsTurnedOn} and the {Volume} is > 0.
        /// </summary>
        public void TryPlayMusic()
        {
            if (IsTurnedOn && Volume < 0d)
            {
                // Radio would play music with {Volume} on {Frequency}.
            }
        }
        #endregion
    }
}
