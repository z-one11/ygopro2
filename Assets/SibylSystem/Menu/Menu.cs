﻿using UnityEngine;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Collections.Generic;

public class Menu : WindowServantSP 
{
    //GameObject screen;
    public override void initialize()
    {
        string hint = File.ReadAllText("config/hint.conf");
        createWindow(Program.I().new_ui_menu);
        UIHelper.registEvent(gameObject, "setting_", onClickSetting);
        UIHelper.registEvent(gameObject, "deck_", onClickSelectDeck);
        UIHelper.registEvent(gameObject, "online_", onClickOnline);
        UIHelper.registEvent(gameObject, "replay_", onClickReplay);
        UIHelper.registEvent(gameObject, "single_", Program.gugugu);
        UIHelper.registEvent(gameObject, "ai_", Program.gugugu);
        UIHelper.registEvent(gameObject, "exit_", onClickExit);
        UIHelper.getByName<UILabel>(gameObject, "version_").text = "YGOPro2 " + Program.GAME_VERSION + " (Test)";
        (new Thread(up)).Start();
    }

    public override void show()
    {
        base.show();
        Program.charge();
    }

    public override void hide()
    {
        base.hide();
    }

    static int Version = 0;
    string url = "http://api.ygo2020.xyz/ygopro2/ver.txt";
    string upurl = "";
    string VERSION = "";
    void up()
    {
        try
        {
            CheckUpgrade();
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.Log(e);
        }
    }

    void CheckUpgrade()
    {
        ServicePointManager.ServerCertificateValidationCallback = HttpDldFile.MyRemoteCertificateValidationCallback;//支持https
        WebClient wc = new WebClient();
        Stream s = wc.OpenRead(url);
        StreamReader sr = new StreamReader(s, Encoding.UTF8);
        string result = sr.ReadToEnd();
        sr.Close();
        s.Close();
        string[] lines = result.Replace("\r", "").Split("\n");
        VERSION = lines[0];
        if (lines[0] != Program.GAME_VERSION)
        {
            upurl = lines[1];
        }
    }

    public override void ES_RMS(string hashCode, List<messageSystemValue> result)
    {
        base.ES_RMS(hashCode, result);
        if (hashCode == "RMSshow_onlyYes")
        {
            if (result[0].value == "yes")
            {
                Application.OpenURL(upurl);
            }
        }
        if (hashCode == "RMSshow_menu")
        {
            if (result[0].value == "left")
            {
                onJoinQQ();
            }
            if (result[0].value == "right")
            {
                onCheckUpgrade();
            }
        }
    }

    bool outed = false;
    public override void preFrameFunction()
    {
        base.preFrameFunction();
        if (upurl != "" && outed == false)
        {
            outed = true;
            RMSshow_yesOrNo("RMSshow_onlyYes", InterString.Get("发现更新!@n是否要下载更新？"), new messageSystemValue { hint = "yes", value = "yes" }, new messageSystemValue { hint = "no", value = "no" });
        }
        Menu.checkCommend();
    }

    void onClickExit()
    {
        Program.I().quit();
        Program.Running = false;
        TcpHelper.SaveRecord();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Process.GetCurrentProcess().Kill();
#endif
    }

    void onClickOnline()
    {
        Program.I().shiftToServant(Program.I().selectServer);
    }

    void onClickAI()
    {
        Program.I().shiftToServant(Program.I().aiRoom);
    }

    void onClickPizzle()
    {
        Program.I().shiftToServant(Program.I().puzzleMode);
    }

    void onClickReplay()
    {
        Program.I().shiftToServant(Program.I().selectReplay);
    }

    void onClickSetting()
    {
        Program.I().setting.show();
    }

    void onClickSelectDeck()
    {
        Program.I().shiftToServant(Program.I().selectDeck);
    }

    void onJoinQQ()
    {
        Application.OpenURL("https://jq.qq.com/?_wv=1027&k=5YrX11n");
    }

    void onCheckUpgrade()
    {
        Program.PrintToChat(InterString.Get("正在检测是否有新版本！"));
        try
        {
            CheckUpgrade();
            if (VERSION != Program.GAME_VERSION)
            {
                RMSshow_yesOrNo
                (
                    "RMSshow_onlyYes",
                    InterString.Get("发现新版本，是否立即下载？"),
                    new messageSystemValue { hint = "yes", value = "yes" },
                    new messageSystemValue { hint = "no", value = "no" }
                );
            }
            else
            {
                showToast("已是最新版本！");
            }
        }
        catch (System.Exception e)
        {
            showToast("检查更新失败！");
        }
    }

    public void onMenu()
    {
        RMSshow_menu
        (
            "RMSshow_menu",
            new messageSystemValue { hint = "left", value = "left" },
            new messageSystemValue { hint = "right", value = "right" }
        );
    }

    public void showToast(string content)
    {
        RMSshow_onlyYes("showToast", InterString.Get(content), null);
    }

    public static void deleteShell()
    {
        try
        {
            if (File.Exists("commamd.shell") == true)
            {
                File.Delete("commamd.shell");
            }
        }
        catch (Exception)
        {
        }
    }

    static int lastTime = 0;
    public static void checkCommend()
    {
        if (Program.TimePassed() - lastTime > 1000)
        {
            lastTime = Program.TimePassed();
            if (Program.I().selectDeck == null)
            {
                return;
            }
            if (Program.I().selectReplay == null)
            {
                return;
            }
            if (Program.I().puzzleMode == null)
            {
                return;
            }
            if (Program.I().selectServer == null)
            {
                return;
            }
            try
            {
                if (File.Exists("commamd.shell") == false)
                {
                    File.Create("commamd.shell").Close();
                }
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.Log(e);
            }
            string all = "";
            try
            {
                all = File.ReadAllText("commamd.shell",Encoding.UTF8);
                string[] mats = all.Split(" ");
                if (mats.Length > 0)
                {
                    switch (mats[0])
                    {
                        case "online":
                            if (mats.Length == 5)
                            {
                                UIHelper.iniFaces();//加载用户头像
                                Program.I().selectServer.KF_onlineGame(mats[1], mats[2], mats[3], mats[4]);
                            }
                            if (mats.Length == 6)
                            {
                                UIHelper.iniFaces();
                                Program.I().selectServer.KF_onlineGame(mats[1], mats[2], mats[3], mats[4], mats[5]);
                            }
                            break;
                        case "edit":
                            if (mats.Length == 2)
                            {
                                Program.I().selectDeck.KF_editDeck(mats[1]);//编辑卡组
                            }
                            break;
                        case "replay":
                            if (mats.Length == 2)
                            {
                                UIHelper.iniFaces();
                                Program.I().selectReplay.KF_replay(mats[1]);//编辑录像
                            }
                            break;
                        case "puzzle":
                            if (mats.Length == 2)
                            {
                                UIHelper.iniFaces();
                                Program.I().puzzleMode.KF_puzzle(mats[1]);//运行残局
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.Log(e);
            }
            try
            {
                if (all != "")
                {
                    if (File.Exists("commamd.shell") == true)
                    {
                        File.WriteAllText("commamd.shell", "");
                    }
                }
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.Log(e);
            }
        }
    }
}
