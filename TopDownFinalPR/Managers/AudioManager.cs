using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace TopDownFinalPR;

public class AudioManager
{
    private static ContentManager _content;
    private static readonly Dictionary<string, Song> _songs = new();
    private static readonly Dictionary<string, SoundEffect> _soundEffects = new();
    private static readonly List<SoundEffectInstance> _soundEffectInstances = new();

    private static float _prevMusicVolume;
    private static float _prevSFXVolume;

    // Global volume control for music
    public static float SongVolume
    {
        get => MediaPlayer.Volume;
        set => MediaPlayer.Volume = Math.Clamp(value, 0f, 1f);
    }

    // Mute all audio
    public static void MuteAudio()
    {
        _prevSFXVolume = SoundEffect.MasterVolume;
        SoundEffect.MasterVolume = 0f;
        MediaPlayer.IsMuted = true;
    }

    // Unmute all audio
    public static void UnMuteAudio()
    {
        SoundEffect.MasterVolume = _prevSFXVolume;
        MediaPlayer.IsMuted = false;
    }

    // Toggle mute status for audio
    public static void ChangeMusicStatus()
    {
        if (MediaPlayer.IsMuted)
            UnMuteAudio();
        else
            MuteAudio();
    }

    // Set up the content manager (must be called before loading sounds)
    public AudioManager(ContentManager contentManager)
    {
        _content = contentManager;
    }

    // Load and add a background music track
    public static void AddSong(string songName, string fileName)
    {
        try
        {
            if (!_songs.ContainsKey(songName))
                _songs[songName] = _content.Load<Song>(fileName);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: Failed to load song '{fileName}': {ex.Message}");
        }
    }

    // Retrieve a song by its name
    public static Song GetSong(string songName)
    {
        return _songs.GetValueOrDefault(songName);
    }

    // Play a song by name
    public static void PlaySong(string songName, bool isLoop = true, float volume = 1f)
    {
        Song song = GetSong(songName);
        if (song == null)
        {
            Console.WriteLine($"ERROR: Song '{songName}' not found.");
            return;
        }

        if (MediaPlayer.State == MediaState.Playing)
            MediaPlayer.Stop();

        MediaPlayer.Volume = volume;
        MediaPlayer.IsRepeating = isLoop;
        MediaPlayer.Play(song);
    }

    // Load and add a sound effect
    public static void AddSoundEffect(string soundName, string fileName)
    {
        try
        {
            if (!_soundEffects.ContainsKey(soundName))
                _soundEffects[soundName] = _content.Load<SoundEffect>(fileName);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: Failed to load sound effect '{fileName}': {ex.Message}");
        }
    }

    // Get a SoundEffectInstance for one-time use
    public static SoundEffectInstance GetSoundEffectInstance(string soundName)
    {
        if (!_soundEffects.ContainsKey(soundName))
        {
            Console.WriteLine($"ERROR: Sound effect '{soundName}' not found.");
            return null;
        }

        try
        {
            SoundEffect effect = _soundEffects[soundName];
            if (effect == null)
            {
                Console.WriteLine($"ERROR: SoundEffect '{soundName}' is null.");
                return null;
            }

            return effect.CreateInstance();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: Could not create instance for '{soundName}': {ex.Message}");
            return null;
        }
    }

    // Play a sound effect
    public static SoundEffectInstance PlaySoundEffect(string soundName, bool isLoop = false, float volume = 1f)
    {
        SoundEffectInstance instance = GetSoundEffectInstance(soundName);

        if (instance == null)
            return null;

        instance.Volume = volume;
        instance.IsLooped = isLoop;
        instance.Play();

        _soundEffectInstances.Add(instance);
        return instance;
    }
}
