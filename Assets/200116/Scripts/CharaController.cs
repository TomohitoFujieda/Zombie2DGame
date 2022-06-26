using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharaController : MonoBehaviour
{
    int hp;

    public double mapCoordinateX;
    public double mapCoordinateY;
    public float walkingTime;
    float delta;
    float f;
    double worldX;
    double worldY;
    double blkSz;
    public bool keyDown;
    public bool walking;
    bool walkingEnd;
    bool firstUpdate;
    public bool endDamageAnim;
    public bool isDead;
    public bool leftDown;
    public bool leftUp;
    public bool rightDown;
    public bool rightUp;
    public bool pass;
    SpriteRenderer spriteRenderer;
    MapController mapController;
    TurnController turnController;
    int currentDirX;
    int currentDirY;
    int order;
    Animator animator;
    GameObject currentBlock;
    Vector2 tempPosition;

    void Spawn()
    {
        this.order = turnController.AddCharactor();
    }

    void Dead()
    {
        Debug.Log("Dead");
        this.isDead = true;
        this.gameObject.name = "DeadChar";
        currentBlock.GetComponent<BlockController>().passable = true;
    }

    public void SetHp(int n)
    {
        hp = n;
    }

    public void Attack(int damage, double coordinateX, double coordinateY)
    {
        GameObject obj = GameObject.Find(string.Format("{0}_{1}_char", coordinateX, coordinateY));
        if (obj == null) return;
        CharaController chara = obj.GetComponent<CharaController>();
        chara.CalcHp(-damage);
        Debug.Log(chara.hp);
    }

    void PlayAttackAnim()
    {

    }

    public int CalcHp(int d)
    {
        this.hp += d;
        if (d < 0)
        {
            Debug.Log("AnimStart");
            DamageAnim();
        }
        return this.hp;
    }

    public int GetMyOrder()
    {
        return order;
    }

    void StopOn(int d, int dir_x, int dir_y)
    {
        if (!(dir_x == 0 || dir_x == -1 || dir_x == 1 || dir_y == 0 || dir_y == -1 || dir_x == 1)) return;
        double destX = this.mapCoordinateX + (double)(d * dir_x);
        double destY = this.mapCoordinateY + (double)(d * dir_y);
        this.currentBlock = this.mapController.GetBlockByCoordinate(destX, destY);
        this.transform.position = new Vector2((float)(-this.blkSz / 2 * (destX - destY)), (float)(-this.blkSz / 4 * (destX + destY) + 75));
        this.tempPosition = new Vector2(this.transform.position.x, this.transform.position.y);
        UpdateCoordinates();
    }

    void UpdateCoordinates()
    {
        worldX = this.transform.position.x;
        worldY = this.transform.position.y;
        this.mapCoordinateX = -(worldX + 2 * (worldY - 75)) / 100;
        this.mapCoordinateY = (worldX - 2 * (worldY - 75)) / 100;
        this.currentBlock = mapController.GetBlockByCoordinate(mapCoordinateX, mapCoordinateY);
        if (this.currentBlock == null) return;
        this.currentBlock.GetComponent<BlockController>().passable = false;
        this.gameObject.name = string.Format("{0}_{1}_char", mapCoordinateX, mapCoordinateY);
    }

    void TurnTo(int nextX, int nextY)
    {
        this.currentDirX = nextX;
        this.currentDirY = nextY;
    }
    
    void PlayWalkAnim()
    {
        if (this.currentDirX < 0 && this.currentDirY == 0)
        {
            this.animator.SetBool("rightUpWalk", true);
        }
        else if (this.currentDirX == 0 && this.currentDirY > 0)
        {
            this.animator.SetBool("rightDownWalk", true);
        }
        else if (this.currentDirX > 0 && this.currentDirY == 0)
        {
            this.animator.SetBool("leftDownWalk", true);
        }
        else if (this.currentDirX == 0 && this.currentDirY < 0)
        {
            this.animator.SetBool("leftUpWalk", true);
        }
    }

    void WalkingAnim()
    {
        
        this.tempPosition.x += (currentDirY < 0 || currentDirX > 0) ?
            (float)(blkSz * 0.5 * delta * -1) :
            (float)(blkSz * 0.5 * delta * 1);
        this.tempPosition.y += (currentDirY > 0 || currentDirX > 0) ?
            (float)(blkSz * 0.25 * delta * -1) :
            (float)(blkSz * 0.25 * delta * 1);
            this.transform.position = this.tempPosition;
    }

    void StopIdle()
    {
        if (this.currentDirX < 0 && this.currentDirY == 0)
        {
            this.animator.SetBool("rightUpIdle", false);
        }
        else if (this.currentDirX == 0 && this.currentDirY > 0)
        {
            this.animator.SetBool("rightDownIdle", false);
        }
        else if (this.currentDirX > 0 && this.currentDirY == 0)
        {
            this.animator.SetBool("leftDownIdle", false);
        }
        else if (this.currentDirX == 0 && this.currentDirY < 0)
        {
            this.animator.SetBool("leftUpIdle", false);
        }
    }

    void TurnToIdle()
    {
        if (this.currentDirX == -1 && this.currentDirY == 0)
        {
            this.animator.SetBool("rightUpWalk", false);
        }
        else if (this.currentDirX == 0 && this.currentDirY == 1)
        {
            this.animator.SetBool("rightDownWalk", false);
        }
        else if (this.currentDirX == 1 && this.currentDirY == 0)
        {
            this.animator.SetBool("leftDownWalk", false);
        }
        else if (this.currentDirX == 0 && this.currentDirY == -1)
        {
            this.animator.SetBool("leftUpWalk", false);
        }

        if (this.currentDirX < 0 && this.currentDirY == 0)
        {
            this.animator.SetBool("rightUpIdle", true);
        }
        else if (this.currentDirX == 0 && this.currentDirY > 0)
        {
            this.animator.SetBool("rightDownIdle", true);
        }
        else if (this.currentDirX > 0 && this.currentDirY == 0)
        {
            this.animator.SetBool("leftDownIdle", true);
        }
        else if (this.currentDirX == 0 && this.currentDirY < 0)
        {
            this.animator.SetBool("leftUpIdle", true);
        }
    }

    void DamageAnim()
    {
        this.animator.SetBool("damage", true);
    }

    IEnumerator FadeOut()
    {
        
        for (float f = 1.0f; f > -0.1f; f -= 0.1f)
        {
            Debug.Log(f);
            var tempcolor = spriteRenderer.color;
            tempcolor.a = f;
            spriteRenderer.color = tempcolor;
            yield return new WaitForFixedUpdate();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        f = 0;
        keyDown = false;
        walking = false;
        walkingEnd = false;
        worldX = this.transform.position.x;
        worldY = this.transform.position.y;
        blkSz = 100;
        walkingTime = 50;
        delta = 1 / walkingTime;
        this.mapCoordinateX = -(worldX + 2 * (worldY - blkSz * (3/4))) / blkSz;
        this.mapCoordinateY = (worldX - 2 * (worldY - blkSz * (3/4))) / blkSz;
        this.tempPosition = new Vector2((float)worldX, (float)worldY);
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.spriteRenderer.sortingLayerName = "Character";

        this.animator = GetComponent<Animator>();

        this.gameObject.name = string.Format("{0}_{1}_char", mapCoordinateX, mapCoordinateY);

        this.mapController = GameObject.Find("MapDirector").GetComponent<MapController>();
        this.turnController = GameObject.Find("TurnController").GetComponent<TurnController>();

        this.currentDirX = 1;
        this.currentDirY = 0;
        this.firstUpdate = true;
        this.endDamageAnim = false;
        this.leftDown = false;
        this.leftUp = false;
        this.rightDown = false;
        this.rightUp = false;
    }


    void FixedUpdate()
    {
        if (walking)
        {
            if (f < 1)
            {
                f += delta;
                WalkingAnim();
            }
            else
            {
                f = 0;
                walkingEnd = true;
                walking = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (firstUpdate)
        {
            Spawn();
            Debug.Log(order);
            UpdateCoordinates();
            this.firstUpdate = false;
        }

        if (endDamageAnim)
        {
            this.animator.SetBool("damage", false);
        }

        if (isDead)
        {
            pass = true;
        }
        
        if (hp <= 0 && !isDead && endDamageAnim)
        {
            StartCoroutine("FadeOut");
            Dead();
        }

        if (turnController.IsMyTurn(order))
        {
            if (walkingEnd)
            {
                StopOn(1, currentDirX, currentDirY);
                TurnToIdle();
                keyDown = false;
                walkingEnd = false;
                turnController.moveToNext = true;
            }

            if (!keyDown && !walking)
            {
                if (leftUp)
                {
                    leftUp = false;
                    StopIdle();
                    keyDown = true;
                    TurnTo(0, -1);
                    if (mapController.IsPassableBlk(mapCoordinateX + currentDirX, mapCoordinateY + currentDirY))
                    {
                        this.currentBlock.GetComponent<BlockController>().passable = true;
                        PlayWalkAnim();
                        walking = true;
                    }
                    else
                    {
                        keyDown = false;
                    }
                }
                else if (leftDown)
                {
                    leftDown = false;
                    StopIdle();
                    keyDown = true;
                    TurnTo(1, 0);
                    if (mapController.IsPassableBlk(mapCoordinateX + currentDirX, mapCoordinateY + currentDirY))
                    {
                        this.currentBlock.GetComponent<BlockController>().passable = true;
                        PlayWalkAnim();
                        walking = true;
                    }
                    else
                    {
                        keyDown = false;
                    }
                }
                else if (rightUp)
                {
                    rightUp = false;
                    StopIdle();
                    keyDown = true;
                    TurnTo(-1, 0);
                    if (mapController.IsPassableBlk(mapCoordinateX + currentDirX, mapCoordinateY + currentDirY))
                    {
                        this.currentBlock.GetComponent<BlockController>().passable = true;
                        PlayWalkAnim();
                        walking = true;
                    }
                    else
                    {
                        keyDown = false;
                    }
                }
                else if (rightDown)
                {
                    rightDown = false;
                    StopIdle();
                    keyDown = true;
                    TurnTo(0, 1);
                    if (mapController.IsPassableBlk(mapCoordinateX + currentDirX, mapCoordinateY + currentDirY))
                    {
                        this.currentBlock.GetComponent<BlockController>().passable = true;
                        PlayWalkAnim();
                        walking = true;
                    }
                    else
                    {
                        keyDown = false;
                    }
                }
                else if (pass)
                {
                    Debug.Log("Passed");
                    pass = false;
                    keyDown = false;
                    turnController.moveToNext = true;
                }
            }
        }
    }
}
