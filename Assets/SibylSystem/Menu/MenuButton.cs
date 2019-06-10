using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public Canvas buttonCanvas;
    public CanvasGroup buttonCanvasGroup;
    public GameObject button;
    // Use this for initialization
    void Start()
    {
        buttonCanvas = GetComponent<Canvas>();
        buttonCanvasGroup = GetComponent<CanvasGroup>();
        button = GameObject.Find("Button");
    }

    public void onClickJoinQQ()
    {
#if !UNITY_EDITOR && UNITY_ANDROID //Android
        AndroidJavaObject jo = new AndroidJavaObject("cn.unicorn369.library.API");
        jo.Call("doJoinQQGroup", "UHm3h3hSrmgp-iYqMiZcc2zO5J1Q8OyW");
#else
        Application.OpenURL("https://jq.qq.com/?_wv=1027&k=50MZVQA");
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if (Program.I().menu!=null && Program.I().setting !=null && Program.I().menu.isShowed && !Program.I().setting.isShowed)
        {
            buttonCanvasGroup.alpha = 1f;
            button.SetActive(true);
        }
        else
        {
            buttonCanvasGroup.alpha = 0f;
            button.SetActive(false);
        }
    }
}
