using Radio.Model;
using System.ComponentModel;

namespace Radio.ViewModel
{
    public class RadioViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// RadioModel used by RadioViewModel to save data
        /// </summary>
        private readonly RadioModel m_RadioModel;

        /// <summary>
        /// Properties which can be bound to by Views using a RadioViewModel as DataContext
        /// </summary>
        #region // Properties
        public bool IsTurnedOn
        {
            get => m_RadioModel.IsTurnedOn;
            set => SetProperty(ref m_RadioModel.IsTurnedOn, value);
        }

        public double Volume
        {
            get => m_RadioModel.Volume;
            set => SetProperty(ref m_RadioModel.Volume, value);
        }

        public double Frequency
        {
            get => m_RadioModel.Frequency;
            set => SetProperty(ref m_RadioModel.Frequency, value);
        }
        #endregion

        /// <summary>
        /// Constructors of RadioViewModel
        /// </summary>
        #region // Constructors
        public RadioViewModel()
        {
            this.m_RadioModel = new RadioModel(50d, 87.5d);
        }

        public RadioViewModel(RadioModel radioModel)
        {
            this.m_RadioModel = radioModel;
        }
        #endregion

        #region // INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler? PropertyChanged;

        public void SetProperty<T>(ref T storage, T value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            if (!object.Equals(storage, value) && PropertyChanged != null)
            {
                storage = value;
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
