namespace MauiGfxTransform;

public class MainDrawable : IDrawable
{
    public const float RectSize = 20;

    public MainViewModel? Vm { get; set; }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        if (Vm == null)
        {
            return;
        }

        canvas.ResetState();

        canvas.Scale(Vm.Scale, Vm.Scale);
        canvas.Translate(-Vm.CameraX, -Vm.CameraY);

        foreach (var point in Vm.Points)
        {
            canvas.DrawRectangle(point.X - RectSize / 2, point.Y - RectSize / 2, RectSize, RectSize);
        }
    }
}
