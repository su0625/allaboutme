using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[RequireComponent(typeof(PhotonView))]
public class PhotonPlayer : MonoBehaviourPun,IPunObservable
{
    private bool isMoving,isRunning;
    public float moveX, moveY, lastMoveX;
    public float walkSpeed = 3f, runSpeed = 5f;
    private Vector2 moveDir;
    private Rigidbody2D rb;
    private Animator animator;
    [HideInInspector] public string currentAnim;
    [HideInInspector] public string nextAnim;
    [SerializeField] private GameObject e_obj;
    private bool can_e;
    private string e_targetSceneName;
    [SerializeField] private PlayerOutfitData outfitData;
    //Hat HairFront HairBack Face ArmUp BodyUp BodyDown ArmDown LegUp LegDown
    [SerializeField] private PlayerData playerData;
    public Text nameComponment;
    private PhotonView pv;

    public SpriteRenderer[] bodyparts;
    public int[] outfitNums = new int[10];
    private int[] outfitNums_temp = new int[10];

    //total 10 parts (no heads
    //Hat HairFront HairBack Face ArmUp BodyUp BodyDown ArmDown LegUp LegDown

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponentInChildren<Animator>();
        pv = this.GetComponent<PhotonView>();

        PhotonNetwork.LocalPlayer.NickName = playerData.playerName;
        if(PhotonNetwork.CurrentRoom == null)
        {
            nameComponment.text = playerData.playerName;
        }
        else
        {
            nameComponment.text = pv.Owner.NickName;
        }

        ChangeAnimState("idle");
        e_obj.SetActive(false);
        can_e = false;

        if(!pv.IsMine)
        {
            Destroy(rb);
        }

        for (int i = 0; i < bodyparts.Length; i++)
        {
            int targetNum = outfitData.OutfitNums[i];
            outfitNums[i] = targetNum;
            outfitNums_temp[i] = targetNum;
            bodyparts[i].sprite = outfitData.outfitSprites[i].sprites[targetNum];
        }
    }

    private void Update() 
    {
        if(pv.IsMine)
        {
            MoveInput();
            E_Input();
            DatabaseUpdate_Outfit();
        }
        ChangeAnimState(nextAnim);

        for(int i=0; i < bodyparts.Length; i++)
        {
            ChangeOutfit(i);
        }
    }

    private void FixedUpdate() 
    {
        if(pv.IsMine)
        {
            MoveFunc();
        }
    }

    private void MoveInput()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;

        isMoving = Input.GetButton("Horizontal") || Input.GetButton("Vertical");
        isRunning = Input.GetButton("Run");
    }

    private void MoveFunc()
    {
        if (isMoving && !isRunning)
        {
            //walk
            rb.velocity = moveDir * walkSpeed;
            nextAnim = "walk";
        }
        else if (isMoving && isRunning)
        {
            //run
            rb.velocity = moveDir * runSpeed;
            nextAnim = "run";
        }
        else
        {
            //idle
            rb.velocity = Vector2.zero;
            nextAnim = "idle";
        }

        FlipFunc();
    }

    private void FlipFunc()
    {
        if(lastMoveX != moveX)
        {
            lastMoveX = moveX;

            if(lastMoveX == 1)
            {
                this.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (lastMoveX == -1)
            {
                this.transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
    }

    private void ChangeAnimState(string newAnimation)
    {
        if(currentAnim != newAnimation)
        {
            animator.Play(newAnimation);
            currentAnim = newAnimation;
        }
    }

    private void E_Input()
    {
        if (can_e && Input.GetButtonDown("Interact") )
        {
            print("E interact Test");

            if (e_targetSceneName == "")
            {
                Debug.LogError("TargetSceneName is Empty！！！");
                return;
            }
            else if(e_targetSceneName == "GameRoom")
            {
                SceneManager.LoadScene(e_targetSceneName);
            }
            else
            {
                //GameObject.Find("Canvas").SendMessage("LoadLevel", e_targetSceneName);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "E Building")
        {
            e_obj.SetActive(true);
            can_e = true;
            e_targetSceneName = other.GetComponent<E_BuildingScript>().targetSceneName;
            print(e_targetSceneName);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "E Building")
        {
            e_obj.SetActive(false);
            can_e = false;
            e_targetSceneName = null;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.nextAnim);
            stream.SendNext(this.transform.position);
            stream.SendNext(this.transform.rotation);

            for(int i = 0; i < bodyparts.Length; i++)
            {
                stream.SendNext(this.outfitNums[i]);
            }
        }
        else if (stream.IsReading)
        {
            this.nextAnim = (string)stream.ReceiveNext();
            this.transform.position = (Vector3)stream.ReceiveNext();
            this.transform.rotation = (Quaternion)stream.ReceiveNext();

            for (int i = 0; i < bodyparts.Length; i++)
            {
                this.outfitNums[i] = (int)stream.ReceiveNext();
            }
        }
    }

    private void ChangeOutfit(int whichBodypart)
    {
        int targetOutfitNum = outfitNums[whichBodypart];

        bodyparts[whichBodypart].sprite = outfitData.outfitSprites[whichBodypart].sprites[targetOutfitNum];
    }

    private void DatabaseUpdate_Outfit()
    {
        for(int i=0; i < outfitNums.Length; i++)
        {
            if (outfitNums_temp[i] != outfitNums[i])
            {
                outfitNums_temp[i] = outfitNums[i];

                int targetNum = outfitNums[i];
                outfitData.UpdateOutfitData(i,targetNum);
            }
        }
    }
}
