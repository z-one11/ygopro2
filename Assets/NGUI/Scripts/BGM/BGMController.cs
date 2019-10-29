using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
//using NAudio;
//using NAudio.Wave;

public class BGMController : MonoBehaviour
{
    private float playTime = 0;
    private bool IsPlaying = false;

    public string soundFilePath;
    public AudioSource audioSource;
    AudioClip audioClip;
    private float multiplier;
    List<string> duel;
    List<string> disadvantage;
    List<string> deck;
    List<string> lobby;
    List<string> lose;
    List<string> menu;
    List<string> siding;
    List<string> win;
    List<string> advantage;
    BGMType currentPlaying;
    Coroutine soundRoutine;
    Uri SoundURI;
    public static BGMController Instance;

    //IWavePlayer iwavePlayer;
    //AudioFileReader audioFileReader;

    public enum BGMType
    {
       none = 0,
       duel = 1,
       disadvantage = 2,
       deck = 3,
       lobby = 4,
       lose = 5,
       menu = 6,
       siding = 7,
       win = 8,
       advantage = 9
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

    private void Update()
    {
        if (audioSource.clip != null && !Program.I().setting.isBGMMute.value && IsPlaying)
        {
            playTime += Time.fixedDeltaTime;
            if (playTime >= audioClip.length)
            {
                //audioSource.Stop();
                IsPlaying = false;
                StartBGM(currentPlaying);
            }
        }
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
            case BGMType.lobby:
                if (lobby.Count != 0)
                {
                    bgmNumber = rnd.Next(0, lobby.Count);
                    PlayRandomBGM(lobby[bgmNumber]);
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

        playTime = -3f;
        IsPlaying = true;
        currentPlaying = kind;
    }

    public void PlayRandomBGM(string bgmName)
    {
        SoundURI = new Uri(new Uri("file:///"), Environment.CurrentDirectory.Replace("\\", "/") + "/" + bgmName);
        soundFilePath = SoundURI.ToString();

        if (Program.I().setting != null && !Program.I().setting.isBGMMute.value)
        {
            if(soundRoutine != null)
                StopCoroutine(soundRoutine);
/*          PC平台，尚未找到好的解决方法，暂时就不支持MP3格式播放吧
            #if !UNITY_ANDROID || !UNITY_IPHONE
            if (bgmName.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
            {
                iwavePlayer = new WaveOut();
                audioFileReader = new AudioFileReader(bgmName);
                iwavePlayer.Init(audioFileReader);
                iwavePlayer.Play();
            } else {
                soundRoutine = StartCoroutine(LoadBGM());
            }
            #else
*/
            soundRoutine = StartCoroutine(LoadBGM());
            //#endif
        }
    }

    public void LoadAllBGM()
    {
        duel = new List<string>();
        disadvantage = new List<string>();
        advantage = new List<string>();
        deck = new List<string>();
        lobby = new List<string>();
        lose = new List<string>();
        menu = new List<string>();
        siding = new List<string>();
        win = new List<string>();

        string soundPath = "sound/bgm/";
        dirPath(soundPath);
        //Unity 能使用的音频格式：.aif .wav .mp3 .ogg
        //注: PC平台不支持外部加载MP3，移动平台才能正常使用
        duel.AddRange(Directory.GetFiles(string.Concat(soundPath, "duel"), "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".wav") || s.EndsWith(".ogg")));
        advantage.AddRange(Directory.GetFiles(string.Concat(soundPath, "advantage"), "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".wav") || s.EndsWith(".ogg")));
        disadvantage.AddRange(Directory.GetFiles(string.Concat(soundPath, "disadvantage"), "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".wav") || s.EndsWith(".ogg")));
        deck.AddRange(Directory.GetFiles(string.Concat(soundPath, "deck"), "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".wav") || s.EndsWith(".ogg")));
        lobby.AddRange(Directory.GetFiles(string.Concat(soundPath, "lobby"), "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".wav") || s.EndsWith(".ogg")));
        lose.AddRange(Directory.GetFiles(string.Concat(soundPath, "lose"), "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".wav") || s.EndsWith(".ogg")));
        menu.AddRange(Directory.GetFiles(string.Concat(soundPath, "menu"), "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".wav") || s.EndsWith(".ogg")));
        siding.AddRange(Directory.GetFiles(string.Concat(soundPath, "siding"), "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".wav") || s.EndsWith(".ogg")));
        win.AddRange(Directory.GetFiles(string.Concat(soundPath, "win"), "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".wav") || s.EndsWith(".ogg")));
    }

    public void dirPath(string path)
    {
        List<string> BGMdir = new List<string>();
        //音乐文件夹
        BGMdir.Add("duel/");
        BGMdir.Add("advantage/");
        BGMdir.Add("disadvantage/");
        BGMdir.Add("deck/");
        BGMdir.Add("lobby/");
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

    private void PlayAudioFile()
    {
        audioSource.clip = audioClip;
        audioSource.volume = Program.I().setting.BGMvol() * multiplier;
        //audioSource.loop = true;
        audioSource.Play();
    }

    private WWW GetAudioFromFile(string pathToFile)
    {
        WWW request = new WWW(pathToFile);
        return request;
    }
}
