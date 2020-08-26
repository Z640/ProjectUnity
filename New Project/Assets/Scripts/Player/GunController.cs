using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Rigidbody2DExtension
{
    public static void AddExplosionForce(this Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
        var dir = (body.transform.position - explosionPosition);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        body.AddForce(dir.normalized * explosionForce * wearoff);
    }
 
    public static void AddExplosionForce(this Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius, float upliftModifier)
    {
        var dir = (body.transform.position - explosionPosition);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        Vector3 baseForce = dir.normalized * explosionForce * wearoff;
        body.AddForce(baseForce);
 
        float upliftWearoff = 1 - upliftModifier / explosionRadius;
        Vector3 upliftForce = Vector2.up * explosionForce * upliftWearoff;
        body.AddForce(upliftForce);
    }
}

public class GunController : MonoBehaviour {
    // 활성화 여부.
    public static bool isActivate = false;

    // 현재 장착된 총
    [SerializeField]
    private Gun currentGun;

    // 연사 속도 계산
    private float currentFireRate;

    // 상태 변수
    private bool isReload = false;
    [HideInInspector]
    public bool isFineSightMode = false;
    public bool Cancle_FireRateLimit = false;

    private float A;

    // 본래 포지션 값.
    private Vector3 originPos;

    // 효과음 재생
    private AudioSource audioSource;

    // 레이저 충돌 정보
    public RaycastHit hitInfo;

    [SerializeField]
    private LayerMask layerMask;

    // 필요한 컴포넌트
    private PlayerController thePlayerController;

    // 피격 이펙트.
    [SerializeField]
    private GameObject hit_effect_prefab;

    void Start() {
        A = currentGun.fireRate;
        originPos = currentGun.transform.localPosition;
        audioSource = GetComponent<AudioSource>();
        thePlayerController = GetComponent<PlayerController>();
    }

    void Update() {
        GunFireRateCalc();
        TryReload();
        TryFire();
        Cancle_Limit();
    }

    private void Cancle_Limit() {
        if (Input.GetKeyDown(KeyCode.L)) {
            Cancle_FireRateLimit = !Cancle_FireRateLimit;
        }
        if (Cancle_FireRateLimit) {
            currentGun.fireRate = 0.01f;
        } else {
            currentGun.fireRate = A;
        }

    }

    // 연사속도 재계산
    private void GunFireRateCalc() {
        if (currentFireRate > 0)
            currentFireRate -= Time.deltaTime;
    }

    // 발사 시도
    private void TryFire() {
        if (Input.GetButtonDown("Fire1") && currentFireRate <= 0 && !isReload) {
            Fire();
        }
    }

    // 발사 전 계산.
    private void Fire() {
        if (!isReload) {
            if (currentGun.currentBulletCount > 0)
                Shoot();
            else {
                StartCoroutine(ReloadCoroutine());
            }
        }
    }

    // 발사 후 계산.
    private void Shoot() {
        if (!Cancle_FireRateLimit)
            currentGun.currentBulletCount--;
        currentFireRate = currentGun.fireRate; // 연사 속도 재계산.
        Shooting();
        currentGun.anim.SetTrigger("Fire");
        currentGun.muzzleFlash.SetTrigger("Fire");
        //PlaySE(currentGun.fire_Sound);
        //currentGun.muzzleFlash.Play();
        StopAllCoroutines();
    }

    private void Shooting() {
        var bullet = Instantiate(currentGun.Bullet, currentGun.barrelLocation.position +
            new Vector3(Random.Range(-GetAccuracy() - currentGun.accuracy, GetAccuracy() + currentGun.accuracy),
                Random.Range(-GetAccuracy() - currentGun.accuracy, GetAccuracy() + currentGun.accuracy),
                0), currentGun.transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * currentGun.BulletPower);
        var casing = Instantiate(currentGun.Cartridge, currentGun.cartridgeExitLocation.position, currentGun.cartridgeExitLocation.rotation) as GameObject;
        casing.GetComponent<Rigidbody2D>().AddExplosionForce(10f, (currentGun.cartridgeExitLocation.position - currentGun.cartridgeExitLocation.right * 0.3f - currentGun.cartridgeExitLocation.up * 0.3f), 1f);
        casing.GetComponent<Rigidbody2D>().AddTorque(Random.Range(1f, 10f));
    }

    // 재장전 시도
    private void TryReload() {
        if (Input.GetKeyDown(KeyCode.R) && !isReload && currentGun.currentBulletCount < currentGun.reloadBulletCount) {
            StartCoroutine(ReloadCoroutine());
        }
    }

    public void CancelReload() {
        if (isReload) {
            StopAllCoroutines();
            isReload = false;
        }
    }

    // 재장전
    IEnumerator ReloadCoroutine() {
        isReload = true;
        //currentGun.anim.SetTrigger("Reload");
        yield return new WaitForSeconds(currentGun.reloadTime);
        currentGun.currentBulletCount = currentGun.reloadBulletCount;
        isReload = false;
    }

    // 사운드 재생.
    private void PlaySE(AudioClip _clip) {
        audioSource.clip = _clip;
        audioSource.Play();
    }

    public Gun GetGun() {
        return currentGun;
    }

    public float GetAccuracy() {
        float gunAccuracy;
        if (thePlayerController.isWalk)
            gunAccuracy = 0.046f;
        if (thePlayerController.isRun)
            gunAccuracy = 0.056f;
        /*else if (thePlayerController.isCrouch == true)
            gunAccuracy = 0.015f;*/
        //정조준 정확도: gunAccuracy = 0.001f;
        else
            gunAccuracy = 0.035f;

        return gunAccuracy;
    }
}