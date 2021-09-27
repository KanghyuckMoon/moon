using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Text currentAmmoText;

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeCurrenAmmon(int count)
    {
        currentAmmoText.text = string.Format("{0}", count);
    }
}
