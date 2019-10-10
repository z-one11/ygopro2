/*
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class BackGroundPlayOGV : MonoBehaviour
{
    public MovieTexture movieTexture;
    string bgFilePath;
    Uri fileURI;

    public static BackGroundPlayOGV Instance;
    public BackGroundPlayOGV()
    {
        BackGroundPlayOGV.Instance = this;
    }

    public void LoadOGV(string fileName)
    {
        fileURI = new Uri(new Uri("file:///"), Environment.CurrentDirectory.Replace("\\", "/") + "/" + fileName);
        bgFilePath = fileURI.ToString();

        StartCoroutine(PlayOGV());
    }

    IEnumerator PlayOGV()
    {
        WWW request = new WWW(bgFilePath);
        movieTexture = request.GetMovieTexture();

        while(!movieTexture.isReadyToPlay)
            yield return request;

        BackGroundPic.backGround.GetComponent<UITexture>().mainTexture = movieTexture;
        BackGroundPic.backGround.GetComponent<UITexture>().depth = -100;

        movieTexture.loop = true;
        movieTexture.Play();
    }
}
*/