using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Radio.View
{
    /// <summary>
    /// Interaction logic for Dial.xaml
    /// </summary>
    public partial class DialView : UserControl, INotifyPropertyChanged
    {
        public DialView()
        {
            InitializeComponent();
        }

        #region // Fields
        private Vector PreviousCursorPoint = default;

        // Angle for Dial Animation
        private double m_Angle = default;
        private readonly double m_MaxRotationAnglePerEvent = 10;

        // Amount for Animation driven by Dial
        private double m_AnimationAmount = default;
        #endregion

        #region // Properties
        /// <summary>
        /// Value must be bound with Mode=TwoWay to ensure proper update in view
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
                "Value",
                typeof(double),
                typeof(DialView));

        /// <summary>
        /// AnimationAmount uses INotifyPropertyChanged for driving animation of other controls
        /// </summary>
        public double AnimationAmount
        {
            get => m_AnimationAmount;
            set => SetProperty(ref m_AnimationAmount, value);
        }
        public double MaxAnimationAmount{ get; set; }

        public double MinAnimationAmount{ get; set; }

        /// <summary>
        /// Angle uses INotifyPropertyChanged to visibly turn the dial
        /// </summary>
        public double Angle
        {
            get => m_Angle;
            set => SetProperty(ref m_Angle, value);
        }

        public double MaxAngle { get; set; }

        public double MinAngle { get; set; }
        /// <summary>
        /// Value binds to the different RadioViewModel Properties
        /// </summary>
        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public double MaxValue { get; set; }

        public double MinValue { get; set; }
        #endregion

        #region // Eventhandler
        /// <summary>
        /// Turns the dial, while also updating value and animation driver based on turned angle.
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">The parameters of the MouseEvent</param>
        private void TurnAndUpdateDialValues(object sender, MouseEventArgs e)
        {
            if (!(Mouse.LeftButton == MouseButtonState.Pressed))
            {
                return;
            }
            Vector CursorPoint = (Vector)e.GetPosition(this) - new Vector(100d, 100d);
            double NewAngle = this.Angle + Vector.AngleBetween(PreviousCursorPoint, CursorPoint);

            if (IsLocked(NewAngle))
            {
                return;
            }

            this.Angle = NewAngle;
            this.AnimationAmount = ConvertFromAngleRangeToRange(NewAngle, MinAnimationAmount, MaxAnimationAmount);
            this.Value = ConvertFromAngleRangeToRange(NewAngle, MinValue, MaxValue);
            this.PreviousCursorPoint = CursorPoint;

        }

        private void SetupForTurn(object sender, MouseButtonEventArgs e)
        {
            this.PreviousCursorPoint = (Vector)e.GetPosition(this) - new Vector(100d, 100d);
        }
        #endregion

        #region // Private Helper Functions
        /// <summary>
        /// Converts the angle of the dial to a value maintaining the ratio from the dials angle range to a specified range. 
        /// </summary>
        /// <param name="angle">The angle to be converted</param>
        /// <param name="toMin">The minimum of the new range</param>
        /// <param name="toMax">The maximum of the new range</param>
        /// <returns></returns>
        private double ConvertFromAngleRangeToRange(double angle, double toMin, double toMax)
        {
            return ((angle - this.MinAngle) / (this.MaxAngle - this.MinAngle)) * (toMax - toMin) + toMin;
        }

        /// <summary>
        /// Dial should only turn when the next Value is within the predefined range of the dial and
        /// when it can rotate smoothly. 
        /// </summary>
        /// <returns>True, if the dial is locked.</returns>
        private bool IsLocked(double angle)
        {
            double NextValue = ConvertFromAngleRangeToRange(angle, MinValue, MaxValue);

            return NextValue > this.MaxValue ||
                   NextValue < this.MinValue ||
                   angle > this.Angle + m_MaxRotationAnglePerEvent ||
                   angle < this.Angle - m_MaxRotationAnglePerEvent;
        }
        #endregion

        #region // INotifyPropertyChanged
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
