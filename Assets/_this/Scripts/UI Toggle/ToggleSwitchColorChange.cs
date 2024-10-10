using UnityEngine;
using UnityEngine.UI;

namespace Christina.UI
{
    public class ToggleSwitchColorChange : ToggleSwitch
    {
        [Header("Elements to Recolor")]
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image handleImage;
        
        [Space]
        [SerializeField] private bool recolorBackground;
        [SerializeField] private bool recolorHandle;
        [Space]
        [SerializeField] private bool changeBackground;
        [SerializeField] private bool changeHandle;
        
        [Header("Colors")]
        [SerializeField] private Color backgroundColorOff = Color.white;
        [SerializeField] private Color backgroundColorOn = Color.white;
        [Space]
        [SerializeField] private Color handleColorOff = Color.white;
        [SerializeField] private Color handleColorOn = Color.white;
        
        [Header("Sprites")]
        [SerializeField] private Sprite backgroundSpriteOff;
        [SerializeField] private Sprite backgroundSpriteOn;
        [Space]
        [SerializeField] private Sprite handleSpriteOff;
        [SerializeField] private Sprite handleSpriteOn;
        
        private bool _isBackgroundImageNotNull;
        private bool _isHandleImageNotNull;
        
        protected override void OnValidate()
        {
            base.OnValidate();
            
            CheckForNull();
            ChangeColors();
        }

        private void OnEnable()
        {
            transitionEffect += ChangeColors;
            transitionEffect += ChangeSprites;
        }
        
        private void OnDisable()
        {
            transitionEffect -= ChangeColors;
            transitionEffect -= ChangeSprites;
        }

        protected override void Awake() 
        {
            base.Awake();
            
            CheckForNull();
            ChangeColors();
            ChangeSprites();
        }

        private void CheckForNull()
        {
            _isBackgroundImageNotNull = backgroundImage != null;
            _isHandleImageNotNull = handleImage != null;
        }


        private void ChangeColors()
        {
            if (recolorBackground && _isBackgroundImageNotNull)
                backgroundImage.color = Color.Lerp(backgroundColorOff, backgroundColorOn, sliderValue); 
            
            if (recolorHandle && _isHandleImageNotNull)
                handleImage.color = Color.Lerp(handleColorOff, handleColorOn, sliderValue); 
        }
        
        private void ChangeSprites()
        {
            if (changeBackground && _isBackgroundImageNotNull)
            {
                // Assuming sliderValue ranges from 0 (off) to 1 (on)
                if (sliderValue < 0.5f) // Adjust this threshold as necessary
                    backgroundImage.sprite = backgroundSpriteOff;
                else
                    backgroundImage.sprite = backgroundSpriteOn;
            }

            if (changeHandle && _isHandleImageNotNull)
            {
                // Same logic for the handle image
                if (sliderValue < 0.5f) // Adjust this threshold as necessary
                    handleImage.sprite = handleSpriteOff;
                else
                    handleImage.sprite = handleSpriteOn;
            }
        }

    }
}




