using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

[RequireComponent(typeof(ScrollRect))]
public class InfiniteScrollView : MonoBehaviour
{
    [SerializeField] public ScrollEntity itemListPrefab;
    [SerializeField] public List<int> dataList;
    [SerializeField] public float itemHeight;
    [SerializeField] public int dataCount;

    private ScrollRect scrollRect;
    private List<ScrollEntity> itemList;
    private RectTransform scRectTransform;

    private float offset;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        scRectTransform = GetComponent<RectTransform>();

        scrollRect.horizontal = false;
        scrollRect.vertical = true;

        itemHeight = itemListPrefab.GetComponent<RectTransform>().rect.height;

        scrollRect.onValueChanged.AddListener((value) => UpdateScroll());
    }

    // Start is called before the first frame update
    void Start()
    {
        dataList.Clear();

        for (int i = 0; i < dataCount; i++)
        {
            dataList.Add(i);
        }

        CreateItem();
        SetContentHeight();
    }

    private void CreateItem()
    {
        itemList = new List<ScrollEntity>();

        int itemCount = (int)(scRectTransform.rect.height / itemHeight) + 3;

        for (int i = 0; i < itemCount; i++)
        {
            ScrollEntity item = Instantiate(itemListPrefab, scrollRect.content);
            itemList.Add(item);

            item.transform.localPosition = new Vector3(0, i * -itemHeight);
            SetData(item, i);
        }

        offset = itemList.Count * itemHeight;
    }

    private void SetContentHeight()
    {
        scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, dataList.Count * itemHeight);
    }

    private void SetData(ScrollEntity item, int idx)
    {
        if (idx < 0 || idx >= dataCount)
        {
            item.gameObject.SetActive(false);
            return;
        }

        item.gameObject.SetActive(true);
        item.SetRank(dataList[idx]);
    }

    private void SetDataAdjustIndex(ScrollEntity item)
    {
        int idx = (int)(item.transform.localPosition.y / -itemHeight);
        SetData(item, idx);
    }

    private void UpdateScroll()
    {
        foreach (ScrollEntity item in itemList)
        {
            RelocationItem(item);
        }
    }

    private void RelocationItem(ScrollEntity item)
    {
        float contentY = scrollRect.content.anchoredPosition.y;

        bool isChangedItem;

        if (item.transform.localPosition.y + contentY > itemHeight * 2)
        {
            item.transform.localPosition -= new Vector3(0, offset);
            isChangedItem = true;
        }
        else if (item.transform.localPosition.y + contentY < -scRectTransform.rect.height - itemHeight)
        {
            item.transform.localPosition += new Vector3(0, offset);
            isChangedItem = true;
        }
        else
        {
            isChangedItem = false;
        }

        if (isChangedItem)
        {
            SetDataAdjustIndex(item);
            RelocationItem(item);
        }
    }
}
