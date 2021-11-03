using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArmaRay : MonoBehaviour
{
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//
    //PROJETIL, BALA, BULLET
    [Header("Prefabs de Munições")]
    [SerializeField] public GameObject Bullet;
    [SerializeField] public GameObject Projectile;
    [SerializeField] private GameObject CurrentProjectile;


    //FORCA DA BALA, BULLET FORCE
    [Header("Configurações Arma")]
    public float shootForce = 200f;

    //CONSTITUICAO DA ARMA, GUN STATS
    public float shootingRate; //tempo entre as disparadas
    public float fireRate; //tempo entre os tiros
    public float spread; //dispersao do tiro
    public float reloadTime; //tempo de recarregat
    public int magazineSize; //tamanho do pente
    public int bulletPerTap; //quantas balas saem por clique
    public bool allowHold; //auto / semiauto
    int bulletsLeft, bulletsShot; //quantas balas tem

    //BOOLS CHECKS
    bool shooting, readyToShoot, reloading;

    //REFERENCES
    [Header("Referências")]
    public Camera fpsCam;
    public Transform primaryWeaponPoint;
    public Transform secondaryWeaponPoint;
    public Transform firePoint;

    //GRAFICO
    public GameObject muzzleFlash;
    public TextMeshProUGUI ammoDisplay;
    public LayerMask PlayerLayerMask;

    //BUG FIXING
    public bool allowInvoke = true;
    public Vector3 mousePos;
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//

    private void Awake()
    {
        //TER CERTEZA SE O PENTE TA FULL
        bulletsLeft = magazineSize;
        readyToShoot = true;
        CurrentProjectile = Bullet;
        firePoint = primaryWeaponPoint;

    }
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//

    private void Update()
    {
        MyInput();

        //set ammo display
        if (ammoDisplay != null)
            ammoDisplay.SetText(bulletsLeft/bulletPerTap + "/" + magazineSize/bulletPerTap);
        
    }
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//
    private void MyInput()
    {
        //CHECAR SE PODE SENTA A PUA
        if (allowHold)
            shooting = Input.GetKey(KeyCode.Mouse0);
        else
            shooting = Input.GetKeyDown(KeyCode.Mouse0);

        //RECARREGAR MANUAL
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
            Reload();
        //RECARGA AUTOMATICA
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0)
            Reload();

        //ATIRANDO, SHOOTING
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            //NAO ATIROU NENHUMA, ainda
            bulletsShot = 0;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ShootPrimaryWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ShootMissile();
        }


    }
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//

    private void Shoot()
    {
        //ja atirou
        readyToShoot = false;

        //ENCONTRAR A POSICAO ACERTADA DO CURSOR USANDO RAYCAST
        //origem do ray eh o meio do player
        //aponta para a posicao do cursor
        float rayLength = 10000f;//distancia infinita onde o z aponta
        Ray ray =  Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //CHECAR SE O RAY MIRA EM ALGO
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit, rayLength, PlayerLayerMask))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(100);//just away from the player
        
        //CALCULO DA DIRECAO DA NAVE ATE O ALVO
        Vector3 directionNOSpread = targetPoint - firePoint.position;

        //CALCULO DO SPREAD
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        //direcao com spread
        Vector3 directionSpread = directionNOSpread + new Vector3(x,y,0);

        //INSTANCIAR A BALA, INSTANCIATE BULLET
        GameObject currentBullet = Instantiate(CurrentProjectile, firePoint.position, Quaternion.identity);
        //rodar a bala na direcao correta
        currentBullet.transform.forward = directionSpread.normalized;

        //ADD FORCES TO BULLET
        currentBullet.GetComponent<Rigidbody>().AddForce(directionSpread.normalized * shootForce, ForceMode.Impulse);

        //INSTANCIAR muzzleFlash
        if (muzzleFlash != null)
            Instantiate(muzzleFlash, firePoint.position, Quaternion.identity);

        //DESCONTAR DAS BALAS E MARCAR Q ATIROU
        bulletsLeft--;
        bulletsShot++;

        //RESET DO SHOOT
        if (allowInvoke)
        {
            Invoke("ResetShot", shootingRate);
            allowInvoke = false;

        }

        //SE TEM MAIS DE UMA BALA POR CLICK
        if (bulletsShot < bulletPerTap && bulletsLeft > 0)
            Invoke("Shoot", shootingRate);
    }

    private void ResetShot()
    {
        //allow shooting and invoking again
        readyToShoot = true;
        allowInvoke = true;
    }
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

    private void ShootPrimaryWeapon()
    {
        CurrentProjectile = Bullet;
        firePoint = primaryWeaponPoint;
        shootForce = 200f;
        shootingRate = 0.1f;
    }

    private void ShootMissile()
    {
        CurrentProjectile = Projectile;
        firePoint = secondaryWeaponPoint;
        shootForce = 20f;
        shootingRate = 0.5f;
    }
}
