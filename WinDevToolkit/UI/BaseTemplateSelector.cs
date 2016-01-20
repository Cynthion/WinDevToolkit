using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WinDevToolkit.UI
{
    /// <summary>
    /// Base class for all template selectors. Unwraps the necessary information such that subclasses
    /// only need to deal with the type T by implementing <see cref="DoSelectTemplate"/>.
    /// </summary>
    /// <typeparam name="T">The type containing the necessary information for the template selection.</typeparam>
    public abstract class BaseTemplateSelector<T> : DataTemplateSelector where T : class
    {
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var element = container as FrameworkElement;
            if (element != null && item is T)
            {
                return DoSelectTemplate((T)item);
            }
            return null;
        }

        protected abstract DataTemplate DoSelectTemplate(T item);
    }
}
