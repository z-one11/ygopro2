using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using NLayer;// Loads mp3 files

public class BGMController : MonoBehaviour
{
    private bool IsPlaying = false;

    public string soundFilePath;
    public AudioSource audioSource;
    AudioClip audioClip;
    List<string> duel;
    List<string> disadvantage;
    List<string> deck;
    List<string> lose;
    List<string> menu;
    List<string> siding;
    List<string> win;
    List<string> advantage;
    BGMType currentPlaying;
    Coroutine soundRoutine;
    Coroutine soundPlayNext;
    Uri SoundURI;
    public static BGMController Instance;

    public enum BGMType
    {
       none = 0,
       duel = 1,
       disadvantage = 2,
       deck = 3,
       lose = 4,
       menu = 5,
       siding = 6,
       win = 7,
       advantage = 8
    }

    public BGMController ()
    {
        currentPlaying = BGMType.none;
        BGMController.Instance = this;
        RefreshBGMDir();
    }
    // Use this for initialization
    public void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0;
    }

    public void PlayNext()
    {
        IsPlaying = false;
        StartBGM(currentPlaying);
    }

    public void StartBGM(BGMType kind)
    {
        if (currentPlaying == kind && IsPlaying)
            return;

        System.Random rnd = new System.Random();
        int bgmNumber = 0;
        switch (kind)
        {
            case BGMType.duel:
                if (duel.Count != 0)
                {
                    bgmNumber = rnd.Next(0, duel.Count);
                    PlayMusic(duel[bgmNumber]);
                }
                break;
            case BGMType.advantage:
                if (advantage.Count != 0)
                {
                    bgmNumber = rnd.Next(0, advantage.Count);
                    PlayMusic(advantage[bgmNumber]);
                }
                break;
            case BGMType.disadvantage:
                if (disadvantage.Count != 0)
                {
                    bgmNumber = rnd.Next(0, disadvantage.Count);
                    PlayMusic(disadvantage[bgmNumber]);
                }
                break;
            case BGMType.deck:
                if (deck.Count != 0)
                {
                    bgmNumber = rnd.Next(0, deck.Count);
                    PlayMusic(deck[bgmNumber]);
                }
                break;
            case BGMType.lose:
                if (lose.Count != 0)
                {
                    bgmNumber = rnd.Next(0, lose.Count);
                    PlayMusic(lose[bgmNumber]);
                }
                break;
            case BGMType.menu:
                if (menu.Count != 0)
                {
                    bgmNumber = rnd.Next(0, menu.Count);
                    PlayMusic(menu[bgmNumber]);
                }
                break;
            case BGMType.siding:
                if (siding.Count != 0)
                {
                    bgmNumber = rnd.Next(0, siding.Count);
                    PlayMusic(siding[bgmNumber]);
                }
                break;
            case BGMType.win:
                if (win.Count != 0)
                {
                    bgmNumber = rnd.Next(0, win.Count);
                    PlayMusic(win[bgmNumber]);
                }
                break;
        }

        currentPlaying = kind;
    }

    public void PlayMusic(string bgmName)
    {
        SoundURI = new Uri(new Uri("file:///"), Environment.CurrentDirectory.Replace("\\", "/") + "/" + bgmName);
        soundFilePath = SoundURI.ToString();
        if (Program.I().setting != null && !Program.I().setting.isBGMMute.value)
        {
            if (soundRoutine != null) { StopCoroutine(soundRoutine); }
            if (soundPlayNext != null) { StopCoroutine(soundPlayNext); }

            #if UNITY_ANDROID || UNITY_IPHONE
                soundRoutine = StartCoroutine(LoadBGM());
            #else
                if (bgmName.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
                {
                    soundFilePath = bgmName;
                    soundRoutine = StartCoroutine(LoadMP3());
                } else {
                    soundRoutine = StartCoroutine(LoadBGM());
                }
            #endif
            IsPlaying = true;
        }
    }

    public bool PlayChant(int code)
    {
        if (!Directory.Exists("sound/chants/")) Directory.CreateDirectory("sound/chants/");
        string path = "sound/chants/" + code.ToString() + ".mp3";
        if (!File.Exists(path))
        {
            path = "sound/chants/" + code.ToString() + ".wav";
        }
        if (!File.Exists(path))
        {
            path = "sound/chants/" + code.ToString() + ".ogg";
        }
        if (File.Exists(path) && audioClip.name != Path.GetFileName(path))
        {
            IsPlaying = false;
            PlayMusic(path);
            return true;
        }
        return false;
    }

    public void RefreshBGMDir()
    {
        duel = new List<string>();
        disadvantage = new List<string>();
        advantage = new List<string>();
        deck = new List<string>();
        lose = new List<string>();
        menu = new List<string>();
        siding = new List<string>();
        win = new List<string>();

        string soundPath = "sound/bgm/";
        dirPath(soundPath);
        //Unity 能使用的音频格式：.aif .wav .mp3 .ogg
        duel.AddRange(Directory.GetFiles(string.Concat(soundPath, "duel"), "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".mp3") || s.EndsWith(".ogg") || s.EndsWith(".wav")));
        advantage.AddRange(Directory.GetFiles(string.Concat(soundPath, "advantage"), "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".mp3") || s.EndsWith(".ogg") || s.EndsWith(".wav")));
        disadvantage.AddRange(Directory.GetFiles(string.Concat(soundPath, "disadvantage"), "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".mp3") || s.EndsWith(".ogg") || s.EndsWith(".wav")));
        deck.AddRange(Directory.GetFiles(string.Concat(soundPath, "deck"), "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".mp3") || s.EndsWith(".ogg") || s.EndsWith(".wav")));
        lose.AddRange(Directory.GetFiles(string.Concat(soundPath, "lose"), "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".mp3") || s.EndsWith(".ogg") || s.EndsWith(".wav")));
        menu.AddRange(Directory.GetFiles(string.Concat(soundPath, "menu"), "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".mp3") || s.EndsWith(".ogg") || s.EndsWith(".wav")));
        siding.AddRange(Directory.GetFiles(string.Concat(soundPath, "siding"), "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".mp3") || s.EndsWith(".ogg") || s.EndsWith(".wav")));
        win.AddRange(Directory.GetFiles(string.Concat(soundPath, "win"), "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".mp3") || s.EndsWith(".ogg") || s.EndsWith(".wav")));
    }

    public void dirPath(string path)
    {
        List<string> BGMdir = new List<string>();
        //音乐文件夹
        BGMdir.Add("duel/");
        BGMdir.Add("advantage/");
        BGMdir.Add("disadvantage/");
        BGMdir.Add("deck/");
        BGMdir.Add("lose/");
        BGMdir.Add("menu/");
        BGMdir.Add("siding/");
        BGMdir.Add("win/");
        //创建文件夹
        for(int i = 0; i < BGMdir.Count; i++)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path + BGMdir[i])))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path + BGMdir[i]));
            }
        }
    }

    public void changeBGMVol(float vol)
    {
        try
        {
            if (audioSource != null)
            {
                audioSource.volume = vol;
            }
        }
        catch { }
    }

    private IEnumerator LoadBGM()
    {
        WWW request = new WWW(soundFilePath);
        yield return request;
        audioClip = request.GetAudioClip(true, true);
        audioClip.name = Path.GetFileName(soundFilePath);
        PlayAudioFile();
    }

    private IEnumerator LoadMP3()
    {
        yield return null;
        audioClip = Mp3Loader.LoadMp3(soundFilePath);
        audioClip.name = Path.GetFileName(soundFilePath);
        PlayAudioFile();
    }

    private IEnumerator PlayNext(float time)
    {
        yield return new WaitForSeconds(time);

        IsPlaying = false;
        StartBGM(currentPlaying);
    }

    private void PlayAudioFile()
    {
        audioSource.clip = audioClip;
        //audioSource.loop = true;
        audioSource.Play();
        soundPlayNext = StartCoroutine(PlayNext(audioClip.length));
    }

}
