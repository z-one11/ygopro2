using UnityEngine;
using System.IO;

public class AppUpdateLog
{
    //游戏数据
    public static string GAME_DATA_VERSION = "updates/ver_" +  Program.PRO_VERSION() + ".txt";
    //音效文件
    public static string GAME_BGM_VERSION = "updates/bgm_0.1.txt";
    //立绘数据
    public static string GAME_CLOSEUP_VERSION = "updates/closeup_0.4.txt";
    //图片数据
    public static string GAME_IMAGE_VERSION = "updates/image_0.2.txt";
    //UI数据
    public static string GAME_UI_VERSION = "updates/ui.txt";

    //处理立绘数据相关文件
    public static string OLD_CLOSEUP_VERSION = "updates/closeup_0.3.txt";
    public static string MAIN_CLOSEUP_ZIP = "closeup_0.4.zip";
    public static string PATCH_CLOSEUP_ZIP = "up_closeup_0.4.zip";

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