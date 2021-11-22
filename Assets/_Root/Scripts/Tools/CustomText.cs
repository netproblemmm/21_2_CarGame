using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tools
{
    internal class CustomText : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private TMP_Text _textMesh;

        public string Text
        {
            get => GetText();
            set => SetText(value);
        }

        private void Awake()
        {
            _text ??= GetComponent<Text>();
            _textMesh ??= GetComponent<TMP_Text>();
        }

        private string GetText()
        {
            if (_text != null)
                return _text.text;

            if (_textMesh != null)
                return _textMesh.text;

            throw new ArgumentException();
        }

        private void SetText(string text)
        {
            if (_text != null)
                _text.text = text;

            if (_textMesh != null)
                _textMesh.text = text;
        }
    }
}
