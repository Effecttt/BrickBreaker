using System.Collections;
using TMPro;
using UnityEngine;

namespace Utils.PopUp
{
    public class TextPopUp : MonoBehaviour
    {
        private const string Path = "TextPopUp/TextPopUp";
        private const string PathDamage = "TextPopUp/TextPopUpDamage";

        public static TextPopUp Create(Vector3 position, object message, bool priority = false, float disappearTime = .2f)
        {
            if (priority)
            {
                var otherTexts = GameObject.FindObjectsOfType<TextPopUp>();
                foreach (var text in otherTexts)
                {
                    Destroy(text.gameObject);
                }
            }
            TextPopUp textPopUp = Instantiate(Resources.Load<TextPopUp>(Path), position, Quaternion.identity);
            textPopUp.Init(message, disappearTime);
            return textPopUp;
        }
        
        public static TextPopUp Create(Transform target, object message, Color color, float disappearTime = .2f)
        {
            TextPopUp textPopUp = Instantiate(Resources.Load<TextPopUp>(PathDamage), target.position, Quaternion.identity);
            textPopUp.Init(message, color, disappearTime);
            return textPopUp;
        }
        
        public static TextPopUp Create(Vector3 pos, object message, Color color, float disappearTime = .2f)
        {
            TextPopUp textPopUp = Instantiate(Resources.Load<TextPopUp>(PathDamage), pos, Quaternion.identity);
            textPopUp.Init(message, color, disappearTime);
            return textPopUp;
        }
        
        public static TextPopUp CreateDamage(Transform target, float message, bool critical = false, float disappearTime = .2f)
        {
            TextPopUp textPopUp = Instantiate(Resources.Load<TextPopUp>(PathDamage), target.position, Quaternion.identity);
            textPopUp.InitDamage(message, critical, target, disappearTime);
            return textPopUp;
        }

        private TMP_Text _popUp;
        private float _disappearTime = 1f;
        private Color _textColor;
        private Transform _target;

        private void Awake()
        {
            _popUp = transform.GetComponent<TMP_Text>();
            _textColor = _popUp.color;
        }

        private void Start()
        {
            StartCoroutine(DestroyT());
        }

        private void Update()
        {
            MoveUp();
            DestroyTimer();
        }

        private void Init(object message, float disappear)
        {
            _popUp.text = message.ToString();
            _disappearTime = disappear;
        }
        
        private void Init(object message, Color color, float disappear)
        {
            _popUp.text = message.ToString();
            _disappearTime = disappear;
            _popUp.color = color;
            _textColor = _popUp.color;
        }

        private void InitDamage(object message, bool critical, Transform target, float disappear)
        {
            if((float)message > 0) _popUp.color = Color.green;
            else if ((float)message < 0) _popUp.color = Color.white;
            else message = "";

            if (critical)
            {
                _popUp.fontSize *= 1.5f;
                _popUp.color = Color.red;
            }

            if (message is float m)
                message = Mathf.Abs(m);

            _textColor = _popUp.color;
            
            _popUp.text = message.ToString();
            _disappearTime = disappear;
            _target = target;
        }

        void MoveUp()
        {
            float ySpeed = 2f;
            transform.position += new Vector3(0, ySpeed) * Time.deltaTime;
        }

        void DestroyTimer()
        {
            _disappearTime -= Time.deltaTime;
            if (_disappearTime < 0)
            {
                float disappearSpeed = 3f;
                _textColor.a -= disappearSpeed * Time.deltaTime;
                _popUp.color = _textColor;
                if (_textColor.a < 0)
                {
                    Destroy(gameObject);
                }
            }
        }

        IEnumerator DestroyT()
        {
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }
    }
}
