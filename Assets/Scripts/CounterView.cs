using System;
using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class CounterView : MonoBehaviour
{
    [SerializeField] private CounterController _counterController;
    private TextMeshPro _counterText;
    private float _delay = 0.5f;
    private int _counter;
    private bool _isCounting;
    private Coroutine _counterCoroutine;
    private String _nameCounter = "Counter: ";
    private WaitForSeconds _wait; 

    private void Start()
    {
       _counterText = transform.GetComponent<TextMeshPro>();
       _counterText.text = _nameCounter + _counter;
       _wait = new WaitForSeconds(_delay);
    }

    private void OnEnable()
    {
        _counterController.OnClickMouse += ProcessCounter;
    }

    private void OnDisable()
    {
        _counterController.OnClickMouse -= ProcessCounter;
    }

    private void ProcessCounter()
    {
        if (_isCounting)
        {
            StopCoroutine(_counterCoroutine);
            _isCounting = false;
        }
        else
        {
            _counterCoroutine = StartCoroutine(CountCoroutine());
            _isCounting = true;
        }
    }

    private IEnumerator CountCoroutine()
    {
        while (enabled)
        {
            _counter++;
            Debug.Log(_nameCounter + _counter);

            if (_counterText != null)
            {
                _counterText.text = _nameCounter + _counter;
            }

            yield return _wait;
        }
    }
}