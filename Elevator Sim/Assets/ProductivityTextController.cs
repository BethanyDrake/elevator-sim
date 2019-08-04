using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductivityTextController : MonoBehaviour
{

    public static ProductivityTextController instance;

    public string metric;
    public int productivity = 0;
    // Start is called before the first frame update

    private Text text;
    void Start()
    {
        instance = this;
        text = GetComponent<Text>();
        text.text = metric + ": "+ productivity;
    }

    public void UpdateProductivity(int amount)
    {
        productivity+= amount;
        text.text = metric + ": "+ productivity;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
