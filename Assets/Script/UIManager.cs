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
    public GameObject hpBar;
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

    public GameObject AddEnemyHUD()
    {
        GameObject hud = Instantiate(hpBar, hudRoot.transform);
        return hud;
    }



    public void UpdatePanel(GameObject hud, Transform target)
    {
        Vector3 targetPos = target.position;
        Vector2 screenPoint = Camera.main.WorldToViewportPoint(targetPos);
        Vector2 canvasPosition;
        Vector2 position = new Vector2(screenPoint.x * canvas.rect.width, screenPoint.y * canvas.rect.height);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas,position,uiCamera, out canvasPosition);
        hud.transform.localPosition = position;
    }
}
