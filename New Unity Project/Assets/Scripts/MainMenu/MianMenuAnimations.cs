using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MianMenuAnimations : MonoBehaviour
{
    //Variables||

        public GameObject hiraganaFrame;
        public GameObject katakanaFrame;
        public GameObject gameLogo;

        //Variables limits
        public float logoMaxRotation = 5f;
        public float rotationSpeed = 0.2f;

        public float platesSpeed = 0.2f;

        //private variables
        private bool logoDirection = false;

        private float hiraganaDefaultY;
        private float katakanaDefaultY;
    //_________||

    //Functions||

        private void logoAnimation()
        {

            //The rect for of the game logo
            RectTransform gameLogoRect = gameLogo.GetComponent<RectTransform>();
            Quaternion gameLogoRotation = gameLogoRect.rotation;

            void addRotation(float offset)
            {
                gameLogoRect.rotation = new Quaternion(gameLogoRotation.x, gameLogoRotation.y, gameLogoRotation.z + (offset * Time.fixedDeltaTime), gameLogoRotation.w);
            }


            //Logo animation
            if (logoDirection)
            {
      
                addRotation(rotationSpeed);
                if (gameLogoRotation.z * Mathf.Rad2Deg > logoMaxRotation / 2)
                {
                    logoDirection = false;
                }
            }
            else
            {

                addRotation(-rotationSpeed);
                if (gameLogoRotation.z * Mathf.Rad2Deg < -logoMaxRotation / 2)
                {
                
                    logoDirection = true;
                }
            }

        }

        private void movePlateIntoScreen(GameObject plate, float GoalPos)
        {
            if (GoalPos > plate.GetComponent<RectTransform>().localPosition.y)
            {

                //Move towards the correct point
                RectTransform plateRect = plate.GetComponent<RectTransform>();

                plateRect.localPosition = new Vector3(0, plateRect.localPosition.y + platesSpeed, 0);
            }
            else if(GoalPos != plate.GetComponent<RectTransform>().localPosition.y)
            {
                //Move towards the correct point
                RectTransform plateRect = plate.GetComponent<RectTransform>();

                plateRect.localPosition = new Vector3(0, GoalPos, 0);
            }
        }
    //_________||

    // Start is called before the first frame update
    void Start()
    {
        //Get the plates positions
        RectTransform hiraganaFramePos = hiraganaFrame.GetComponent<RectTransform>();
        RectTransform katakanaFramePos = katakanaFrame.GetComponent<RectTransform>();
        hiraganaDefaultY = hiraganaFramePos.localPosition.y;
        katakanaDefaultY = katakanaFramePos.localPosition.y;

        //Put the plates outside of the screen
        hiraganaFramePos.localPosition = new Vector3(0, hiraganaDefaultY - 300, 0);
        katakanaFramePos.localPosition = new Vector3(0, katakanaDefaultY - 300, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Add the logo animation to the scene
        logoAnimation();

        //Move the plates onto the screen
        movePlateIntoScreen(hiraganaFrame, hiraganaDefaultY);
        movePlateIntoScreen(katakanaFrame, katakanaDefaultY);
    }
}
