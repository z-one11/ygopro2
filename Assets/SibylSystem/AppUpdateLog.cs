using UnityEngine;
using System.IO;

public class AppUpdateLog
{
    public static string GAME_VERSION = Program.PRO_VERSION() + "-0107";
    //游戏数据
    public static string GAME_DATA_VERSION = "updates/ver_" + GAME_VERSION + ".txt";
    //音效文件
    public static string GAME_SOUND_VERSION = "updates/sound_0.1.txt";
    //图片数据
    public static string GAME_IMAGE_VERSION = "updates/image_0.5.txt";    //卡图包
    //UI数据
    public static string GAME_UI_VERSION = "updates/ui.txt";

    //立绘数据
    public static string GAME_CLOSEUP_VERSION = "updates/closeup_0.12.txt";//新版本
    public static string OLD_CLOSEUP_VERSION = "updates/closeup_0.11.txt"; //旧版本

    public static string MAIN_CLOSEUP_ZIP = "closeup_0.12.zip";    //主数据
    public static string PATCH_CLOSEUP_ZIP = "up_closeup_0.12.zip";//更新包

    //MD5
    public static string MD5_MAIN_CLOSEUP = "2e1f2b2fc9d89ffa1e82743710b7b03b";  //主数据
    public static string MD5_PATCH_CLOSEUP = "b6db833490c7d99c0e827b4d973ce2ff"; //更新包
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