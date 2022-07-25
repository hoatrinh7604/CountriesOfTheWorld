using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowNumberController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI numberText;
    [SerializeField] Image image;
    [SerializeField] string character = "";
    [SerializeField] Sprite[] listSprites;

    public void SetInfo(int index)
    {
        image.sprite = listSprites[index];
    }
}
