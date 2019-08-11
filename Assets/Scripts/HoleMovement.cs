﻿using Core.Signal.Level;
using Core.View;
using UnityEngine;
using Zenject;

public class HoleMovement : MonoBehaviour
{
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _verticalSpeed;
    
    private SwipeInput _swipeInput;
    private SwipeInput SwipeInput
    {
        get
        {
            if (_swipeInput == null) _swipeInput = GetComponent<SwipeInput>();
            return _swipeInput;
        }
    }

    private SignalBus _signalBus;
    private PlatformView _platformView;


    private Vector3 _initialPosition;
    private bool _shouldMove;
    private float _minPosX, _maxPosX, _minPosZ, _maxPosZ;
    
    [Inject]
    private void Construct(
        SignalBus signalBus,
        PlatformView platformView)
    {
        _signalBus = signalBus;
        _platformView = platformView;
        
        _signalBus.Subscribe<StageLoadedSignal>(OnStageLoaded);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<StageLoadedSignal>(OnStageLoaded);        
    }

    private void Awake()
    {
        _initialPosition = transform.position;
    }
    
    public void FixedUpdate()
    {
        if(_shouldMove) Move();
    }

    private void Move()
    {
        float posX = transform.position.x + _horizontalSpeed * SwipeInput.Horizontal * Time.fixedDeltaTime;
        float posZ = transform.position.z + _verticalSpeed * SwipeInput.Vertical * Time.fixedDeltaTime;

        posX = Mathf.Clamp(posX, _minPosX, _maxPosX);
        posZ = Mathf.Clamp(posZ, _minPosZ, _maxPosZ);
        
        transform.position = new Vector3(posX, 0f, posZ);
    }

    #region Signal Listeners

    private void OnStageLoaded()
    {
        transform.position = _initialPosition;
        
        _shouldMove = true;
        
        _minPosX = _platformView.transform.position.x - _platformView.transform.lossyScale.x / 2f + 0.75f;
        _maxPosX = _platformView.transform.position.x + _platformView.transform.lossyScale.x / 2f - 0.75f;
        
        _minPosZ = _platformView.transform.position.z - _platformView.transform.lossyScale.z / 2f + 0.75f;
        _maxPosZ = _platformView.transform.position.z + _platformView.transform.lossyScale.z / 2f - 0.75f;
    }    

    #endregion
}
