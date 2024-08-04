using System.Collections;
using TMPro;
using UnityEngine;

public class CounterController : MonoBehaviour
{
    private int _counter;
    private bool _isCounting;
    private Coroutine _counterCoroutine;

    public TextMeshPro counterText;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
    }

    IEnumerator CountCoroutine()
    {
        while (enabled)
        {
            _counter++;
            Debug.Log("Counter: " + _counter);

            if (counterText != null)
            {
                counterText.text = "Counter: " + _counter;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}