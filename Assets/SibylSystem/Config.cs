using System;
using System.Collections.Generic;
using System.IO;
public static class Config
{
    public static uint ClientVersion = 0x134b;

    class oneString
    {
        public string original = "";
        public string translated = "";
    }

    static List<oneString> translations = new List<oneString>();

    static List<oneString> uits = new List<oneString>();    

    static string path;

    public static bool getEffectON(string raw)
    {
        return true;
    }

    public static void initialize(string path)
    {
        Config.path = path;   
        if (File.Exists(path) == false)
        {
            File.Create(path).Close();
        }
        string txtString = File.ReadAllText(path);
        string[] lines = txtString.Replace("\r", "").Split("\n");
        for (int i = 0; i < lines.Length; i++)
        {
            string[] mats = lines[i].Split("->");
            if (mats.Length == 2)
            {
                oneString s = new oneString();
                s.original = mats[0];
                s.translated = mats[1];
                translations.Add(s);
            }
        }
    }

    static bool loaded = false;

    public static string Getui(string original)
    {
        if (loaded == false)
        {
            loaded = true;
            string[] lines = File.ReadAllText("textures/ui/config.txt").Replace("\r", "").Replace(" ", "").Split("\n");//YGOMobile Paths
            for (int i = 0; i < lines.Length; i++)
            {
                string[] mats = lines[i].Split("=");
                if (mats.Length == 2)
                {
                    oneString s = new oneString();
                    s.original = mats[0];
                    s.translated = mats[1];
                    uits.Add(s);
                }
            }
        }
        string return_value = "";
        for (int i = 0; i < uits.Count; i++)
        {
            if (uits[i].original == original)
            {
                return_value = uits[i].translated;
                break;
            }
        }
        return return_value;
    }

    internal static float getFloat(string v)
    {
        int getted = 0;
        try
        {
            getted = Int32.Parse(Get(v, "0"));
        }
        catch (Exception)   
        {
        }
        return ((float)getted) / 100000f;
    }

    internal static void setFloat(string v,float f) 
    {
        Set(v,((int)(f* 100000f)).ToString());
    }

    public static string Get(string original,string defau)  
    {
        string return_value = defau;
        bool finded = false;
        for (int i = 0; i < translations.Count; i++)
        {
            if (translations[i].original == original)
            {
                return_value = translations[i].translated;
                finded = true;
                break;
            }
        }
        if (finded == false)
        {
            if (path != null)
            {
                File.AppendAllText(path, original + "->" + defau + "\r\n");
                oneString s = new oneString();
                s.original = original;
                s.translated = defau;
                return_value = defau;
                translations.Add(s);
            }
        }
        return return_value;
    }

    public static void Set(string original,string setted)
    {
        bool finded = false;
        for (int i = 0; i < translations.Count; i++)
        {
            if (translations[i].original == original)
            {
                finded = true;
                translations[i].translated = setted;
            }
        }
        if (finded == false)
        {
            oneString s = new oneString();
            s.original = original;
            s.translated = setted;
            translations.Add(s);
        }
        string all = "";
        for (int i = 0; i < translations.Count; i++)
        {
            all += translations[i].original + "->" + translations[i].translated + "\r\n";
        }
        File.WriteAllText(path, all);
    }

    public static void writeUIConfig()
    {
        string line = "gameButtonSign.color=3e4043FF\ngameChainCheckArea.color=16181bFF\nallUI.color=16181bFF\nList.color=16181bFF\nlable.color=FFFFFFFF\nlable.color.fadecolor=CCCCCCFF";
        Directory.CreateDirectory(Path.GetDirectoryName("textures/ui/config.txt"));
        File.WriteAllText("textures/ui/config.txt", line);
    }

    public static void writeDuelConfig()
    {
        string line = "FFFF00";
        Directory.CreateDirectory(Path.GetDirectoryName("textures/duel/chainColor.txt"));
        File.WriteAllText("textures/duel/chainColor.txt", line);
    }

    public static void writeDuelHealthBarConfig()
    {
        string line = "totalSize.width=370\ntotalSize.height=124\nplayerNameLable.x=202\nplayerNameLable.y=31\nplayerNameLable.width=300\nplayerNameLable.height=22\nplayerNameLable.color=FFFFFFFF\nplayerNameLable.alignment=0\nplayerNameLable.effect=1\nhealthLable.x=230.9\nhealthLable.y=60.9\nhealthLable.width=300\nhealthLable.height=32\nhealthLable.color=FFFFFFFF\nhealthLable.alignment=0\nhealthLable.effect=2\ntimeLable.x=217.1\ntimeLable.y=87.8\ntimeLable.width=300\ntimeLable.height=24\ntimeLable.color=FFDAACFF\ntimeLable.alignment=0\ntimeLable.effect=2\nhealth.x=231.2\nhealth.y=62\nhealth.width=220\nhealth.height=30\ntime.x=216.7\ntime.y=87.4\ntime.width=190\ntime.height=20\nface.x=61.5\nface.y=72.6\nface.size=70\nface.type=0";
        Directory.CreateDirectory(Path.GetDirectoryName("textures/duel/healthBar/config.txt"));
        File.WriteAllText("textures/duel/healthBar/config.txt", line);
    }

}
