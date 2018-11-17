using AutomaticWebInfoGetterWpfLib.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutomaticWebInfoGetterWpfLib.Views
{
    /// <summary>
    /// Interaction logic for AddSettingView.xaml
    /// </summary>
    ///
    public partial class AddSettingView : UserControl
    {

        public Storyboard openingStoryboard;

        public AddSettingView()
        {
            InitializeComponent();

            //openingStoryboard = new Storyboard();
            //var animation = new ThicknessAnimation();
            //animation.BeginTime = new TimeSpan(0);
            //Storyboard.SetTargetName(animation, XPathAddingGrid.Name);
            //Storyboard.SetTargetProperty(animation, new PropertyPath(Grid.MarginProperty));
            //animation.To = new Thickness(0, 0, 0, 0);
            //animation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            //openingStoryboard.Children.Add(animation);


            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void XPathAddingGrid_LayoutUpdated(object sender, EventArgs e)
        {
            var animation = ((Resources["OpeningAddXPathGridAnimation"] as Storyboard).Children.First()) as ThicknessAnimationUsingKeyFrames;
            Storyboard.SetTargetName(animation, XPathAddingGrid.Name);
            animation.KeyFrames[0].Value = new Thickness(0, -XPathAddingGrid.ActualHeight, 0, 0);

            animation.KeyFrames[1].Value = new Thickness(0, 0, 0, 0);
            animation.Completed += (s, evargs) => XPathAddingGrid.Margin = new Thickness(0, 0, 0, 0);
            //animation.Duration = new Duration(TimeSpan.FromSeconds(1));



            animation = ((Resources["ClosingAddXPathGridAnimation"] as Storyboard).Children.First()) as ThicknessAnimationUsingKeyFrames;
            Storyboard.SetTargetName(animation, XPathAddingGrid.Name);
            animation.KeyFrames[0].Value = new Thickness(0, 0, 0, 0);

            animation.KeyFrames[1].Value = new Thickness(0, -XPathAddingGrid.ActualHeight, 0, 0);
            animation.Completed += (s, evargs) =>
            {
                Binding binding = new Binding();
                binding.Source = XPathAddingGrid.ActualHeight;
                XPathAddingGrid.SetBinding(MarginProperty, binding);
            };

            animation.Completed += (s, evargs) =>
            {
                Binding binding = new Binding("ActualHeightProperty");
                binding.Source = XPathAddingGrid;
                binding.Mode = BindingMode.OneWay;
                binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                binding.Converter = Resources["MarginConverter"] as MarginConverter;
                binding.ConverterParameter = MarginConverterSideOptions.Top;
                XPathAddingGrid.SetBinding(MarginProperty, binding);
            };
            //animation.Completed += (s, evargs) => XPathAddingGrid.Margin = new Thickness(0, -XPathAddingGrid.ActualHeight, 0, 0);
            //animation.Duration = new Duration(TimeSpan.FromSeconds(1));
        }

        private void XPathAddingGrid_LayoutUpdated(object sender, SizeChangedEventArgs e)
        {

        }
    }
}
