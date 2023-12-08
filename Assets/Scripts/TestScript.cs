using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] public GameObject itemListPrefab;
    [SerializeField] public RectTransform contentsField;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 2500; i++)
        {
            Instantiate(itemListPrefab, contentsField);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
