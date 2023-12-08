using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollEntity : MonoBehaviour
{
    [SerializeField] private Text rankLabel;

    public void SetRank(int rank)
    {
        rankLabel.text = rank.ToString();
    }
}
