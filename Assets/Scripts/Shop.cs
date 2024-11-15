using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{


    [SerializeField] private float _timeToStartPurchase;
    [SerializeField] private float _timeToCompletePurchase;
    [SerializeField] private Image _purchaseProgressImage;


    private float _purchaseProgress;
    private bool _isPlayerInside;
    private bool _alreadyPurchased;
    private float _startPurchase;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckPurchase();

        _purchaseProgressImage.fillAmount = _purchaseProgress / _timeToCompletePurchase;
    }

    void CheckPurchase()
    {
        if (!ShouldStartPurchase())
            return;

        if (!_isPlayerInside)
            return;

        _purchaseProgress += Time.deltaTime;

        if (_purchaseProgress >= _timeToCompletePurchase)
        {
            Debug.Log("Purchased");
            _alreadyPurchased = true;

            //Dar recompensa
        }
    }

    bool ShouldStartPurchase()
    {
        return Time.time >= _startPurchase && !_alreadyPurchased;
    }

    void Reset()
    {
        _purchaseProgress = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInside = true;
            _startPurchase = Time.time + _timeToStartPurchase;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInside = false;
            Reset();
            //_startPurchase = Time.time + _timeToStartPurchase;
        }
    }
}
