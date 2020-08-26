using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject Cartridge;
    public Transform barrelLocation;
    public Transform cartridgeExitLocation;
    public float BulletPower;

    public string gunName;
    public float range;
    public float accuracy;
    public float fireRate;
    public float reloadTime;

    public int damage;

    public int reloadBulletCount;
    public int currentBulletCount;

    public Animator anim;
    public Animator muzzleFlash;
    public AudioClip fire_Sound;
}
