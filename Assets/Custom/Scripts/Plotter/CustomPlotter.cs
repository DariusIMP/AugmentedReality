using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Custom.Scripts.Plotter
{
    public class CustomPlotter : MonoBehaviour
    {
        const int Horizontaldivs = 10;
        const int Verticaldivs = 10;

        private Image PlotImage;

        private Texture2D PlotTexture;
        private Vector2 TextureResolution = new Vector2(500, 500);

        public List<Vector2> _dots;
        public float minX;
        public float maxX;
        public float minY;
        public float maxY;
        
        public int dotsAmount;
        
        // Start is called before the first frame update
        public void Start()
        {
            PlotImage = GetComponent<Image>();
            if (PlotImage == null) throw new Exception("Simplest plot needs an image component in the same GameObject in order to work.");

            SetResolution();
            
            _dots = new List<Vector2>();
            //Test();
            TestDraw2();
        }

        public Vector2 AdjustCoordinateToImageSize(float x, float fx)
        {
            float newX = x / maxX * (TextureResolution.x / 2);
            float newFx = fx / maxY * (TextureResolution.y / 2);
            return new Vector2(newX, newFx);
        }
        
        public void SetDots(Func<float, float> func)
        {
            float stepSize = (maxX - minX) / dotsAmount;
            float x = minX;
            for (int i = 0; i <= dotsAmount; i++)
            {
                var fx = func(x);
                _dots.Add(AdjustCoordinateToImageSize(x, fx));
                x += stepSize;
            }
        }

        public float Sin(float x)
        {
            return 10 * (float)Math.Sin(x);
        }

        public void Test()
        {
            minX = -10;
            maxX = 10;
            
            SetDots(Sin);
            for (int i = 0; i < _dots.Count; i++)
            {
                Debug.Log("Vector : " + _dots[i].x + "-" + _dots[i].y);
            }
        }

        public void TestDraw()
        {
            DrawLine(PlotTexture, new Vector2(-5,-5), new Vector2(5, 5), Color.red);
            PlotTexture.Apply();
        }

        public void TestDraw2()
        {
            SetDots(Sin);
            for (int i = 1; i < _dots.Count; i++)
            {
                Vector2 start = _dots[i - 1];
                Vector2 end = _dots[i];
                Debug.Log("start: " + start.x + " : " + start.y + " | end: " + end.x + " : " + end.y);
                DrawLine(PlotTexture, start, end, Color.red);
            }
            PlotTexture.Apply();
        }
        
        public void SetResolution()
        {
            PlotTexture = new Texture2D((int)TextureResolution.x, (int)TextureResolution.y);
            Rect rect = new Rect(0, 0, TextureResolution.x, TextureResolution.y);
            PlotImage.GetComponent<Image>().sprite = Sprite.Create(PlotTexture, rect, new Vector2(0.0f, 0.0f));
        }
        
        private void DrawLine(Texture2D PlotTexture, Vector2 start, Vector2 end, Color LineColor)
        {
            Debug.Log("DrawLine");

            start += new Vector2(TextureResolution.x / 2, TextureResolution.y / 2);
            end += new Vector2(TextureResolution.x / 2, TextureResolution.y / 2);
            
            int x0 = (int) start.x;
            int y0 = (int) start.y;
            int x1 = (int) end.x;
            int y1 = (int) end.y;
            
            int dx = 0;
            int dy = 0;
            int sx = 0;
            int sy = 0;
            dx = Math.Abs(x1 - x0);
            dy = Math.Abs(y1 - y0);
            sx = x0 < x1 ? 1 : -1;
            sy = y0 < y1 ? 1 : -1;
            int err = (dx > dy ? dx : -dy) / 2, e2;

            while (true)
            {
                if (!IsOutOfBoundaries(x0, y0))
                {
                    PlotTexture.SetPixel(x0, y0, LineColor);
                }
                if (x0 == x1 && y0 == y1) break;
                e2 = err;
                if (e2 > -dx) { err -= dy; x0 += sx; }
                if (e2 < dy) { err += dx; y0 += sy; }
            }
        }

        private bool IsOutOfBoundaries(int x, int y)
        {
            return x < 0 || x > TextureResolution.x || y < 0 || y > TextureResolution.y;
        }
        
    }
}
