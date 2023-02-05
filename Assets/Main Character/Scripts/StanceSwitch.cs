using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StanceSwitch : MonoBehaviour
{
    [SerializeField]
    private float scaleX = 1f;
    public Animator anim;
    public MonoBehaviour humanScript;
    public MonoBehaviour animalScript;
    public GameObject humanGroundCheck;
    public GameObject animalGroundCheck;
    public CapsuleCollider2D humanCollider;
    public BoxCollider2D animalCollider;
    public GameObject humanUI;
    public GameObject animalUI;

    //Form
    private bool canSwitch = true;
    private bool isSwitching;
    private float switchingTIme = 30f;
    private float switchingCooldown = 150f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenuScript.isPaused)
        {
            //Transform
            if (Input.GetKeyDown(KeyCode.Alpha5) && canSwitch)
            {
                StartCoroutine(Transform());
            }
        }
    }
    private void FixedUpdate()
    {
    }
    private IEnumerator Transform()
    {
        canSwitch = false;
        isSwitching = true;
        anim.SetLayerWeight(1, 1);
        anim.SetLayerWeight(0, 0);
        anim.SetBool("isAnimal", true);
        humanScript.enabled = false;
        humanCollider.enabled = false;
        humanGroundCheck.SetActive(false);
        humanUI.SetActive(false);
        animalCollider.enabled = true;
        animalScript.enabled = true;
        animalGroundCheck.SetActive(true);
        animalUI.SetActive(true);
        yield return new WaitForSeconds(switchingTIme);
        isSwitching = false;
        anim.SetLayerWeight(0, 1);
        anim.SetLayerWeight(1, 0);
        humanScript.enabled = true;
        humanCollider.enabled = true;
        humanGroundCheck.SetActive(true);
        humanUI.SetActive(true);
        animalCollider.enabled = false;
        animalScript.enabled = false;
        animalGroundCheck.SetActive(false);
        animalUI.SetActive(false);
        yield return new WaitForSeconds(switchingCooldown);
        canSwitch = true;

    }
}
