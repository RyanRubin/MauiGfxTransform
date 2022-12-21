namespace MauiGfxTransform;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        var vm = new MainViewModel();
        vm.GraphicsView = graphicsView;
        vm.PropertyChanged += (sender, e) => {
            graphicsView.Invalidate();
        };
        BindingContext = vm;

        var drawable = new MainDrawable
        {
            Vm = vm
        };
        graphicsView.Drawable = drawable;
    }
}
