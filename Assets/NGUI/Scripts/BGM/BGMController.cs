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
    private float multiplier;
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
        LoadAllBGM();
    }
    // Use this for initialization
    public void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        multiplier = 0.8f;
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
                    PlayRandomBGM(duel[bgmNumber]);
                }
                break;
            case BGMType.advantage:
                if (advantage.Count != 0)
                {
                    bgmNumber = rnd.Next(0, advantage.Count);
                    PlayRandomBGM(advantage[bgmNumber]);
                }
                break;
            case BGMType.disadvantage:
                if (disadvantage.Count != 0)
                {
                    bgmNumber = rnd.Next(0, disadvantage.Count);
                    PlayRandomBGM(disadvantage[bgmNumber]);
                }
                break;
            case BGMType.deck:
                if (deck.Count != 0)
                {
                    bgmNumber = rnd.Next(0, deck.Count);
                    PlayRandomBGM(deck[bgmNumber]);
                }
                break;
            case BGMType.lose:
                if (lose.Count != 0)
                {
                    bgmNumber = rnd.Next(0, lose.Count);
                    PlayRandomBGM(lose[bgmNumber]);
                }
                break;
            case BGMType.menu:
                if (menu.Count != 0)
                {
                    bgmNumber = rnd.Next(0, menu.Count);
                    PlayRandomBGM(menu[bgmNumber]);
                }
                break;
            case BGMType.siding:
                if (siding.Count != 0)
                {
                    bgmNumber = rnd.Next(0, siding.Count);
                    PlayRandomBGM(siding[bgmNumber]);
                }
                break;
            case BGMType.win:
                if (win.Count != 0)
                {
                    bgmNumber = rnd.Next(0, win.Count);
                    PlayRandomBGM(win[bgmNumber]);
                }
                break;
        }

        IsPlaying = true;
        currentPlaying = kind;
    }

    public void PlayRandomBGM(string bgmName)
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
        }
    }

    public void LoadAllBGM()
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
                audioSource.volume = vol * multiplier;
            }
        }
        catch { }

    }

    private IEnumerator LoadBGM()
    {
        WWW request = GetAudioFromFile(soundFilePath);
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
        audioSource.volume = Program.I().setting.BGMvol() * multiplier;
        //audioSource.loop = true;
        audioSource.Play();
        soundPlayNext = StartCoroutine(PlayNext(audioClip.length));
    }

    private WWW GetAudioFromFile(string pathToFile)
    {
        WWW request = new WWW(pathToFile);
        return request;
    }
}
