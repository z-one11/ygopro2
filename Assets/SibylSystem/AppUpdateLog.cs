using UnityEngine;
using System.IO;

public class AppUpdateLog
{
    public static string GAME_VERSION = Program.PRO_VERSION() + "-0729";
    //游戏数据
    public static string GAME_DATA_VERSION = "updates/ver_" + GAME_VERSION + ".txt";
    //音效文件
    public static string GAME_BGM_VERSION = "updates/bgm_0.1.txt";
    //图片数据
    public static string GAME_IMAGE_VERSION = "updates/image_0.2.txt";
    //UI数据
    public static string GAME_UI_VERSION = "updates/ui.txt";

    //立绘数据
    public static string GAME_CLOSEUP_VERSION = "updates/closeup_0.5.txt";//新版本
    public static string OLD_CLOSEUP_VERSION = "updates/closeup_0.5.txt"; //(暂无更新包，下次移除)
    //public static string OLD_CLOSEUP_VERSION = "updates/closeup_0.4.txt"; //旧版本

    public static string MAIN_CLOSEUP_ZIP = "closeup_0.5.zip";    //主数据
    public static string PATCH_CLOSEUP_ZIP = "up_closeup_0.5.zip";//更新包

    //MD5
    public static string MD5_MAIN_CLOSEUP = "64337778f5c55434f49c1c79385b641f";//主数据
    public static string MD5_PATCH_CLOSEUP = "64337778f5c55434f49c1c79385b641f"; //(暂无更新包，下次移除)
    //public static string MD5_OLD_CLOSEUP = "8a8bf2e69e1b0198b0a8231ec3b7bf10"; //更新包
    public static string MD5_IMAGE = "5d8b494c835806383f3b4db3d1d13913";

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