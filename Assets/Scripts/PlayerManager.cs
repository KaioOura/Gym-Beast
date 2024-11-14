using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    //Being aware that we are making a single player game where only 1 instance of player is being spawned, 
    //a Singleton will make a lot easier the game devlopment.

    [SerializeField] private LevelPlayerModule[] _levelPlayerModules;

    private int _currency;
    private int _level = 1;
    private int _maxBodyPileCount = 1;


    public int Level => _level;
    public int MaxBodyPileCount => _maxBodyPileCount;
    public int Currency => _currency;


    public event Action<int> OnMaxPileCountUpdate;
    public event Action<Color> OnChangePlayerColor;
    public event Action<int, bool> OnCurrencyChange;

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

    bool CanLevelUp()
    {
        if (_level > _levelPlayerModules.Length)
            return false;

        if (_levelPlayerModules[_level].CurrencyNeeded > _currency)
            return false;

        return true;
    }


    [ContextMenu("Level Up")]
    public void LevelUp()
    {
        if (!CanLevelUp())
        {
            return;
        }

        _currency -= _levelPlayerModules[_level].CurrencyNeeded;
        OnCurrencyChange?.Invoke(_currency, false);
        //Evento atualizando UI

        _maxBodyPileCount += _levelPlayerModules[_level].ValueToAddToPile;
        OnMaxPileCountUpdate?.Invoke(_maxBodyPileCount);
        OnChangePlayerColor?.Invoke(_levelPlayerModules[_level].PlayerColor);
        
        _level += 1;
    }

    public void OnReceivePayment(int value)
    {
        _currency += value;

        OnCurrencyChange?.Invoke(_currency, true);

        if (CanLevelUp())
        {
            //Evento indicando possibilidade de level up, provavelmente UI
        }

        //Evento disparando quando ganhou;
    }
}
