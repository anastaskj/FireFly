using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string AppearFire;

    [FMODUnity.EventRef]
    public string ArmorPickup;

    [FMODUnity.EventRef]
    public string AttackFire;

    [FMODUnity.EventRef]
    public string ClothMovement;

    [FMODUnity.EventRef]
    public string DeathPlayer;

    [FMODUnity.EventRef]
    public string EnemyDeathFire;

    [FMODUnity.EventRef]
    public string HealthPickup;

    [FMODUnity.EventRef]
    public string LasherWalk;

    [FMODUnity.EventRef]
    public string PixieWalk;

    [FMODUnity.EventRef]
    public string StaticFire;

    [FMODUnity.EventRef]
    public string StepsPlayer;

    [FMODUnity.EventRef]
    public string TakeDamagePlayer;

    [FMODUnity.EventRef]
    public string Wisp;

    [FMODUnity.EventRef]
    public string DeathPlayerFire;

    [FMODUnity.EventRef]
    public string PixieAttack;

    [FMODUnity.EventRef]
    public string TerraCollide;

    [FMODUnity.EventRef]
    public string TerraDeath;

    [FMODUnity.EventRef]
    public string TerraSpawn;

    [FMODUnity.EventRef]
    public string PortalActivate;

    [FMODUnity.EventRef]
    public string LasherAttack;

    [FMODUnity.EventRef]
    public string XpGain;

    [FMODUnity.EventRef]
    public string PickupFire;

    [FMODUnity.EventRef]
    public string CantChangeFire;

    [FMODUnity.EventRef]
    public string BarrierSpawn;

    [FMODUnity.EventRef]
    public string MapToggle;

    //FMOD.Studio.EventInstance Music;
    //private void Awake()
    //{
    //    Music = FMODUnity.RuntimeManager.CreateInstance("event:/Music");
    //}

    //private void Start()
    //{
    //    FMODUnity.RuntimeManager.AttachInstanceToGameObject(Music, GetComponent<Transform>(), GetComponent<Rigidbody2D>());
    //    Music.start();
    //}

    public void AppearFireSound() //
    {
        FMODUnity.RuntimeManager.PlayOneShot(this.AppearFire);
    }
    public void ArmorPickupSound() //
    {
        FMODUnity.RuntimeManager.PlayOneShot(this.ArmorPickup);
    }
    public void AttackFireSound() //
    {
        FMODUnity.RuntimeManager.PlayOneShot(this.AttackFire);
    }
    public void ClothMovementSound() //
    {
        FMODUnity.RuntimeManager.PlayOneShot(this.ClothMovement);
    }
    public void DeathPlayerSound() //
    {
        FMODUnity.RuntimeManager.PlayOneShot(this.DeathPlayer);
    }
    public void EnemyDeathFireSound() //
    {
        FMODUnity.RuntimeManager.PlayOneShot(this.EnemyDeathFire);
    }
    public void HealthPickupSound() //
    {
        FMODUnity.RuntimeManager.PlayOneShot(this.HealthPickup);
    }
    public void LasherWalkSound() //
    {
        FMODUnity.RuntimeManager.PlayOneShot(this.LasherWalk); 
    }
    public void PixieWalkSound() //
    {
        FMODUnity.RuntimeManager.PlayOneShot(this.PixieWalk); 
    }
    public void StaticFireSound() //
    {
        FMOD.Studio.EventInstance StaticFire;
        StaticFire = FMODUnity.RuntimeManager.CreateInstance("event:/StaticFire");
        StaticFire.start();
        
    }
    public void PortalActivateSound()
    {
        FMOD.Studio.EventInstance portalActive;
        portalActive = FMODUnity.RuntimeManager.CreateInstance("event:/PortalActivate");
        portalActive.start();

    }
    public void PortalDeactivateSound()
    {
        FMOD.Studio.EventInstance portalActive;
        portalActive = FMODUnity.RuntimeManager.CreateInstance("event:/PortalActivate");
        portalActive.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

    }

    public void StepsPlayerSound() //
    {
        FMODUnity.RuntimeManager.PlayOneShot(this.StepsPlayer);
    }
    public void TakeDamagePlayerSound() //
    {
        FMODUnity.RuntimeManager.PlayOneShot(this.TakeDamagePlayer); 
    }
    public void WispSound() //
    {
        FMODUnity.RuntimeManager.PlayOneShot(this.Wisp);
    }
    public void DeathPlayerFireSound() //
    {
        FMODUnity.RuntimeManager.PlayOneShot(this.DeathPlayerFire);
    }
    public void PixieAttackSound() //
    {
        FMODUnity.RuntimeManager.PlayOneShot(this.PixieAttack);
    }
    public void CantChangeFireSound() 
    {
        FMODUnity.RuntimeManager.PlayOneShot(this.CantChangeFire);
    }
    public void PickupFireSound() 
    {
        FMODUnity.RuntimeManager.PlayOneShot(this.PickupFire);
    }
    public void LasherAttackSound() 
    {
        FMODUnity.RuntimeManager.PlayOneShot(this.LasherAttack);
    }
    public void GainXpSound() 
    {
        FMODUnity.RuntimeManager.PlayOneShot(this.XpGain);
    }
    public void TerraSpawnSound() 
    {
        FMODUnity.RuntimeManager.PlayOneShot(this.TerraSpawn);
    }
    public void TerraDeathSound() 
    {
        FMODUnity.RuntimeManager.PlayOneShot(this.TerraDeath);
    }
    public void TerraCollideSound() 
    {
        FMODUnity.RuntimeManager.PlayOneShot(this.TerraCollide);
    }
    public void BarrierSpawnSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(this.BarrierSpawn);
    }
    public void MapToggleSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(this.MapToggle);
    }

}
