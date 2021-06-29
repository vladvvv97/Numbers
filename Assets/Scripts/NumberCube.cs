using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberCube : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private BoxCollider2D[] bc2d;
    private bool _readyToMerge = false;
    
    public bool ReadyToMerge { get => _readyToMerge;  private set => _readyToMerge = value; }

    protected virtual void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    protected virtual void Start()
    {
        rb2d.sleepMode = RigidbodySleepMode2D.NeverSleep;
        rb2d.velocity = new Vector2(0, -GameManager.Instance.Speed);
    }

    protected virtual void Update()
    {
        //if (transform.position.y <= -GameManager.Instance.CamHeight + GameManager.Instance.Offset)
        //{
        //    rb2d.velocity = Vector2.zero;
        //}

        //if (rb2d.velocity.y == 0)
        //{
        //    Invoke(nameof(ReadyToMerge), GameManager.Instance.WaitForTriggerEnterCheckTime);
        //}
           
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        NumberCube OtherCube = collision.gameObject.GetComponent<NumberCube>();

        
        if(GameManager.Instance.AllCubesReadyToMerge)
        {
            if (OtherCube)
            {
                Destroy(this.gameObject);
                Destroy(collision.gameObject);
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Invoke(nameof(SetReadyToMergeToTrue), GameManager.Instance.WaitForTriggerEnterCheckTime);
        rb2d.gravityScale = 1;
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        ReadyToMerge = false;
        rb2d.constraints = RigidbodyConstraints2D.FreezePositionX & RigidbodyConstraints2D.FreezeRotation;
    }
    private void SetReadyToMergeToTrue()
    {
            ReadyToMerge = true;
    }

}