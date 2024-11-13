using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    [SerializeField] private int _bodyPileCountLimit = 5;

    private int _currency;
    private int _level = 1;
    private int _maxBodyPileCount = 1;


    public int Level => _level;
    public int MaxBodyPileCount => _maxBodyPileCount;
    public int Currency => _currency;
    public event Action<int> OnMaxPileCountUpdate;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LevelUp()
    {
        if (_maxBodyPileCount < _bodyPileCountLimit)
        {
            _maxBodyPileCount += 1;
            OnMaxPileCountUpdate?.Invoke(_maxBodyPileCount);
        }
    }

    public void OnReceivePayment(int value)
    {
        _currency += value;
        //Evento disparando quando ganhou;
    }
}
