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

        [Range(10, 100)]
        public int InitialAmmo;

        public int PackAmmoValue;

        public int _currentAmmo { get; private set; }

        [Range(0.01f, 2.0f)]
        public float Cadency;
        private float _timer;

        public GameObject Bow;
        public GameObject Arrow;

        [Header("Audio Clips")]
        [Space(5)]

        protected AudioSource _source;
    
        public AudioClip Shoot;
       

        private void Awake()
        {
            Instance = this;
            _currentAmmo = InitialAmmo;
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
                    _source.PlayOneShot(Shoot);
                    Instantiate(Arrow, Bow.transform.position, Bow.transform.rotation);
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
                _currentAmmo += PackAmmoValue;
                Destroy(other.gameObject);
                Manager.MatchManager.Instance.OnArrowUpdate.Invoke();
            }
        }
    }
}
