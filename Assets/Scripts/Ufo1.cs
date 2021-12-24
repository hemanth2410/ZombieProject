using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Ufo1 : MonoBehaviour
{

    GameManager gmInst;

    [SerializeField] private LayerMask hitLayer;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float range;



    [SerializeField] float stunDuration;
    private float timeBetweenShots;

    [SerializeField] private float startTimeBtwShots;


    private bool canShoot;
    Animator UFOanim;
    Animator Avataranim;


    GameObject _beam;
    Material beamMat;


    [ColorUsage(true, true)]
    [SerializeField] Color stunBeamColor;
    [ColorUsage(true, true)]
    [SerializeField] Color normalBeamColor;

    [SerializeField] float normalBeamSpeed;
    [SerializeField] float activeBeamSpeed;

    Expression expScript;

    private int uFOIntroParamID;
    private int uFOStunParamID;
    private int avatarStunParamID;
    private int avatarIntroParamID;
    private int avatarWinParamID;
    private int avatarDeadParamID;
    private int uFOExitParamID;
    private int uFODeadParamID;
    private int uFOPlantParamID;

    private GameObject currentUFO;
    private GameObject currentAvatar;
    private GameObject currentBeam;

    [SerializeField] private Transform uFOTransform;
    [SerializeField] private Transform avatarTransform;
    [SerializeField] private Transform beamTransform;

    Vector3 spawnOffset;

    [SerializeField] GameObject confettiA;
    [SerializeField] GameObject confettiB;
    [SerializeField] GameObject avatarEffect;
    Material avatarEffectMat;


    [SerializeField] Color coinColor;   // yellow
    [SerializeField] Color stunColor;   // red
    [SerializeField] Color normalColor;   // blue

    [TextArea(3, 10)]
    public string[] instructionData;

    private List<string> instruction;
    bool canTapToRemoveinst = false;
    int instIndex = 0;
    Rigidbody rb;
    LevelManager lmInst;

    [SerializeField] string looseM = "Be Careful where you plant!";
    BoxCollider playerCollider;

    [SerializeField] Transform topPoint;
    [SerializeField] Transform botPoint;
    [SerializeField] Transform movableGFX;

    float moveUpValue = 0.1f;
    float moveDownValue = 1.5f;

    [SerializeField] Transform[] BeamChildren;
    Vector3 currentPos;
    [SerializeField] GameObject impactEffect;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<BoxCollider>();
        UFOanim = uFOTransform.GetComponent<Animator>();
        Avataranim = avatarTransform.GetComponent<Animator>();
        avatarEffectMat = avatarEffect.GetComponent<Renderer>().material;


    }

    private void OnEnable()
    {

        TouchEvents.OnHold += HoldShoot;
        TouchEvents.EndHold += HoldEnd;
        TouchEvents.OnTap += TapInput;
        GameEvents.current.OnLevelWin += WinParticles;
        GameEvents.current.OnLevelLoose += Loose;
        //GameEvents.current.OnHouseHit += UFOHit;


    }
    private void OnDisable()
    {

        TouchEvents.OnHold -= HoldShoot;
        TouchEvents.EndHold -= HoldEnd;
        TouchEvents.OnTap -= TapInput;
        GameEvents.current.OnLevelWin -= WinParticles;
        GameEvents.current.OnLevelLoose -= Loose;
        // GameEvents.current.OnHouseHit -= UFOHit;

    }
    private void Start()
    {
        gmInst = GameManager.current;
        lmInst = LevelManager.current;
        uFOIntroParamID = Animator.StringToHash("start");
        uFOStunParamID = Animator.StringToHash("stunned");
        uFOExitParamID = Animator.StringToHash("end");
        uFODeadParamID = Animator.StringToHash("dead");
        avatarStunParamID = Animator.StringToHash("stun");
        avatarIntroParamID = Animator.StringToHash("start");
        avatarWinParamID = Animator.StringToHash("win");
        avatarDeadParamID = Animator.StringToHash("dead");
        uFOPlantParamID = Animator.StringToHash("plant");

        instruction = new List<string>();

        Init();
        timeBetweenShots = 0f;
        canShoot = true;


        spawnOffset = new Vector3(0.5f, 2.85f, 0);

       

        if (_beam != null)
        {

            beamMat.SetFloat("_isActive", 0);

        }


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canTapToRemoveinst)
            {

                canTapToRemoveinst = false;
                GameEvents.current.RemoveInstruction();
                StartCoroutine(InstructionDelay(2.5f));

            }
        }
        currentPos = transform.position;

        //==------------------------TO rEMOVE  =============--------------------//
        if (Input.GetKey(KeyCode.Space) && canShoot)
        {
            //  PlayerDown();
            timeBetweenShots -= Time.deltaTime;
            if (_beam != null)
            {

                beamMat.SetFloat("_isActive", 1);
                for (int i = 1; i < BeamChildren.Length; i++)
                {
                    BeamChildren[i].gameObject.SetActive(true);
                }
            }


            if (timeBetweenShots <= 0)
            {
                RaycastHit hit;

                if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, range, hitLayer))
                {
                    Debug.DrawRay(currentPos, shootPoint.forward * hit.distance, Color.yellow);

                    if (hit.collider.gameObject.GetComponent<Ground>() != null)
                    {

                        GameEvents.current.UFOShoot(hit.collider.transform.position + spawnOffset);
                        UFOanim.SetTrigger(uFOPlantParamID);

                        if (currentPos.y >= 13f)
                        {
                            transform.position = new Vector3(transform.position.x, 13f, transform.position.z);

                        }
                        else
                        {
                            transform.DOMoveY(currentPos.y + moveUpValue, 0.5f).SetEase(Ease.OutBack);
                        }
                        hit.transform.GetComponent<Collider>().enabled = false;
                    }

                    if (hit.collider.gameObject.GetComponent<Enemy>() != null)
                    {
                        hit.transform.GetComponent<Enemy>().GotHit();
                        StartCoroutine(stunUFO());
                        ChangeFlashColor(stunColor);


                        if (currentPos.y <= 5f)
                        {

                            transform.position = new Vector3(transform.position.x, 5f, transform.position.z);
                        }
                        else
                        {
                            transform.DOMoveY(currentPos.y - moveDownValue, 0.5f).SetEase(Ease.OutBack);
                        }


                    }
                    if (hit.collider.gameObject.GetComponent<MilitaryBase>() != null)
                    {
                        hit.transform.GetComponent<MilitaryBase>().Shoot(this.transform);
                        ChangeFlashColor(stunColor);
                        UFOHit();
                    }
                    if (hit.collider.gameObject.GetComponent<MilitaryPopup>() != null)
                    {
                        hit.transform.GetComponent<MilitaryPopup>().Shoot(this.transform);
                        ChangeFlashColor(stunColor);
                        UFOHit();
                    }
                    if (hit.collider.gameObject.GetComponent<Coin>() != null)
                    {
                        GameEvents.current.UFOShoot(hit.collider.transform.position + spawnOffset);
                        hit.transform.GetComponent<Coin>().AddCoinFlash(hit.collider.transform.position + spawnOffset);
                        ChangeFlashColor(coinColor);
                        hit.transform.GetComponent<Collider>().enabled = false;
                    }
                }

                timeBetweenShots = startTimeBtwShots;
            }

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //PlayerUp();
            timeBetweenShots = 0;
            if (_beam != null)
            {

                beamMat.SetFloat("_isActive", 0);
                for (int i = 1; i < BeamChildren.Length; i++)
                {
                    BeamChildren[i].gameObject.SetActive(false);
                }
            }


        }
        else
        {
            Debug.DrawRay(currentPos, shootPoint.forward * range, Color.white);


        }

    }

    private void Init()
    {
        currentAvatar = gmInst.avatars[ShopManager.selectedAvatarIndex].ItemPrefab;
        currentUFO = gmInst.uFOs[ShopManager.selectedUFOIndex].ItemPrefab;
        currentBeam = gmInst.beams[ShopManager.selectedBeamIndex].ItemPrefab;

        if (GameManager.currentLevel == 0)             // 2* basic tutorial
        {


            instruction.Add(instructionData[0]);
            instruction.Add(instructionData[1]);

        }
        if (GameManager.currentLevel == 4)             // popup
        {

            instruction.Add(instructionData[2]);
        }
        if (GameManager.currentLevel == 9)             // military
        {

            instruction.Add(instructionData[3]);
        }
        //if (GameManager.currentLevel == 14)             // military popup   
        //{

        //    instruction.Add(instructionData[4]);
        //}

        StartCoroutine(SpawnGraphics());
    }

    IEnumerator SpawnGraphics()
    {
        SpawnUFOGraphics();
        yield return new WaitForSeconds(1f);

        SpawnAvatarGraphics();
        SpawnBeamGraphics();
        StartCoroutine(InstructionDelay(1f));


    }

    private void TapInput(Vector2 pos)
    {

        if (canTapToRemoveinst)
        {
            canTapToRemoveinst = false;
            GameEvents.current.RemoveInstruction();
            StartCoroutine(InstructionDelay(2.5f));

        }
    }

    IEnumerator InstructionDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (instIndex < instruction.Count)
        {
            ShowInst(instIndex);
            canTapToRemoveinst = true;
        }
        else { canTapToRemoveinst = false; }

    }

    private void ShowInst(int index)
    {

        GameEvents.current.ShowInstruction(instruction[index]);

        instIndex++;

    }


    public void SpawnAvatarGraphics()
    {

        GameObject _currentAvatar = Instantiate(currentAvatar, avatarTransform.position, avatarTransform.localRotation, avatarTransform);

        expScript = _currentAvatar.GetComponent<Expression>();

        Avataranim.SetTrigger(avatarIntroParamID);

    }
    public void SpawnUFOGraphics()
    {
        uFOTransform.localScale = Vector3.zero;
        Instantiate(currentUFO, uFOTransform.position, Quaternion.identity, uFOTransform);
        UFOanim.SetTrigger(uFOIntroParamID);
        AudioManager.PlayLevelStartAudio();
    }
    public void SpawnBeamGraphics()
    {
        _beam = Instantiate(currentBeam, beamTransform.position, Quaternion.Euler(0, 40, 0), beamTransform);
        BeamChildren = _beam.GetComponentsInChildren<Transform>();
        for (int i = 1; i < BeamChildren.Length; i++)
        {
            BeamChildren[i].gameObject.SetActive(false);
        }

        beamMat = _beam.GetComponent<Renderer>().material;

        beamMat.SetColor("_tint", normalBeamColor);
    }

    private void HoldShoot(Vector2 pos)
    {
        // PlayerDown();

        if (_beam != null)
        {

            beamMat.SetFloat("_isActive", 1);
        }

        if (canShoot && pos.y > 130)
        {


            timeBetweenShots -= Time.deltaTime;


            if (timeBetweenShots <= 0)
            {
                RaycastHit hit;

                if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, range, hitLayer))
                {
                    Debug.DrawRay(transform.position, shootPoint.forward * hit.distance, Color.yellow);

                    if (hit.collider.gameObject.GetComponent<Ground>() != null)
                    {


                        GameEvents.current.UFOShoot(hit.collider.transform.position + spawnOffset);
                        hit.transform.GetComponent<Collider>().enabled = false;
                        if (currentPos.y >= 13f)
                        {
                            transform.position = new Vector3(transform.position.x, 13f, transform.position.z);

                        }
                        else
                        {
                            transform.DOMoveY(currentPos.y + moveUpValue, 0.5f).SetEase(Ease.OutBack);
                        }
                        UFOanim.SetTrigger(uFOPlantParamID);
                        if (GameManager.enableVibrate)
                        {
                            Vibration.Vibrate(30);
                        }

                    }


                    if (hit.collider.gameObject.GetComponent<Enemy>() != null)
                    {
                        hit.transform.GetComponent<Enemy>().GotHit();
                        StartCoroutine(stunUFO());
                        ChangeFlashColor(stunColor);
                        if (currentPos.y <= 5f)
                        {

                            transform.position = new Vector3(transform.position.x, 5f, transform.position.z);
                        }
                        else
                        {
                            transform.DOMoveY(currentPos.y - moveDownValue, 0.5f).SetEase(Ease.OutBack);
                        }
                        if (GameManager.enableVibrate)
                        {
                            Vibration.Vibrate(1500);   // stun duration
                        }

                    }

                    if (hit.collider.gameObject.GetComponent<MilitaryBase>() != null)
                    {
                        hit.transform.GetComponent<MilitaryBase>().Shoot(this.transform);
                        ChangeFlashColor(stunColor);
                        UFOHit();
                        if (GameManager.enableVibrate)
                        {
                            Vibration.Vibrate(500);   // stun duration
                        }
                    }
                    if (hit.collider.gameObject.GetComponent<MilitaryPopup>() != null)
                    {
                        hit.transform.GetComponent<MilitaryPopup>().Shoot(this.transform);
                        ChangeFlashColor(stunColor);
                        UFOHit();
                        if (GameManager.enableVibrate)
                        {
                            Vibration.Vibrate(500);   // stun duration
                        }
                    }

                    if (hit.collider.gameObject.GetComponent<Coin>() != null)
                    {
                        GameEvents.current.UFOShoot(hit.collider.transform.position + spawnOffset);
                        hit.transform.GetComponent<Coin>().AddCoinFlash(hit.collider.transform.position + spawnOffset);
                        ChangeFlashColor(coinColor);
                        hit.transform.GetComponent<Collider>().enabled = false;
                    }
                }

                timeBetweenShots = startTimeBtwShots;
            }

        }
        else
        {


            return;
        }

    }

    private void HoldEnd(Vector2 pos)
    {
        // PlayerUp();
        timeBetweenShots = startTimeBtwShots;
        if (_beam != null)
        {

            beamMat.SetFloat("_isActive", 0);
        }

    }

    IEnumerator stunUFO()
    {
        canShoot = false;

        UFOanim.SetBool(uFOStunParamID, true);
        Avataranim.SetBool(avatarStunParamID, true);
        expScript.SadExp();
        if (_beam != null)
        {
            beamMat.SetColor("_tint", stunBeamColor);
        }
        AudioManager.PlayStunAudio();
        yield return new WaitForSeconds(stunDuration);
        expScript.HappyExp();
        canShoot = true;
        if (_beam != null)
        {
            beamMat.SetColor("_tint", normalBeamColor);
        }

        UFOanim.SetBool(uFOStunParamID, false);
        Avataranim.SetBool(avatarStunParamID, false);


    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<OutPortal>() != null)
        {

            GameEvents.current.LevelEndTrigger();

        }
        if (other.transform.parent.GetComponent<Enemy>() != null)
        {

            //GameEvents.current.HouseHit();
            // HouseHit();
            //GameEvents.current.LevelEndTrigger();   // its will slow level move
            GameObject effectInst = Instantiate(impactEffect, transform.position, transform.rotation);
            AudioManager.PlayExplosionAudio();
            Destroy(effectInst, 2f);
            Dead(looseM);

        }
    }

    public void WinParticles(int starsAchieved, int starsAccum, TreeTypes currentTree, int score, int arg5, int arg6)
    {
        StartCoroutine(Win());

    }

    IEnumerator Win()
    {
        expScript.HappyExp();
        confettiA.SetActive(true);
        confettiB.SetActive(true);

        yield return new WaitForSeconds(1f);
        _beam.SetActive(false);
        avatarEffect.gameObject.SetActive(false);
        Avataranim.SetBool(avatarWinParamID, true);
        UFOanim.SetBool(uFOExitParamID, true);
    }

    public void ChangeFlashColor(Color color)
    {
        StartCoroutine(ChangeAvatarFlash(color));

    }
    public void FlashIntro()
    {
        StartCoroutine(AvatarFlash());

    }
    IEnumerator AvatarFlash()
    {

        avatarEffectMat.SetColor("_tint", normalColor);

        for (float a = 0; a <= 1; a += 0.02f)
        {
            avatarEffectMat.SetFloat("_visibility", a);
            yield return null;
        }

    }
    IEnumerator ChangeAvatarFlash(Color color)
    {


        avatarEffectMat.SetColor("_tint", color);
        yield return new WaitForSeconds(stunDuration);
        avatarEffectMat.SetColor("_tint", normalColor);


    }

    // stun
    private void UFOHit()
    {
        canShoot = false;

        UFOanim.SetBool(uFOStunParamID, true);
        Avataranim.SetBool(avatarDeadParamID, true);
        expScript.SadExp();
        playerCollider.enabled = false;
        if (_beam != null)
        {
            beamMat.SetColor("_tint", stunBeamColor);
        }
        AudioManager.PlayStunAudio();

    }


    public void Loose(int a, string msg)
    {
        expScript.SadExp();
        Destroy(this.gameObject, 2f);
    }


    // dead
    public void Dead(string looseMessage)
    {

        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddForce(7, 10, 3, ForceMode.Impulse);
        UFOanim.SetBool(uFODeadParamID, true);
        beamTransform.gameObject.SetActive(false);

        GameEvents.current.LevelLoose(lmInst.levelScore, looseMessage);
        AudioManager.PlayLooseAudio();
    }
    // house hit
    //public void HouseHit()
    //{

    //    UFOanim.SetBool(uFODeadParamID, true);
    //    beamTransform.gameObject.SetActive(false);

    //    GameEvents.current.LevelLoose(lmInst.levelScore, looseMessage);
    //    AudioManager.PlayLooseAudio();
    //}


    //private void PlayerDown()
    //{
    //    movableGFX.transform.position = Vector3.Lerp(movableGFX.transform.position, botPoint.position, 0.2f);
    //}
    //private void PlayerUp()
    //{
    //    movableGFX.transform.position = Vector3.Lerp(movableGFX.transform.position, topPoint.position, 0.4f);
    //}


}

