using Avalonia;
using Avalonia.Markup.Xaml;






namespace Cursed_Market
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}