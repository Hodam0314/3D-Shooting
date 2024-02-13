using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum Cams//�̸� ���ǵǾ�߸� �ϴ� ������
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
        for(int iNum = 0; iNum < count; ++iNum)//���ٽ� for���� �������� �����̵Ǵ� ������ ��Ӻ��ϴ°� �� ���ϴ�
            //�������� �ּҸ� ��� �����ϱ� ������ ������ �߱�
        {
            int num = iNum;
            listBtns[iNum].onClick.AddListener(() => switchCamera(num));
            //���ٽ� -> �����Լ�
            //��������Ʈ -> �븮�� Ȥ�� ���߿� ����� ������
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
                Debug.Log("���� �ƹ��� �׼ǵ� ������ ���� �ʽ��ϴ�.");
            }
            else
            {
                _action.Invoke();
            }
        }
    }

    //��� : �Ű������� ���޹��� ī�޶�� ���ְ�, ������ ī�޶�� ���ݴϴ�.
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
