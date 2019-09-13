using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class BackGroundPlay : MonoBehaviour
{
    public MovieTexture movieTexture;

    public static BackGroundPlay Instance;
    string bgFilePath;
    Uri fileURI;

    public BackGroundPlay()
    {
        BackGroundPlay.Instance = this;
    }

    public void LoadOGV(string fileName)
    {
        fileURI = new Uri(new Uri("file:///"), Environment.CurrentDirectory.Replace("\\", "/") + "/" + fileName);
        bgFilePath = fileURI.ToString();

        StartCoroutine(PlayOGV());
    }

    private WWW GetFile(string pathToFile)
    {
        WWW request = new WWW(pathToFile);
        return request;
    }

    IEnumerator PlayOGV()
    {
        WWW request = GetFile(bgFilePath);
        movieTexture = (MovieTexture)request.GetMovieTexture();

        while(!movieTexture.isReadyToPlay)
            yield return request;

        BackGroundPic.backGround.GetComponent<UITexture>().mainTexture = movieTexture;

        movieTexture.loop = true;
        movieTexture.Play();
    }
}