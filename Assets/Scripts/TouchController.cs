using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    [SerializeField] private BacklightLines backlightLines;
    [SerializeField] private float deadZone = 0.5f;

    private Vector2 _startTapPosition;
    private Vector2 _endTapPosition;
    private Vector2 _deltaPosition;

    private bool _isMobile;
    private bool _alreadyTouch = false;
    private bool _isSwiping;

    public bool IsSwiping { get => _isSwiping; set => _isSwiping = value; }

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
                    IsSwiping = true;
                    Swipe();
                }
                else
                {
                    IsSwiping = false;
                    _alreadyTouch = true;
                    backlightLines.EnableBacklights();
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
                        IsSwiping = true;
                        Swipe();
                    }
                    else
                    {
                        IsSwiping = false;
                        _alreadyTouch = true;
                        backlightLines.EnableBacklights();
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
            AchievementConditionManager.Instance.InvokeOnSwipeAction();
            if (currentCube && !currentCube.gameObject.GetComponentInChildren<ThreeNumberCube>())
            {
                NumberCube[] cubes = currentCube.GetComponentsInChildren<NumberCube>();
                float x1 = cubes[cubes.GetLowerBound(0)].transform.position.x;
                float x2 = cubes[cubes.GetUpperBound(0)].transform.position.x;
                float temp = cubes[cubes.GetLowerBound(0)].transform.position.x;

                cubes[cubes.GetLowerBound(0)].transform.position = new Vector2(x2, cubes[cubes.GetLowerBound(0)].transform.position.y);
                cubes[cubes.GetUpperBound(0)].transform.position = new Vector2(temp, cubes[cubes.GetUpperBound(0)].transform.position.y);
            }
            else if (currentCube && currentCube.gameObject.GetComponentInChildren<ThreeNumberCube>())
            {
                NumberCube[] cubes = currentCube.GetComponentsInChildren<NumberCube>();
                float x0 = cubes[0].transform.position.x;
                float x1 = cubes[1].transform.position.x;
                float x2 = cubes[2].transform.position.x;

                cubes[0].transform.position = new Vector2(x1, cubes[0].transform.position.y);
                cubes[1].transform.position = new Vector2(x2, cubes[1].transform.position.y);
                cubes[2].transform.position = new Vector2(x0, cubes[2].transform.position.y);
            }
            else
            {
                return;
            }
    }
    public void ResetSwipe()
    {
        IsSwiping = false;

        _startTapPosition = Vector2.zero;
        _endTapPosition = Vector2.zero;
        _deltaPosition = Vector2.zero;
    }
    
}
