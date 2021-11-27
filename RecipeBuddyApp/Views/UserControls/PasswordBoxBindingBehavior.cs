using System.Linq;
using System.Reflection;
using System.Security;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace RecipeBuddy.Views
{
    public class PasswordBoxBindingBehavior : Microsoft.Xaml.Interactivity.Behavior<PasswordBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.PasswordChanged += OnPasswordBoxValueChanged;
        }

        public SecureString Password
        {
            get { return (SecureString)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        //public static readonly DependencyProperty PasswordProperty =
        //    DependencyProperty.Register("Password", typeof(SecureString), typeof(PasswordBoxBindingBehavior), new PropertyMetadata(OnSourcePropertyChanged));

        public static readonly DependencyProperty PasswordProperty =
           DependencyProperty.Register("Password", typeof(SecureString), typeof(PasswordBoxBindingBehavior), null);

        private static void OnSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
            {
                PasswordBoxBindingBehavior behavior = d as PasswordBoxBindingBehavior;
                behavior.AssociatedObject.PasswordChanged -= OnPasswordBoxValueChanged;
                behavior.AssociatedObject.Password = string.Empty;
                behavior.AssociatedObject.PasswordChanged += OnPasswordBoxValueChanged;
            }
        }

        private static void OnPasswordBoxValueChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            var behavior = Microsoft.Xaml.Interactivity.Interaction.GetBehaviors(passwordBox).OfType<PasswordBoxBindingBehavior>().FirstOrDefault();
            if (behavior != null)
            {
                ////var binding = BindingOperations.GetBindingExpression(behavior, PasswordProperty);
                //var binding = BindingOperations.GetBindingExpression(behavior, PasswordProperty);
                //if (binding != null)
                //{
                //    PropertyInfo property = binding.DataItem.GetType().GetProperty(binding.ParentBinding.Path.Path);
                //    if (property != null)
                //        property.SetValue(binding.DataItem, passwordBox.SecurePassword, null);
                //}
            }
        }
    }
}
