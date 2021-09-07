using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    public static TouchController Instance;

    public static bool isSwiping;

    [SerializeField] private float deadZone = 0.5f;

    private Vector2 _startTapPosition;
    private Vector2 _endTapPosition;
    private Vector2 _deltaPosition;

    private bool _isMobile;
    [SerializeField] private bool _alreadyTouch = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        _isMobile = Application.isMobilePlatform;
    }

    private void Update()
    {
        if (!_isMobile)
        {
            if (Input.GetMouseButtonDown(0) && _alreadyTouch == false)
            {               
                _startTapPosition = GameManager.Instance.MousePosition;               
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _endTapPosition = GameManager.Instance.MousePosition;
                _deltaPosition = _endTapPosition - _startTapPosition;
                if (_deltaPosition.magnitude > deadZone)
                {
                    isSwiping = true;
                    Swipe();
                }
                else
                {
                    isSwiping = false;
                    _alreadyTouch = true;
                    BacklightLines.Instance.EnableBacklights();
                }
            }
        }
        else
        {
            if (Input.touchCount > 0 && _alreadyTouch == false)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    _startTapPosition = Input.GetTouch(0).position;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    _endTapPosition = Input.GetTouch(0).position;
                    _deltaPosition = _endTapPosition - _startTapPosition;

                    if (_deltaPosition.magnitude > deadZone)
                    {
                        isSwiping = true;
                        Swipe();
                    }
                    else
                    {
                        isSwiping = false;
                        _alreadyTouch = true;
                        BacklightLines.Instance.EnableBacklights();
                    }
                }
            }
        }

        if (GameManager.Instance.AllCubesReadyToMerge == true)
        {
            _alreadyTouch = false;
        }
    }
    public void Swipe()
    {
            GameObject currentCube = GameManager.Instance.CurrentCube;

            if (currentCube /*&& Mathf.Abs(this._rb2d.velocity.y) > 0.1*/)
            {
                NumberCube[] cubes = currentCube.GetComponentsInChildren<NumberCube>();
                float x1 = cubes[cubes.GetLowerBound(0)].transform.position.x;
                float x2 = cubes[cubes.GetUpperBound(0)].transform.position.x;
                float temp = cubes[cubes.GetLowerBound(0)].transform.position.x;

                cubes[cubes.GetLowerBound(0)].transform.position = new Vector2(x2, cubes[cubes.GetLowerBound(0)].transform.position.y);
                cubes[cubes.GetUpperBound(0)].transform.position = new Vector2(temp, cubes[cubes.GetUpperBound(0)].transform.position.y);
            }

    }
    public void ResetSwipe()
    {
        isSwiping = false;

        _startTapPosition = Vector2.zero;
        _endTapPosition = Vector2.zero;
        _deltaPosition = Vector2.zero;

    }
    
}
