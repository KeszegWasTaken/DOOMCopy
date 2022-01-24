using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class WeaponSwitching : MonoBehaviour
{

    int selectedWeapon = 0;
    Keyboard kb;
    Mouse m;

    // Start is called before the first frame update
    void Start()
    {
        kb = InputSystem.GetDevice<Keyboard>();
        m = InputSystem.GetDevice<Mouse>();
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int prevWeapon = selectedWeapon;
        if(m.scroll.ReadValue().y > 0f){
            if(selectedWeapon >= transform.childCount-1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        if(m.scroll.ReadValue().y < 0f){
            if(selectedWeapon <= 0)
                selectedWeapon = transform.childCount-1;
            else
                selectedWeapon--;
        }
        if(kb.digit1Key.wasPressedThisFrame){
            selectedWeapon = 0;
        }

        if(kb.digit2Key.wasPressedThisFrame){
            selectedWeapon = 1;
        }

        if(prevWeapon != selectedWeapon){
            SelectWeapon();
        }
    }

    public void SelectWeapon(){
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if(i == selectedWeapon){
                weapon.gameObject.SetActive(true);
            }
            else{
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }

    
}
