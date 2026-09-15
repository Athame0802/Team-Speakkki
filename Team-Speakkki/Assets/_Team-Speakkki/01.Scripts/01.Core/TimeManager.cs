using System;
using UnityEngine;

namespace TeamSpeakkki.Managers
{
    public class TimeManager : MonoBehaviour
    {
        public static TimeManager Instance;

        [SerializeField] private int startTime = 300;

        public event Action<int> OnTimeChanged;
        public event Action OnTimeOver;

        public int CurrentTime { get; private set; }

        private float remainTime;
        private bool isRunning;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        private void Start()
        {
            StartTimer();
        }

        private void Update()
        {
            if (isRunning == false)
            {
                return;
            }

            remainTime -= Time.deltaTime;

            if (remainTime <= 0f)
            {
                remainTime = 0f;
                isRunning = false;
                SetTime(0);
                OnTimeOver?.Invoke();
                return;
            }

            SetTime(Mathf.CeilToInt(remainTime));
        }

        public void StartTimer()
        {
            remainTime = startTime;
            CurrentTime = startTime;
            isRunning = true;
            OnTimeChanged?.Invoke(CurrentTime);
        }

        public void StopTimer()
        {
            isRunning = false;
        }

        public void AddTime(int amount)
        {
            remainTime += amount;
            SetTime(Mathf.CeilToInt(remainTime));
        }

        private void SetTime(int value)
        {
            if (CurrentTime == value)
            {
                return;
            }

            CurrentTime = value;
            OnTimeChanged?.Invoke(CurrentTime);
        }
    }
}