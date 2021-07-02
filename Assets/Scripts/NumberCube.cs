using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberCube : MonoBehaviour
{
    
    

    private SpriteRenderer _sr;
    private Rigidbody2D _rb2d;
    private TextMeshPro _tmpro;
    public bool ReadyToMerge { get => _readyToMerge;  private set => _readyToMerge = value; }
   [SerializeField] private bool _readyToMerge = false;
    public int Value { get => _value; private set => _value = value; }    
    private int _value;

    protected virtual void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _tmpro = GetComponentInChildren<TextMeshPro>();
    }
    protected virtual void Start()
    {
        _rb2d.sleepMode = RigidbodySleepMode2D.NeverSleep;
        _rb2d.gravityScale = 0;
        _rb2d.velocity = new Vector2(0, -GameManager.Instance.Speed);

        Value = GameManager.Instance.CubeValueTypeChance[Random.Range(0, GameManager.Instance.CubeValueTypeChance.Length)];

        _tmpro.text = Value.ToString();

        _sr.color = GameManager.Instance.CubeColors[Value - 1];
    }

    protected virtual void Update()
    {
        if (_rb2d.velocity.y < -0.1f)
        {
            ReadyToMerge = false;
        }
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        NumberCube OtherCube = collision.gameObject.GetComponent<NumberCube>();

        
        if(GameManager.Instance.AllCubesReadyToMerge)
        {
            if (OtherCube && OtherCube.Value == this.Value)
            {
                Destroy(this.gameObject);
                Destroy(collision.gameObject);
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        InvokeRepeating(nameof(SetReadyToMergeToTrue), GameManager.Instance.WaitForReadyToMerge, GameManager.Instance.WaitForReadyToMerge);
        _rb2d.gravityScale = 1;
        //_rb2d.velocity = Vector2.zero;
        //_rb2d.gravityScale = 0.5f;
        //_rb2d.constraints = RigidbodyConstraints2D.FreezeAll;

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //ReadyToMerge = false;
        //_rb2d.constraints = RigidbodyConstraints2D.FreezePositionX & RigidbodyConstraints2D.FreezeRotation;
        
    }
    private void SetReadyToMergeToTrue()
    {
        ReadyToMerge = true;
    }

}