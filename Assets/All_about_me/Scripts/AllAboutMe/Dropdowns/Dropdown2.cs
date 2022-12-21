using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dropdown2 : MonoBehaviour
{
    [SerializeField] ParaData para;
    private Dropdown0 Manual;
    private IntroDiaglog Intro;
    private HintDiaglog Hint;
    public Dropdown Dropdown;
    public Text HintText;
    int index;
    List<string> items = new List<string>();

    void Start()
    {
        Intro = GetComponent<IntroDiaglog>();
        Hint = GetComponent<HintDiaglog>();
        Manual = GetComponent<Dropdown0>();

        AddOptions();
        Dropdown.options.Clear();

        DropdowmItemSelected(Dropdown);
    }

    void DropdowmItemSelected(Dropdown dropdown)
    {
        index = Dropdown.value;
        Dropdown.onValueChanged.AddListener(delegate { DropdowmItemSelected(Dropdown);});
    }

    public void AddOptions()
    {
        items.Clear();
        switch(Intro.num)
        {
            case 5:
                HintText.text = "(Year)";
                Dropdown.captionText.text = "1980";
                for (int i = 1980; i < 2023; i++)
                {
                    items.Add(i.ToString());
                }
                break;
        }
        foreach (var item in items)
        {
            Dropdown.options.Add(new Dropdown.OptionData() { text = item });
        }
    }
    public void SetAct()
    {
        Dropdown.gameObject.SetActive(false);
        switch(Intro.num)
        {
            case 5:
                Dropdown.gameObject.SetActive(true);
                break;
        }
    }

    public void DropdowmClear()
    {
        Dropdown.options.Clear();
    }
}
