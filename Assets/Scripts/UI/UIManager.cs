using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currencyTMP;


    // Start is called before the first frame update
    void Start()
    {
        PlayerManager.Instance.OnCurrencyChange += UpdateCurrency;
    }

    void OnDestroy()
    {
        PlayerManager.Instance.OnCurrencyChange -= UpdateCurrency;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateCurrency(int value, bool isPositive)
    {
        //if positive, gaining animation. If not, losing animation

        _currencyTMP.text = value.ToString();
    }
}
