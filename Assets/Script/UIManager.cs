using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Text currentAmmoText;

    public Camera uiCamera;
    public RectTransform canvas;
    public Slider hpBar;
    public GameObject player;
    public GameObject hudRoot;

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeCurrenAmmon(int count)
    {
        currentAmmoText.text = string.Format("{0}", count);
    }

    public Slider AddEnemyHUD()
    {
        Slider hud = Instantiate(hpBar, hudRoot.transform);
        return hud;
    }



    public void UpdateHUDPanel(Slider hud, Vector3 target)
    {
        Vector3 targetPos = target;
        Vector3 viewPoint = Camera.main.WorldToViewportPoint(targetPos);
        if(viewPoint.z < 0)
        {
            return;
        }
        Vector2 position = new Vector2(viewPoint.x * canvas.rect.width, viewPoint.y * canvas.rect.height);
        hud.transform.localPosition = position;
    }
}
