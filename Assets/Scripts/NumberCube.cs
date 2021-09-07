using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class NumberCube : MonoBehaviour
{     
    private SpriteRenderer _sr;
    private Rigidbody2D _rb2d;
    private TextMeshPro _tmpro;

    private float _initialMousePositionX;

    public bool ReadyToMerge { get => _readyToMerge;  private set => _readyToMerge = value; }
    private bool _readyToMerge = false;
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

        _sr.sprite = GameManager.Instance.CubeSkins[Value - 1];
    }

    protected virtual void Update()
    {


        if (_rb2d.velocity.y < -0.01f)
        {
            ReadyToMerge = false;          
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        NumberCube OtherCube = collision.gameObject.GetComponent<NumberCube>();

        
        if(GameManager.Instance.AllCubesReadyToMerge && GameManager.Instance._timer >= GameManager.Instance.WaitForReadyToMerge)
        {           
            if (OtherCube && OtherCube.Value == this.Value)
            {
                
                if (!GameManager.Instance.CubesToDestroy.Contains(this))
                {
                    GameManager.Instance.CubesToDestroy.Add(this);               
                }

                Destroy(this.gameObject);
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.Instance.CurrentCube)
        {
            GameManager.Instance.ResetSpeed();
            GameManager.Instance.CurrentCube.transform.DetachChildren();
            Destroy(GameManager.Instance.CurrentCube);
        }

        InvokeRepeating(nameof(SetReadyToMergeToTrue), GameManager.Instance.WaitForReadyToMerge, GameManager.Instance.WaitForReadyToMerge);
        _rb2d.gravityScale = 1;       
    }
    private void SetReadyToMergeToTrue()
    {
        ReadyToMerge = true;        
    }


    //private void OnMouseUp()
    //{
    //    GameObject currentCube = GameManager.Instance.CurrentCube;
    //    if (currentCube && Mathf.Abs(this._rb2d.velocity.y) > 0.1)
    //    {
    //        //if (_initialMousePositionX + GameManager.Instance.MousePosition.x != _initialMousePositionX)
    //        //{
    //            NumberCube[] cubes = currentCube.GetComponentsInChildren<NumberCube>();
    //            float x1 = cubes[cubes.GetLowerBound(0)].transform.position.x;
    //            float x2 = cubes[cubes.GetUpperBound(0)].transform.position.x;
    //            float temp = cubes[cubes.GetLowerBound(0)].transform.position.x;

    //            cubes[cubes.GetLowerBound(0)].transform.position = new Vector2(x2, cubes[cubes.GetLowerBound(0)].transform.position.y);
    //            cubes[cubes.GetUpperBound(0)].transform.position = new Vector2(temp, cubes[cubes.GetUpperBound(0)].transform.position.y);
    //        //}
    //    }
    //}
}