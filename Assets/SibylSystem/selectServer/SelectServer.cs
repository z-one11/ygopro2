﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

public class SelectServer : WindowServantSP
{
    UIPopupList list;
    UIPopupList serversList;

    UIInput inputIP;
    UIInput inputPort;
    UIInput inputPsw;
    UIInput inputVersion;

    UISprite inputIP_;
    UISprite inputPort_;

    public UITexture face;

    public override void initialize()
    {
        createWindow(Program.I().new_ui_selectServer);
        UIHelper.registEvent(gameObject, "exit_", onClickExit);
        UIHelper.registEvent(gameObject, "face_", onClickFace);
        UIHelper.registEvent(gameObject, "join_", onClickJoin);
        UIHelper.registEvent(gameObject, "clearPsw_", onClearPsw);
        serversList = UIHelper.getByName<UIPopupList>(gameObject, "server");
        //serversList.fontSize = 30;
        UIHelper.registEvent(gameObject, "server", pickServer);
        UIHelper.getByName<UIInput>(gameObject, "name_").value = Config.Get("name", "YGOPro2 User");
        list = UIHelper.getByName<UIPopupList>(gameObject, "history_");
        UIHelper.registEvent(gameObject, "history_", onSelected);
        name = Config.Get("name", "YGOPro2 User");
        inputIP = UIHelper.getByName<UIInput>(gameObject, "ip_");
        inputPort = UIHelper.getByName<UIInput>(gameObject, "port_");
        inputPsw = UIHelper.getByName<UIInput>(gameObject, "psw_");
        inputIP_ = UIHelper.getByName<UISprite>(gameObject, "ip_");
        inputPort_ = UIHelper.getByName<UISprite>(gameObject, "port_");
        inputVersion = UIHelper.getByName<UIInput>(gameObject, "version_");
        set_version("0x" + String.Format("{0:X}", Config.ClientVersion));

        inputIP.value = Config.Get("ip_", "s1.ygo233.com");
        inputPort.value = Config.Get("port_", "233");
        serversList.value = Config.Get("serversPicker", "[OCG]233 Server");

        face = UIHelper.getByName<UITexture>(gameObject, "face_");
        face.mainTexture = UIHelper.getFace(name);

        //方便免修改 [selectServerWithRoomlist.prefab]
        serversList.items.Add("[OCG]233 Server");
        serversList.items.Add("[OCG]233 Server (大师规则2020)");
        serversList.items.Add("[TCG]Koishi Server");
        serversList.items.Add("[OCG&TCG]23333先行服");
        serversList.items.Add("[OCG&TCG]YGOhollow (JP)");
        serversList.items.Add("----------------------------------------------");
        serversList.items.Add("[2Pick]轮抽服");
        serversList.items.Add("[SP Duel]Koishi Server");
        //
        SetActiveFalse();
    }

    private void pickServer()
    {
        string server = serversList.value;
        switch (server)
        {
            case "[OCG]233 Server":
            {
                UIHelper.getByName<UIInput>(gameObject, "ip_").value = "s1.ygo233.com";
                UIHelper.getByName<UIInput>(gameObject, "port_").value = "233";
                Config.Set("serversPicker", "[OCG]233 Server");

                inputIP_.enabled = false;
                inputPort_.enabled = true;
                break;
            }
            case "[OCG]233 Server (大师规则2020)":
            {
                UIHelper.getByName<UIInput>(gameObject, "ip_").value = "s1.ygo233.com";
                UIHelper.getByName<UIInput>(gameObject, "port_").value = "2020";
                Config.Set("serversPicker", "[OCG]233 Server (大师规则2020)");

                inputIP_.enabled = false;
                inputPort_.enabled = false;
                break;
            }
            case "[TCG]Koishi Server":
            {
                UIHelper.getByName<UIInput>(gameObject, "ip_").value = "koishi.moecube.com";
                UIHelper.getByName<UIInput>(gameObject, "port_").value = "1311";
                Config.Set("serversPicker", "[TCG]Koishi Server");

                inputIP_.enabled = false;
                inputPort_.enabled = true;
                break;
            }
            case "[OCG&TCG]23333先行服":
            {
                UIHelper.getByName<UIInput>(gameObject, "ip_").value = "s1.ygo233.com";
                UIHelper.getByName<UIInput>(gameObject, "port_").value = "23333";
                Config.Set("serversPicker", "[OCG&TCG]23333先行服");

                inputIP_.enabled = false;
                inputPort_.enabled = false;
                break;
            }
            case "[OCG&TCG]YGOhollow (JP)":
            {
                UIHelper.getByName<UIInput>(gameObject, "ip_").value = "ads.ygoads.com";
                UIHelper.getByName<UIInput>(gameObject, "port_").value = "7911";
                Config.Set("serversPicker", "[OCG&TCG]YGOhollow (JP)");

                inputIP_.enabled = false;
                inputPort_.enabled = false;
                break;
            }
            //----------------------------------------------
            case "[SP Duel]Koishi Server":
            {
                UIHelper.getByName<UIInput>(gameObject, "ip_").value = "koishi.moecube.com";
                UIHelper.getByName<UIInput>(gameObject, "port_").value = "7373";
                Config.Set("serversPicker", "[SP Duel]Koishi Server");

                inputIP_.enabled = false;
                inputPort_.enabled = false;
                break;
            }
            case "[2Pick]轮抽服":
            {
                UIHelper.getByName<UIInput>(gameObject, "ip_").value = "2pick.mycard.moe";
                UIHelper.getByName<UIInput>(gameObject, "port_").value = "765";
                Config.Set("serversPicker", "[2Pick]轮抽服");

                inputIP_.enabled = false;
                inputPort_.enabled = false;
                break;
            }
            default:
            {
                Config.Set("serversPicker", "----------------------------------------------");

                inputIP_.enabled = true;
                inputPort_.enabled = true;
                break;
            }
        }
    }

    void onSelected()
    {
        if (list != null)
        {
            readString(list.value);
        }
    }

    private void readString(string str)
    {
        str = str.Substring(5, str.Length - 5);
        inputPsw.value = str;
    }

    void onClearPsw()
    {
        string PswString = File.ReadAllText("config/passwords.conf");
        string[] lines = PswString.Replace("\r", "").Split("\n");
        for (int i = 0; i < lines.Length; i++)
        {
            list.RemoveItem(lines[i]);//清空list
        }
        FileStream stream = new FileStream("config/passwords.conf", FileMode.Truncate, FileAccess.ReadWrite);//清空文件内容
        stream.Close();
        inputPsw.value = "";
        Program.PrintToChat(InterString.Get("房间密码已清空"));
    }

    public override void show()
    {
        base.show();
        Program.I().room.RMSshow_clear();
        printFile(true);
        Program.charge();
    }

    public override void preFrameFunction()
    {
        base.preFrameFunction();
        Menu.checkCommend();
    }

    void printFile(bool first)
    {
        list.Clear();
        if (File.Exists("config/passwords.conf") == false)
        {
            File.Create("config/passwords.conf").Close();
        }
        string txtString = File.ReadAllText("config/passwords.conf");
        string[] lines = txtString.Replace("\r", "").Split("\n");
        for (int i = 0; i < lines.Length; i++)
        {
            if (i == 0)
            {
                if (first)
                {
                    readString(lines[i]);
                }
            }
            list.AddItem(lines[i]);
        }
    }

    void onClickExit()
    {
        Program.I().shiftToServant(Program.I().menu);
        if (TcpHelper.tcpClient != null)
        {
            if (TcpHelper.tcpClient.Connected)
            {
                TcpHelper.tcpClient.Close();
            }
        }
        save();
    }

    public void set_version(string str)
    {
        UIHelper.getByName<UIInput>(gameObject, "version_").value = str;
    }

    void onClickJoin()
    {
        if (!isShowed)
        {
            return;
        }
        string Name = UIHelper.getByName<UIInput>(gameObject, "name_").value;
        string ipString = UIHelper.getByName<UIInput>(gameObject, "ip_").value;
        string portString = UIHelper.getByName<UIInput>(gameObject, "port_").value;
        string pswString = UIHelper.getByName<UIInput>(gameObject, "psw_").value;
        string versionString = UIHelper.getByName<UIInput>(gameObject, "version_").value;
        KF_onlineGame(Name, ipString, portString, versionString, pswString);
    }

    public void onClickRoomList()
    {
        if (!isShowed)
        {
            return;
        }
        string Name = UIHelper.getByName<UIInput>(gameObject, "name_").value;
        string ipString = UIHelper.getByName<UIInput>(gameObject, "ip_").value;
        string portString = UIHelper.getByName<UIInput>(gameObject, "port_").value;
        string pswString = "L";
        string versionString = UIHelper.getByName<UIInput>(gameObject, "version_").value;
        KF_onlineGame(Name, ipString, portString, versionString, pswString);
    }

    public void onHide(bool Bool)
    {
        gameObject.SetActive(!Bool);
    }

    public void KF_onlineGame(string Name, string ipString, string portString, string versionString, string pswString = "")
    {
        name = Name;
        Config.Set("name", name);
        if (ipString == "" || portString == "" || versionString == "")
        {
            RMSshow_onlyYes("", InterString.Get("非法输入！请检查输入的主机名。"), null);
        }
        else
        {
            if (name != "")
            {
                string fantasty = "psw: " + pswString;
                list.items.Remove(fantasty);
                list.items.Insert(0, fantasty);
                list.value = fantasty;
                if (list.items.Count > 5)
                {
                    list.items.RemoveAt(list.items.Count - 1);
                }
                string all = "";
                for (int i = 0; i < list.items.Count; i++)
                {
                    all += list.items[i] + "\r\n";
                }
                File.WriteAllText("config/passwords.conf", all);
                printFile(false);
                (new Thread(() => { TcpHelper.join(ipString, name, portString, pswString, versionString); })).Start();
            }
            else
            {
                RMSshow_onlyYes("", InterString.Get("昵称不能为空。"), null);
            }
        }
    }

    GameObject faceShow = null;

    public string name = "";

    void onClickFace()
    {
        name = UIHelper.getByName<UIInput>(gameObject, "name_").value;
        face.mainTexture = UIHelper.getFace(name);
        RMSshow_face("showFace", name);
        Config.Set("name", name);
    }

    public void save()
    {
        Config.Set("name", UIHelper.getByName<UIInput>(gameObject, "name_").value);
        Config.Set("ip_", UIHelper.getByName<UIInput>(gameObject, "ip_").value);
        Config.Set("port_", UIHelper.getByName<UIInput>(gameObject, "port_").value);
    }

}
