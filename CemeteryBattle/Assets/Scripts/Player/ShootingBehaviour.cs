using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 namespace PainfulTest.Player
{
    [RequireComponent(typeof(AudioSource))]
    public class ShootingBehaviour : MonoBehaviour
    {
        public static ShootingBehaviour Instance { get; private set; }

        [Header("Shooting Parameters")]
        [Space(5)]

        [SerializeField, Range(10, 100)]
        private int _initialAmmo;

        [SerializeField, Range(10, 100)]
        private int _packAmmoValue;

        public int _currentAmmo { get; private set; }

        [SerializeField, Range(0.01f, 2.0f)]
        private float Cadency;
        private float _timer;

        [SerializeField] private GameObject _bow;
        [SerializeField] private GameObject _arrow;

        [Header("Audio Clips")]
        [Space(5)]

        protected AudioSource _source;

        [SerializeField] protected AudioClip _shoot;
       

        private void Awake()
        {
            Instance = this;
            _currentAmmo = _initialAmmo;
        }

        public void Start()
        {
            _source = GetComponent<AudioSource>();
            Manager.MatchManager.Instance.OnArrowUpdate.Invoke();
        }

        void Update()
        {
            if (!Player.PlayerHealth.Instance.IsDead && !Manager.MatchManager.Instance.TimeIsUp)
            {
                if (_timer <= Cadency)
                {
                    _timer += Time.deltaTime;
                }

                if (Input.GetKeyDown(KeyCode.Mouse0) && _timer > Cadency && _currentAmmo > 0 && !Settings.PauseMenu.Instance.IsPaused)
                {
                    _source.PlayOneShot(_shoot);
                    Instantiate(_arrow, _bow.transform.position, _bow.transform.rotation);
                    _currentAmmo--;
                    Manager.MatchManager.Instance.OnArrowUpdate.Invoke();
                    _timer = 0;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Items.CollectableItem>()!=null && other.gameObject.GetComponent<Items.CollectableItem>().Type==Items.CollectableType.ArrowPack)
            {
                Items.CollectableItem arrow = other.gameObject.GetComponent<Items.CollectableItem>();
                _currentAmmo += _packAmmoValue;
                Destroy(other.gameObject);
                Manager.MatchManager.Instance.OnArrowUpdate.Invoke();
            }
        }
    }
}
