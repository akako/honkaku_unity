using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Audio管理クラス。シーンをまたいでも破棄されないシングルトンで実装。
/// </summary>
public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    
    [SerializeField] private AudioSource _audioSource;
    private readonly Dictionary<string, AudioClip> _clips = new Dictionary<string, AudioClip>();

    public static AudioManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (null != instance)
        {
            // 既にインスタンスがある場合は自身を破棄する
            Destroy(gameObject);
            return;
        }

        // シーンを遷移しても破棄されなくする
        DontDestroyOnLoad(gameObject);
        // インスタンスとして保持
        instance = this;

        // Resources/2D_SEディレクトリ下のAudio Clipをすべて取得
        var audioClips = Resources.LoadAll<AudioClip>("2D_SE");
        foreach (var clip in audioClips)
        {
            // Audio ClipをDictionaryに保持しておく
            _clips.Add(clip.name, clip);
        }
    }

    /// <summary>
    /// 指定した名前の音声ファイルを再生します。
    /// </summary>
    /// <param name="clipName"></param>
    /// <exception cref="Exception"></exception>
    public void Play(string clipName)
    {
        if (!_clips.ContainsKey(clipName))
        {
            // 存在しない名前を指定したらエラー
            throw new Exception("Sound " + clipName + " is not defined");
        }

        // 指定の名前のclipに差し替えて再生
        _audioSource.clip = _clips[clipName];
        _audioSource.Play();
    }
}