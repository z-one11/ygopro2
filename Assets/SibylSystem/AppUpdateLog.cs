using UnityEngine;
using System.IO;

public class AppUpdateLog
{
    public static string GAME_VERSION = Program.PRO_VERSION() + "-1219";
    //游戏数据
    public static string GAME_DATA_VERSION = "updates/ver_" + GAME_VERSION + ".txt";
    //音效文件
    public static string GAME_SOUND_VERSION = "updates/sound_0.1.txt";
    //图片数据
    public static string GAME_IMAGE_VERSION = "updates/image_0.5.txt";    //卡图包
    //UI数据
    public static string GAME_UI_VERSION = "updates/ui.txt";

    //立绘数据
    public static string GAME_CLOSEUP_VERSION = "updates/closeup_0.11.txt";//新版本
    public static string OLD_CLOSEUP_VERSION = "updates/closeup_0.10.txt"; //旧版本

    public static string MAIN_CLOSEUP_ZIP = "closeup_0.11.zip";    //主数据
    public static string PATCH_CLOSEUP_ZIP = "up_closeup_0.11.zip";//更新包

    //MD5
    public static string MD5_MAIN_CLOSEUP = "b661703c411fea0154ec3d0c8e4eef01";  //主数据
    public static string MD5_PATCH_CLOSEUP = "acd0d950c5bd8bbf956f61d4a66ecac4"; //更新包
    public static string MD5_IMAGE = "cac9a9dc4748e838a5ba3d6aa3dc924c";         //卡图包

    //保留的文件
    public static string[] File =
    {
        Program.GAME_PATH + GAME_DATA_VERSION,
        Program.GAME_PATH + GAME_SOUND_VERSION,
        Program.GAME_PATH + GAME_CLOSEUP_VERSION,
        Program.GAME_PATH + GAME_IMAGE_VERSION,
        Program.GAME_PATH + GAME_UI_VERSION
    };

}