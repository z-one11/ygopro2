using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Video;

public class BackGroundPlayMP4 : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    public static BackGroundPlayMP4 Instance;
    public BackGroundPlayMP4()
    {
        BackGroundPlayMP4.Instance = this;
    }

    public void LoadMP4(string fileName)
    {
        Uri fileURI = new Uri(new Uri("file:///"), Environment.CurrentDirectory.Replace("\\", "/") + "/" + fileName);
        string bgFilePath = fileURI.ToString();

        videoPlayer = BackGroundPic.backGround.AddComponent<VideoPlayer>();
        videoPlayer.url = bgFilePath;
        videoPlayer.isLooping = true;
        videoPlayer.Play();

        StartCoroutine(PlayMP4());
    }

    IEnumerator PlayMP4()
    {
        yield return new WaitForSeconds(3f);//延时播放，否则会黑屏

        BackGroundPic.backGround.GetComponent<UITexture>().mainTexture = BackGroundPic.backGround.GetComponent<VideoPlayer>().texture;
        BackGroundPic.backGround.GetComponent<UITexture>().depth = -100;
    }

}