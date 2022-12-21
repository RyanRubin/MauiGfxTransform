using System.ComponentModel;
using Point = System.Drawing.Point;

namespace MauiGfxTransform;

public class MainViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public GraphicsView? GraphicsView { get; set; }

    private float scale = 1;
    public float Scale
    {
        get => scale;
        set
        {
            scale = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Scale)));
        }
    }

    private float cameraX;
    public float CameraX
    {
        get => cameraX;
        set
        {
            cameraX = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CameraX)));
        }
    }

    private float cameraY;
    public float CameraY
    {
        get => cameraY;
        set
        {
            cameraY = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CameraY)));
        }
    }

    public List<Point> Points { get; set; } = new() {
        //new Point{ X = 10, Y = 10 },
        //new Point{ X = 990, Y = 10 },
        //new Point{ X = 10, Y = 490 },
        //new Point{ X = 990, Y = 490 },

        //new Point{ X = 110, Y = 110 },
        //new Point{ X = 890, Y = 110 },
        //new Point{ X = 110, Y = 390 },
        //new Point{ X = 890, Y = 390 },

        new Point{ X = 110, Y = 110 },
        new Point{ X = 290, Y = 110 },
        new Point{ X = 110, Y = 390 },
        new Point{ X = 290, Y = 390 },
    };

    public Command ZoomToFitCommand { get; set; }

    public MainViewModel()
    {
        ZoomToFitCommand = new Command(() => {
            ArgumentNullException.ThrowIfNull(GraphicsView, nameof(GraphicsView));

            float pointsMinX = Points.Min(p => p.X) - MainDrawable.RectSize * 1.5f;
            float pointsMaxX = Points.Max(p => p.X) + MainDrawable.RectSize * 1.5f;
            float pointsMinY = Points.Min(p => p.Y) - MainDrawable.RectSize * 1.5f;
            float pointsMaxY = Points.Max(p => p.Y) + MainDrawable.RectSize * 1.5f;
            float pointsWidth = pointsMaxX - pointsMinX;
            float pointsHeight = pointsMaxY - pointsMinY;
            float pointsAspectRatio = pointsWidth / pointsHeight;

            float gvWidth = (float)GraphicsView.Width;
            float gvHeight = (float)GraphicsView.Height;
            float gvAspectRatio = gvWidth / gvHeight;

            if (pointsAspectRatio >= gvAspectRatio)
            {
                Scale = gvWidth / pointsWidth;

                CameraX = pointsMinX;

                float pointsMidY = pointsMinY + pointsHeight / 2;
                float gvHeightScaled = gvHeight / Scale;
                CameraY = pointsMidY - gvHeightScaled / 2;
            }
            else
            {
                Scale = gvHeight / pointsHeight;

                float pointsMidX = pointsMinX + pointsWidth / 2;
                float gvWidthScaled = gvWidth / Scale;
                CameraX = pointsMidX - gvWidthScaled / 2;

                CameraY = pointsMinY;
            }
        });
    }
}
