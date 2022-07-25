using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberContentController : MonoBehaviour
{
    [SerializeField] GameObject showNumberPrefab;
    [SerializeField] Transform content;

    public void Spaw(int index)
    {
        ClearContent();
        GameObject item = Instantiate(showNumberPrefab, Vector3.zero, Quaternion.identity, content);
        item.transform.localPosition = Vector3.zero;
        item.GetComponent<ShowNumberController>().SetInfo(index);
    }



    private void ClearContent()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }
}
