using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{


    [SerializeField] private float _timeToStartPurchase;
    [SerializeField] private float _timeToCompletePurchase;
    [SerializeField] private Image _purchaseProgressImage;
    [SerializeField] private int _price;
    [SerializeField] private PurchaseReward _purchaseReward;


    private float _purchaseProgress;
    private bool _isPlayerInside;
    private bool _alreadyPurchased;
    private float _startPurchase;

    public event Action<int> OnPurchase;


    // Start is called before the first frame update
    void Start()
    {
        OnPurchase += PlayerManager.Instance.OnPayment;
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
            if (PlayerManager.Instance.Currency >= _price && CanGiveReward())
            {
                Debug.Log("Purchased");
                _alreadyPurchased = true;

                
                OnPurchase?.Invoke(_price);
            }


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

    


    bool CanGiveReward()
    {
        switch (_purchaseReward)
        {
            case PurchaseReward.None:
                {
                    break;
                }
            case PurchaseReward.Level:
                {
                    return PlayerManager.Instance.CanLevelUp();
                }

            default:
                {

                    break;
                }
        }
        return true;
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

public enum PurchaseReward
{
    None,
    Level,
    Skin
}
