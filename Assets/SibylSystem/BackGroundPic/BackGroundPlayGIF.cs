using UnityEngine;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

public class BackGroundPlayGIF : MonoBehaviour
{
    public float Mytime = 0.05f;
    List<Texture2D> bg;
    int dex = 0;
    float time;
    public int x, y;

    public static BackGroundPlayGIF Instance;
    public BackGroundPlayGIF()
    {
        BackGroundPlayGIF.Instance = this;
    }

    public void LoadGIF(string fileName)
    {
        Bitmap bitmap = (Bitmap)System.Drawing.Image.FromFile(fileName);
        bg = GifToTexture(bitmap);
    }

    void Update()
    {
        BackGroundPic.backGround.GetComponent<UITexture>().mainTexture = bg[dex];
        BackGroundPic.backGround.GetComponent<UITexture>().depth = -100;

        time += Time.deltaTime;
        if (time > Mytime)
        {
            dex++;
            if (dex == bg.Count)
            {
                dex = 0;
            }
            time = 0;
        }
    }

    List<Texture2D> GifToTexture(Bitmap bitmap)
    {
        List<Texture2D> texture2D = null;
        if (bitmap != null)
        {
            texture2D = new List<Texture2D>();
            FrameDimension frameDimension = new FrameDimension(bitmap.FrameDimensionsList[0]);
            int framCount = bitmap.GetFrameCount(frameDimension);
            for (int i = 0; i < framCount; i++)
            {
                bitmap.SelectActiveFrame(frameDimension, i);
                var framBitmap = new Bitmap(bitmap.Width, bitmap.Height);
                System.Drawing.Graphics.FromImage(framBitmap).DrawImage(bitmap, Point.Empty);
                var frameTexture2D = new Texture2D(framBitmap.Width, framBitmap.Height);
                for (int x = 0; x < framBitmap.Width; x++)
                {
                    for (int y = 0; y < framBitmap.Height; y++)
                    {
                        System.Drawing.Color sourceColor = framBitmap.GetPixel(x, y);
                        frameTexture2D.SetPixel(x, framBitmap.Height - 1 - y, new Color32(sourceColor.R, sourceColor.G, sourceColor.B, sourceColor.A));
                    }
                }
                frameTexture2D.Apply();
                texture2D.Add(frameTexture2D);
            }
        }
        return texture2D;
    }

}
