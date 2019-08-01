using UnityEngine;
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

    public override void initialize()
    {
        createWindow(Program.I().new_ui_selectServer);
        UIHelper.registEvent(gameObject, "exit_", onClickExit);
        UIHelper.registEvent(gameObject, "face_", onClickFace);
        UIHelper.registEvent(gameObject, "join_", onClickJoin);
        UIHelper.registEvent(gameObject, "clearPsw_", onClearPsw);
        serversList = UIHelper.getByName<UIPopupList>(gameObject, "server");
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
        serversList.value = Config.Get("serversPicker", "[Ser1] 233正式服");

        serversList.items.Add("[Ser1] 233正式服");
        serversList.items.Add("[Ser2] 23333先行服");
        serversList.items.Add("[Ser3] 2Pick轮抽服");
        serversList.items.Add("[Ser4] Koishi Server (TCG)");
        serversList.items.Add("[Ser5] Koishi Server (高速决斗)");
        serversList.items.Add("[自定义]");

        SetActiveFalse();
    }

    private void pickServer()
    {
        string server = serversList.value;
        switch (server)
        {
            case "[Ser1] 233正式服":
            {
                UIHelper.getByName<UIInput>(gameObject, "ip_").value = "s1.ygo233.com";
                UIHelper.getByName<UIInput>(gameObject, "port_").value = "233";
                Config.Set("serversPicker", "[Ser1] 233正式服");
                save();

                inputIP_.enabled = false;
                inputPort_.enabled = false;
                break;
            }
            case "[Ser2] 23333先行服":
            {
                UIHelper.getByName<UIInput>(gameObject, "ip_").value = "s1.ygo233.com";
                UIHelper.getByName<UIInput>(gameObject, "port_").value = "23333";
                Config.Set("serversPicker", "[Ser2] 23333先行服");
                save();

                inputIP_.enabled = false;
                inputPort_.enabled = false;
                break;
            }
            case "[Ser3] 2Pick轮抽服":
            {
                UIHelper.getByName<UIInput>(gameObject, "ip_").value = "2pick.mycard.moe";
                UIHelper.getByName<UIInput>(gameObject, "port_").value = "765";
                Config.Set("serversPicker", "[Ser3] 2Pick轮抽服");
                save();

                inputIP_.enabled = false;
                inputPort_.enabled = false;
                break;
            }
            case "[Ser4] Koishi Server (TCG)":
            {
                UIHelper.getByName<UIInput>(gameObject, "ip_").value = "koishi.moecube.com";
                UIHelper.getByName<UIInput>(gameObject, "port_").value = "1311";
                Config.Set("serversPicker", "[Ser4] Koishi Server (TCG)");
                save();

                inputIP_.enabled = false;
                inputPort_.enabled = false;
                break;
            }
            case "[Ser5] Koishi Server (高速决斗)":
            {
                UIHelper.getByName<UIInput>(gameObject, "ip_").value = "koishi.moecube.com";
                UIHelper.getByName<UIInput>(gameObject, "port_").value = "7373";
                Config.Set("serversPicker", "[Ser5] Koishi Server (高速决斗)");
                save();

                inputIP_.enabled = false;
                inputPort_.enabled = false;
                break;
            }
            default:
            {
                Config.Set("serversPicker", "[自定义]");
                save();

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
        RMSshow_face("showFace", name);
        Config.Set("name", name);
    }

    public void save()
    {
        Config.Set("ip_", UIHelper.getByName<UIInput>(gameObject, "ip_").value);
        Config.Set("port_", UIHelper.getByName<UIInput>(gameObject, "port_").value);
    }

}
