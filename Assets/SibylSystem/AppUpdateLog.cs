using UnityEngine;
using System.IO;

public class AppUpdateLog
{
    public static string GAME_VERSION = Program.PRO_VERSION() + "-0812";
    //游戏数据
    public static string GAME_DATA_VERSION = "updates/ver_" + GAME_VERSION + ".txt";
    //音效文件
    public static string GAME_BGM_VERSION = "updates/bgm_0.1.txt";
    //图片数据
    public static string GAME_IMAGE_VERSION = "updates/image_0.3.txt";
    //UI数据
    public static string GAME_UI_VERSION = "updates/ui.txt";

    //立绘数据
    public static string GAME_CLOSEUP_VERSION = "updates/closeup_0.6.txt";//新版本
    public static string OLD_CLOSEUP_VERSION = "updates/closeup_0.5.txt"; //旧版本

    public static string MAIN_CLOSEUP_ZIP = "closeup_0.6.zip";    //主数据
    public static string PATCH_CLOSEUP_ZIP = "up_closeup_0.6.zip";//更新包

    //MD5
    public static string MD5_MAIN_CLOSEUP = "a6fdf212c9bf76e6597b71bd1c672e15";//主数据
    public static string MD5_PATCH_CLOSEUP = "3facf0c655c7fbf6510fe7fe92af131c"; //更新包
    public static string MD5_IMAGE = "368c0f42651394e3aeda67cf3956af0e";

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