using System.Collections;
using TMPro;
using UnityEngine;

public class Chrono : MonoBehaviour
{
    /// <summary>
    /// Time in seconds.
    /// </summary>
    [SerializeField]
    private float _time;

    /// <summary>
    /// Minutes on screen.
    /// </summary>
    [SerializeField]
    private TMP_Text _minutes;

    /// <summary>
    /// Seconds on screen.
    /// </summary>
    [SerializeField]
    private TMP_Text _seconds;

    /// <summary>
    /// Coroutine that executes itself at each second.
    /// </summary>
    private Coroutine _decrementTimer;

    /// <summary>
    /// Value representing minutes lefts.
    /// </summary>
    private int _nbrOfMinutes;

    /// <summary>
    /// Value representing seconds lefts.
    /// </summary>
    private int _nbrOfSeconds;

    // Singleton
    private static Chrono _instance = null;

    public static Chrono Instance => _instance;

    // Observer
    public delegate void ProgressDelegate();

    //public event ProgressDelegate NewSecond;

    private void Awake()
    {
        // Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        ConvertTimeIntoChrono(_time);

        _minutes.SetText(ConvertToString(_nbrOfMinutes));
        _seconds.SetText(ConvertToString(_nbrOfSeconds));

        _decrementTimer = StartCoroutine(DecrementChrono());
    }

    /// <summary>
    /// Convert seconds into minutes and seconds.
    /// </summary>
    /// <param name="_time"> Time to convert. </param>
    private void ConvertTimeIntoChrono(float _time)
    {
        _nbrOfMinutes = Mathf.FloorToInt(_time / 60f);
        _nbrOfSeconds = (int)(_time - (Mathf.FloorToInt(_time / 60f) * 60f));
    }

    /// <summary>
    /// Convert seconds and minutes into seconds.
    /// </summary>
    /// <param name="_minutes"> Minutes left to convert. </param>
    /// <param name="_seconds"> Seconds left to convert. </param>
    /// <returns></returns>
    //private float ConvertChronoIntoTime(int _minutes, int _seconds)
    //{
    //    float _time = 0;

    //    _time += _minutes * 60;
    //    _time += _seconds;

    //    return _time;
    //}

    /// <summary>
    /// Convert time left into string that can be show to screen.
    /// </summary>
    /// <param name="_time"> Time to convert. </param>
    /// <returns></returns>
    private string ConvertToString(int _time)
    {
        if (_time >= 10)
        {
            return _time.ToString();
        }
        else
        {
            return $"{0}{_time}";
        }
    }

    /// <summary>
    /// Decrements chrono at each second.
    /// </summary>
    /// <returns></returns>
    private IEnumerator DecrementChrono()
    {
        yield return new WaitForSeconds(1f);

        // Decrements seconds at each second and minutes when seconds are under 0
        // Stops the chrono if minutes and seconds are equals to 0
        if (_nbrOfSeconds - 1 == -1 && _nbrOfMinutes != 0)
        {
            _nbrOfSeconds = 59;
            _nbrOfMinutes -= 1;

            _minutes.SetText(ConvertToString(_nbrOfMinutes));
            _seconds.SetText(ConvertToString(_nbrOfSeconds));
            _decrementTimer = StartCoroutine(DecrementChrono());
        }
        else if (_nbrOfSeconds - 1 == -1 && _nbrOfMinutes == 0)
        {
            StopTimer();
        }
        else
        {
            _nbrOfSeconds -= 1;
            _seconds.SetText(ConvertToString(_nbrOfSeconds));
            _decrementTimer = StartCoroutine(DecrementChrono());
        }
    }

    /// <summary>
    /// Stops the chrono.
    /// </summary>
    public void StopTimer()
    {
        StopCoroutine(_decrementTimer);
        GameManager.Instance.IsGameOver = true;
    }
}
