using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InitialScript : MonoBehaviour
{
    public GameObject Self;
    public TMP_Text Amount;
    public TMP_Text Source;
    public int lifeTimeInSeconds;
    public int distanceBetweenNext;
    private float counter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if (counter > lifeTimeInSeconds) Destroy(Self);
    }
    public void SetAmount(int amount)
    {
        Amount.text = amount.ToString() + "$";
        if (amount < 0)
        {
            Amount.color = Color.red;
            Source.color = Color.red;
        }
        else
        {
            Amount.color = Color.green;
            Source.color = Color.green;
        }
    }
    public void SetSource(string source)
    {
        Source.text = source;
    }
    public void SetY(int order)
    {
        RectTransform AmountRt = Amount.GetComponent<RectTransform>();
        RectTransform SourceRt = Source.GetComponent<RectTransform>();
        AmountRt.anchoredPosition += new Vector2(0, (order * distanceBetweenNext));
        SourceRt.anchoredPosition += new Vector2(0, (order * distanceBetweenNext));
    }
}
