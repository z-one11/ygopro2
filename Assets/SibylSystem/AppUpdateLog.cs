using UnityEngine;
using System.IO;

public class AppUpdateLog
{
    public static string GAME_VERSION = Program.PRO_VERSION() + "-0917";
    //游戏数据
    public static string GAME_DATA_VERSION = "updates/ver_" + GAME_VERSION + ".txt";
    //音效文件
    public static string GAME_BGM_VERSION = "updates/bgm_0.1.txt";
    //图片数据
    public static string GAME_IMAGE_VERSION = "updates/image_0.3.txt";
    //UI数据
    public static string GAME_UI_VERSION = "updates/ui.txt";

    //立绘数据
    public static string GAME_CLOSEUP_VERSION = "updates/closeup_0.7.txt";//新版本
    public static string OLD_CLOSEUP_VERSION = "updates/closeup_0.6.txt"; //旧版本

    public static string MAIN_CLOSEUP_ZIP = "closeup_0.7.zip";    //主数据
    public static string PATCH_CLOSEUP_ZIP = "up_closeup_0.7.zip";//更新包

    //MD5
    public static string MD5_MAIN_CLOSEUP = "b179118413468e182181810e6bd6b7fe";//主数据
    public static string MD5_PATCH_CLOSEUP = "a5b282cc537468d89288191a05fa74ee"; //更新包
    public static string MD5_IMAGE = "b841f081e850a6b712c5f2657fae15b9";

    //保留的文件
    public static string[] File =
    {
        Program.GAME_PATH + GAME_DATA_VERSION,
        Program.GAME_PATH + GAME_BGM_VERSION,
        Program.GAME_PATH + GAME_CLOSEUP_VERSION,
        Program.GAME_PATH + GAME_IMAGE_VERSION,
        Program.GAME_PATH + GAME_UI_VERSION
    };

}