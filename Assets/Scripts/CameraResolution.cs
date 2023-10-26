using System.Runtime.InteropServices;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    // �����ػ󵵸� �����ϰ� ���� �κ�(Letterbox)�� ���� ���ӿ�����Ʈ(Prefab)
    public GameObject m_objBackScissor;


    void Awake()
    {
        // ���۽� �ѹ� ����(���� �����߿� �ػ󵵰� ����Ǹ� �ٽ� ȣ��)
        UpdateResolution();
    }


    void UpdateResolution()
    {
        // ������Ʈ ���� �ִ� ��� ī�޶� ������
        Camera[] objCameras = Camera.allCameras;

        // ���� ���ϱ�(16:9 ����)
        //width 9, height 16
        float fResolutionX = Screen.width / 9f;
        float fResolutionY = Screen.height / 16f;

        // X�� Y���� ū ���� ȭ���� ���η� ���� ���
        if (fResolutionX > fResolutionY)
        {
            // ��Ⱦ��(Aspect Ratio) ���ϱ�
            // 16:9�� ��� 1.77:1
            float fValue = (fResolutionX - fResolutionY) * 0.5f;
            fValue = fValue / fResolutionX;

            // ������ ���� ��Ⱦ�� �������� ī�޶��� ����Ʈ�� �缳��
            // ����ȭ�� ��ǥ��°� ������ �ȵ�!
            foreach (Camera obj in objCameras)
            {
                obj.rect = new Rect(((Screen.width * fValue) / Screen.width) + (obj.rect.x * (1.0f - (2.0f * fValue))),
                                    obj.rect.y,
                                    obj.rect.width * (1.0f - (2.0f * fValue)),
                                    obj.rect.height);
            }


            // ���ʿ� �� ���͹ڽ��� �����ϰ� ��ġ����
            GameObject objLeftScissor = (GameObject)Instantiate(m_objBackScissor);
            objLeftScissor.GetComponent<Camera>().rect = new Rect(0, 0, (Screen.width * fValue) / Screen.width, 1.0f);

            // ������ ���͹ڽ�
            GameObject objRightScissor = (GameObject)Instantiate(m_objBackScissor);
            objRightScissor.GetComponent<Camera>().rect = new Rect((Screen.width - (Screen.width * fValue)) / Screen.width,
                                                                   0,
                                                                   (Screen.width * fValue) / Screen.width,
                                                                   1.0f);


            // ������ �� ���͹ڽ��� �ڽ����� �߰�
            objLeftScissor.transform.parent = gameObject.transform;
            objRightScissor.transform.parent = gameObject.transform;
        }
        // ȭ���� ���η� ���� ��쵵 ������ ������ ��ħ
        else if (fResolutionX < fResolutionY)
        {
            float fValue = (fResolutionY - fResolutionX) * 0.5f;
            fValue = fValue / fResolutionY;

            foreach (Camera obj in objCameras)
            {
                obj.rect = new Rect(obj.rect.x,
                                    ((Screen.height * fValue) / Screen.height) + (obj.rect.y * (1.0f - (2.0f * fValue))),
                                    obj.rect.width,
                                    obj.rect.height * (1.0f - (2.0f * fValue)));

                //obj.rect = new Rect( obj.rect.x , obj.rect.y + obj.rect.y * fValue, obj.rect.width, obj.rect.height - obj.rect.height * fValue );
            }


            GameObject objTopScissor = (GameObject)Instantiate(m_objBackScissor);
            objTopScissor.GetComponent<Camera>().rect = new Rect(0, 0, 1.0f, (Screen.height * fValue) / Screen.height);

            GameObject objBottomScissor = (GameObject)Instantiate(m_objBackScissor);
            objBottomScissor.GetComponent<Camera>().rect = new Rect(0, (Screen.height - (Screen.height * fValue)) / Screen.height
                                                    , 1.0f, (Screen.height * fValue) / Screen.height);


            objTopScissor.transform.parent = gameObject.transform;
            objBottomScissor.transform.parent = gameObject.transform;
        }
        else
        {
            // Do Not Setting Camera
        }
    }
}
