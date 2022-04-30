using System.Windows;
using System.Windows.Controls;

namespace VectorWars.Enemies
{
    /// <summary>
    /// Interaction logic for Enemy.xaml
    /// </summary>
    public partial class Enemy : UserControl
    {
        public Enemy()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register(
                name: "Position",
                propertyType: typeof(Point),
                ownerType: typeof(Enemy),
                typeMetadata: new FrameworkPropertyMetadata(
                    defaultValue: new Point(0, 0),
                    flags: FrameworkPropertyMetadataOptions.AffectsRender,
                    propertyChangedCallback: new PropertyChangedCallback(OnPositionChanged)));

        public Point Position
        {
            get => (Point)GetValue(PositionProperty);
            set => SetValue(PositionProperty, value);
        }

        private static void OnPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
