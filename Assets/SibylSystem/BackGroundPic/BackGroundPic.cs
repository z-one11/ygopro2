using UnityEngine;
using System;
using System.IO;
public class BackGroundPic : Servant
{
    public static GameObject backGround;
    public override void initialize()
    {
        backGround = create(Program.I().mod_simple_ngui_background_texture, Vector3.zero, Vector3.zero, false, Program.ui_back_ground_2d);
        string fileName = "textures/bg";
        LoadPic();
        if (File.Exists(fileName + ".mp4"))
        {
            fileName += ".mp4";
            backGround.AddComponent<BackGroundPlayMP4>();
            BackGroundPlayMP4.Instance.LoadMP4(fileName);
        }
        /*else if (File.Exists(fileName + ".ogv"))
        {
            //不支持移动平台 (原因：MovieTexture)
            fileName += ".ogv";
            backGround.AddComponent<BackGroundPlayOGV>();
            BackGroundPlayOGV.Instance.LoadOGV(fileName);
        }*/
        else if (File.Exists(fileName + ".gif") && Program.ANDROID_API_N)
        {
            //移动平台只支持：Android 7.0+ (原因：libgdiplus.so 使用 Termux 编译生成)
            fileName += ".gif";
            backGround.AddComponent<BackGroundPlayGIF>();
            BackGroundPlayGIF.Instance.LoadGIF(fileName);
        }
        else if (File.Exists(fileName + ".png"))
        {
            fileName += ".png";
            LoadJpgOrPng(fileName);
        }
        else if (File.Exists(fileName + ".jpg"))
        {
            fileName += ".jpg";
            LoadJpgOrPng(fileName);
        }
    }

    public void LoadPic()
    {
        Texture2D pic = (Texture2D)Resources.Load("bg_menu");
        backGround.GetComponent<UITexture>().mainTexture = pic;
    }

    public void LoadJpgOrPng(string fileName)
    {
        FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        file.Seek(0, SeekOrigin.Begin);
        byte[] data = new byte[file.Length];
        file.Read(data, 0, (int)file.Length);
        file.Close();
        file.Dispose();
        file = null;
        Texture2D pic = new Texture2D(1024, 600);
        pic.LoadImage(data);
        backGround.GetComponent<UITexture>().mainTexture = pic;
        backGround.GetComponent<UITexture>().depth = -100;
    }

    public override void applyShowArrangement()
    {
        UIRoot root = Program.ui_back_ground_2d.GetComponent<UIRoot>();
        float s = root.activeHeight / Screen.height;
        var tex = backGround.GetComponent<UITexture>().mainTexture;
        float ss = (float)tex.height / (float)tex.width;
        int width = (int)(Screen.width * s);
        int height = (int)(width * ss);
        if (height < Screen.height)
        {
            height = (int)(Screen.height * s);
            width = (int)(height / ss);
        }
        backGround.GetComponent<UITexture>().height = height+2;
        backGround.GetComponent<UITexture>().width = width+2;
    }

    public override void applyHideArrangement()
    {
        applyShowArrangement();
    }
}
