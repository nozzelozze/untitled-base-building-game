using System;
using System.Numerics;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

public class Camera
{
    Vector2i cameraMouseOffset;
    static View view = new View();
    static Vector2f defaultSize = new Vector2f(1920, 1080);
    Vector2f targetSize = new Vector2f(1920, 1080);

    public Camera(View cameraView)
    {
        view = cameraView;
    }

    public static float getDistanceToCamera(Vector2f position)
    {
        Vector2f distanceVec = position - view.Center;
        return (float)Math.Sqrt(Math.Pow(distanceVec.X, 2) + Math.Pow(distanceVec.Y, 2)); // use built in function
    }

    public static float getZoomLevel()
    {
        /* returns an value between 0 and 1 */
        return (Math.Clamp(new Vector2(Camera.view.Size.X, Camera.view.Size.Y).Length(), 2000, 9000))/9000;
    }

    public void scroll(object ? sender, MouseWheelScrollEventArgs e)
    {
        if (e.Delta > 0)
        {
            if (new Vector2(targetSize.X, targetSize.Y).Length() > 2000) targetSize *= 0.8f;
        } else if (e.Delta < 0)
        {
            if (new Vector2(targetSize.X, targetSize.Y).Length() < 9000) targetSize *= 1.2f;
        }
    }

    public static Vector2f winPositionToCam(Vector2f position)
    {
        float factor = defaultSize.X / view.Size.X;
        Vector2f newPosition = position/factor;
        newPosition += view.Center - view.Size/2;

        return newPosition;
    }

    public static Vector2f camPositionToWin(Vector2f position)
    {
        float factor = defaultSize.X / view.Size.X;
        Vector2f newPosition = position/factor;
        newPosition -= view.Center - view.Size/2;

        return newPosition;
    }

    public void updateCamera(RenderWindow renderWindow)
    {
        renderWindow.SetView(view);
        if (Input.events.Contains(Mouse.Button.Right)) cameraMouseOffset = -Mouse.GetPosition(renderWindow) - (Vector2i)view.Center;
        if (Mouse.IsButtonPressed(Mouse.Button.Right))
        {
            Vector2 zoomWeight;
            float zoomWeightLength;
            zoomWeight.X = defaultSize.X / targetSize.X;
            zoomWeight.Y = defaultSize.Y / targetSize.Y;
            zoomWeightLength = zoomWeight.Length();
            
            view.Center = -(Vector2f)Mouse.GetPosition(renderWindow) - (Vector2f)cameraMouseOffset;
        }
        view.Size += (targetSize - view.Size) * .05f;
        renderWindow.SetView(view);
    }
}