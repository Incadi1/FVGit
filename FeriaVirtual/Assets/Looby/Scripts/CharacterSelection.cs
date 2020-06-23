using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    private GameObject[] CharacterList;
    private int index;



    // Start is called before the first frame update
    private void Start()
    {
        index = PlayerPrefs.GetInt("CharaterSelected");
        CharacterList = new GameObject[transform.childCount];

        //fill the array with our models
        for (int i = 0; i < transform.childCount; i++)
            CharacterList[i] = transform.GetChild(i).gameObject;


        //we toggle of their renderer
        foreach (GameObject go in CharacterList)
            go.SetActive(false);

        //Selected Character
        if (CharacterList[index])
            CharacterList[index].SetActive(true);
    }

    public void ToggleLeft()
    {
        CharacterList[index].SetActive(false);

        index--;
        if (index < 0)
            index = CharacterList.Length - 1;
        CharacterList[index].SetActive(true);
    }

    public void ToggleRight()
    {
        CharacterList[index].SetActive(false);

        index++;
        if (index == CharacterList.Length)
            index = 0;
        CharacterList[index].SetActive(true);
    }

    public void ConfirmButton()
    {
        PlayerPrefs.SetInt("CharaterSelected", index);
        SceneManager.LoadScene("Start/Scenes/Characters");
    }
}
