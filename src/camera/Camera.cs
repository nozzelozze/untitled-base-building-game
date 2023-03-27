using System;
using System.Numerics;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class Camera
{
    Vector2i CameraMouseOffset;
    Vector2i OldViewCenter;
    static View View = new View();
    static Vector2f DefaultSize = new Vector2f(1920, 1080);
    Vector2f TargetSize = new Vector2f(1920, 1080);
    float zoomFactor;

    public Camera(View cameraView)
    {
        View = cameraView;
    }

    public static float GetDistanceToCamera(Vector2f position)
    {
        Vector2f distanceVec = position - View.Center;
        return (float)Math.Sqrt(Math.Pow(distanceVec.X, 2) + Math.Pow(distanceVec.Y, 2)); // use built in function
    }

    public static float GetZoomLevel()
    {
        /* returns an value between 0 and 1 */
        return (Math.Clamp(new Vector2(Camera.View.Size.X, Camera.View.Size.Y).Length(), 2000, 9000))/9000;
    }

    public void Scroll(object ? sender, MouseWheelScrollEventArgs e)
    {
        if (e.Delta > 0)
        {
            if (new Vector2(TargetSize.X, TargetSize.Y).Length() > 2000) TargetSize *= 0.8f;
        } else if (e.Delta < 0)
        {
            if (new Vector2(TargetSize.X, TargetSize.Y).Length() < 9000) TargetSize *= 1.2f;
        }
    }

    public static Vector2f WinPositionToCam(Vector2f position)
    {
        float factor = DefaultSize.X / View.Size.X;
        Vector2f newPosition = position/factor;
        newPosition += View.Center - View.Size/2;

        return newPosition;
    }

    public static Vector2f CamPositionToWin(Vector2f position)
    {
        float factor = DefaultSize.X / View.Size.X;
        Vector2f newPosition = position/factor;
        newPosition -= View.Center - View.Size/2;

        return newPosition;
    }

    // chatgpt do not touch
    bool wasRightMouseButtonDown = false;
    Vector2i previousMousePosition;
    public void UpdateCamera(RenderWindow renderWindow)
    {
        zoomFactor = View.Size.X / DefaultSize.X;
        bool isRightMouseButtonDown = Mouse.IsButtonPressed(Mouse.Button.Right);

        if (!wasRightMouseButtonDown && isRightMouseButtonDown)
        {
            previousMousePosition = Mouse.GetPosition(renderWindow);
        }

        Player.Instance.IsPanning = false;

        if (isRightMouseButtonDown)
        {
            Vector2i currentMousePosition = Mouse.GetPosition(renderWindow);
            Vector2f delta = WinPositionToCam((Vector2f)currentMousePosition) - WinPositionToCam((Vector2f)previousMousePosition);

            View.Center -= delta;

            if ((Vector2i)View.Center != OldViewCenter)
            {
                Player.Instance.IsPanning = true;
            }

            previousMousePosition = currentMousePosition;
        }

        View.Size += (TargetSize - View.Size) * .05f;
        renderWindow.SetView(View);

        wasRightMouseButtonDown = isRightMouseButtonDown;
    }
    // chatgpt do not touch

}
