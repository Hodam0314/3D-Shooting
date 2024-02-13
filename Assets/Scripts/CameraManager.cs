using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum Cams//미리 정의되어야만 하는 데이터
{
    MainCam,
    SubCam1,
    SubCam2,
    SubCam3,
}

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    [SerializeField] List<Camera> listCam;
    [SerializeField] List<Button> listBtns;

    private UnityAction _action = null;
    public UnityAction Action { set => _action = value; }
    public void AddAction(UnityAction _addAction)
    {
        _action += _addAction;
    }

    public void RemoveAction(UnityAction _removeAction)
    {
        _action -= _removeAction;
    }

    [SerializeField] bool forTest = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    void Start()
    {
        switchCamera(Cams.MainCam);
        initBtns();
    }

    private void initBtns()
    {
        int count = listBtns.Count;
        for(int iNum = 0; iNum < count; ++iNum)//람다식 for문을 만났을때 조건이되는 변수가 계속변하는게 그 변하는
            //데이터의 주소를 계속 전달하기 때문에 문제를 야기
        {
            int num = iNum;
            listBtns[iNum].onClick.AddListener(() => switchCamera(num));
            //람다식 -> 무명함수
            //델리게이트 -> 대리자 혹은 나중에 실행될 예약기능
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            switchCamera(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            switchCamera(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            switchCamera(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            switchCamera(3);
        }

        if (forTest == true)
        {
            forTest = false;

            if (_action == null)
            {
                Debug.Log("저는 아무런 액션도 가지고 있지 않습니다.");
            }
            else
            {
                _action.Invoke();
            }
        }
    }

    //기능 : 매개변수로 전달받은 카메라는 켜주고, 나머지 카메라는 꺼줍니다.
    private void switchCamera(Cams _value)
    {
        int count = listCam.Count;
        int findNum = (int)_value;
        for (int iNum = 0; iNum < count; ++iNum)
        {
            Camera cam = listCam[iNum];
            cam.enabled = iNum == findNum;
        }
    } 
    
    private void switchCamera(int _value)
    {
        int count = listCam.Count;
        for (int iNum = 0; iNum < count; ++iNum)
        {
            Camera cam = listCam[iNum];
            cam.enabled = iNum == _value;
        }
    }


}
