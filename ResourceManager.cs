using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains all the Audio files and Materials 
/// </summary>
public static class ResourceManager 
{
    public static bool initialized = false;
    static AudioSource _audioSource;
    static Dictionary<AudioClipName, AudioClip> _audioClip = new Dictionary<AudioClipName, AudioClip>();
    static Dictionary<MaterialName, Material> _material = new Dictionary<MaterialName, Material>();

    public static void InitializeResources(AudioSource aSource)
    {
        //Initializing Audio
        _audioSource = aSource;
        initialized = true;

        _audioClip.Add(AudioClipName.BackgroundMusic, Resources.Load<AudioClip>(@"Audio/BackgroundMusic"));
        _audioClip.Add(AudioClipName.BonusHit, Resources.Load<AudioClip>(@"Audio/BonusHit"));
        _audioClip.Add(AudioClipName.GameOver, Resources.Load<AudioClip>(@"Audio/GameOver"));
        _audioClip.Add(AudioClipName.GunEffect,Resources.Load<AudioClip>(@"Audio/GunEffect"));
        _audioClip.Add(AudioClipName.NegativeHit, Resources.Load<AudioClip>(@"Audio/NegativeHit"));
        _audioClip.Add(AudioClipName.PositiveHit, Resources.Load<AudioClip>(@"Audio/PositiveHit"));
        _audioClip.Add(AudioClipName.Lift, Resources.Load<AudioClip>(@"Audio/lift"));
        _audioClip.Add(AudioClipName.LevelS, Resources.Load<AudioClip>(@"Audio/LevelS"));
        _audioClip.Add(AudioClipName.DragonDeath, Resources.Load<AudioClip>(@"Audio/DragonDeath")); 
        _audioClip.Add(AudioClipName.DragonAttack, Resources.Load<AudioClip>(@"Audio/DragonAttack")); 
        _audioClip.Add(AudioClipName.HellMate, Resources.Load<AudioClip>(@"Audio/HellMate"));
        _audioClip.Add(AudioClipName.HellMate2, Resources.Load<AudioClip>(@"Audio/HellMate2")); 
        _audioClip.Add(AudioClipName.BulletImpact, Resources.Load<AudioClip>(@"Audio/BulletImpact"));
        _audioClip.Add(AudioClipName.TimeMate, Resources.Load<AudioClip>(@"Audio/TimeMate")); 
         _audioClip.Add(AudioClipName.Minigun, Resources.Load<AudioClip>(@"Audio/Minigun"));

        //Initializing skybox Materials
        _material.Add(MaterialName.Bonus, Resources.Load<Material>(@"Materials/SkyBox - Bonus"));
        _material.Add(MaterialName.Bumper, Resources.Load<Material>(@"Materials/SkyBox - Bumper"));
        _material.Add(MaterialName.Green, Resources.Load<Material>(@"Materials/SkyBox - Green"));
        _material.Add(MaterialName.Platform, Resources.Load<Material>(@"Materials/SkyBox - Platform"));
        _material.Add(MaterialName.RedDust, Resources.Load<Material>(@"Materials/SkyBox - RedDust"));
        _material.Add(MaterialName.DarkBlue, Resources.Load<Material>(@"Materials/SkyBox - DarkBlue"));
        _material.Add(MaterialName.TBS, Resources.Load<Material>(@"Materials/SkyBox - TBS"));
        


        //EInitializing Materials
  
        _material.Add(MaterialName.EBumper, Resources.Load<Material>(@"EMaterials/Bumper"));
        _material.Add(MaterialName.EGreen, Resources.Load<Material>(@"EMaterials/Green"));
        _material.Add(MaterialName.ENavyGrid, Resources.Load<Material>(@"EMaterials/NavyGrid"));             
        _material.Add(MaterialName.EPickup, Resources.Load<Material>(@"EMaterials/Pickup"));
        _material.Add(MaterialName.EPinkSmooth, Resources.Load<Material>(@"EMaterials/PinkSmooth"));
        _material.Add(MaterialName.EPlatform, Resources.Load<Material>(@"EMaterials/Platform"));
        _material.Add(MaterialName.EPurple, Resources.Load<Material>(@"EMaterials/Purple"));
        _material.Add(MaterialName.ERedDust, Resources.Load<Material>(@"EMaterials/RedDust"));
        _material.Add(MaterialName.ERedYello, Resources.Load<Material>(@"EMaterials/SkyBox"));
        _material.Add(MaterialName.ETBS, Resources.Load<Material>(@"EMaterials/TBS"));



    }

    public static void Play(AudioClipName name, float vol)    
    {
        _audioSource.PlayOneShot(_audioClip[name], vol);
    }

    public static void ChangeSkybox(MaterialName Mat) 
    {
        Camera.main.GetComponent<Skybox>().material = _material[Mat];
    }
    



}

