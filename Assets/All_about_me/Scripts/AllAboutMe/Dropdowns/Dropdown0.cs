using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dropdown0 : MonoBehaviour
{
    [SerializeField] ParaData ParaData;
    private AzureSpeechToText Azure;
    private AzureTextToSpeech Azure1;
    private IntroDiaglog Intro;
    private HintDiaglog Hint;
    private Dropdown1 Dropdown1;
    private Dropdown2 Dropdown2;
    public Dropdown Dropdown;
    public Text HintText;
    public Text TextSpeech;
    public InputField InputAns;
    int index;
    List<string> items = new List<string>();
    int count;
    string keyword;
    public TextAsset textFile;
    public List<string> textList = new List<string>();
    
    void Start()
    {
        Azure = GetComponent<AzureSpeechToText>();
        Azure1 = GetComponent<AzureTextToSpeech>();
        Intro = GetComponent<IntroDiaglog>();
        Hint = GetComponent<HintDiaglog>();
        Dropdown1 = GetComponent<Dropdown1>();
        Dropdown2 = GetComponent<Dropdown2>();
        GetTextFromFile(textFile);
        AddOptions();
        Dropdown.options.Clear();

        DropdowmItemSelected(Dropdown);
    }

    void DropdowmItemSelected(Dropdown dropdown)
    {
        index = Dropdown.value;
        if(Dropdown.captionText.text == "其他")
        {
            InputAns.gameObject.SetActive(true);
        }
        else
        {
            InputAns.gameObject.SetActive(false);
        }
        Dropdown.onValueChanged.AddListener(delegate { DropdowmItemSelected(Dropdown);});
    }

    public void AddOptions()
    {
        items.Clear();

        switch(Intro.num)
        {
            case 1:
                HintText.text = "(Name)";
                Dropdown.captionText.text = "Jeff";
                items.Add("Jeff");
                items.Add("Alex");
                items.Add("Ben");
                items.Add("Jerry");
                items.Add("Peter");
                items.Add("Zack");
                items.Add("Eric");
                items.Add("Danny");
                items.Add("Andy");
                items.Add("其他");
                break;
            case 2:
                HintText.text = "(brave)";
                Dropdown.captionText.text = "brave";
                items.Add("brave");
                items.Add("not brave");
                break;
            case 3:
                HintText.text = "(sex)";
                Dropdown.captionText.text = "boy";
                items.Add("boy");
                items.Add("girl");
                break;
            case 4:
                HintText.text = "(age)";
                Dropdown.captionText.text = "five";
                items.Add("five");
                items.Add("six");
                items.Add("seven");
                items.Add("eight");
                items.Add("nine");
                items.Add("ten");
                items.Add("eleven");
                items.Add("twelve");
                items.Add("thirteen");
                items.Add("fourteen");
                items.Add("fifteen");
                items.Add("sixteen");
                items.Add("seventeen");
                items.Add("eighteen");
                items.Add("nineteen");
                items.Add("twenty");
                break;
            case 5:
                HintText.text = "(Month)";
                Dropdown.captionText.text = "January";
                items.Add("January");
                items.Add("Febuary");
                items.Add("March");
                items.Add("April");
                items.Add("May");
                items.Add("June");
                items.Add("July");
                items.Add("August");
                items.Add("September");
                items.Add("October");
                items.Add("November");
                items.Add("December");
                break;
            case 6:
                HintText.text = "(City)";
                Dropdown.captionText.text = "Taipei";
                items.Add("Taipei");
                items.Add("New Taipei");
                items.Add("Keelung");
                items.Add("Taoyuan");
                items.Add("Hsinchu");
                items.Add("Miaoli");
                items.Add("Taichung");
                items.Add("Changhua");
                items.Add("Nantou");
                items.Add("Yunlin");
                items.Add("Chiayi");
                items.Add("Tainan");
                items.Add("Kaohsiung");
                items.Add("Pingtung");
                items.Add("Ilan");
                items.Add("Hualien");
                items.Add("Taitung");
                items.Add("Penghu");
                items.Add("Kinmen");
                break;
            case 7:
                HintText.text = "(family's amount)";
                Dropdown.captionText.text = "two";
                items.Add("two");
                items.Add("three");
                items.Add("four");
                items.Add("five");
                items.Add("six");
                items.Add("seven");
                items.Add("eight");
                items.Add("nine");
                items.Add("ten");
                break;
            case 8:
                HintText.text = "(Sister's amount)";
                Dropdown.captionText.text = "one sister";
                items.Add("zero");
                items.Add("one sister");
                items.Add("two sisters");
                items.Add("three sisters");
                items.Add("four sisters");
                items.Add("five sisters");
                break;
            case 9:
                HintText.text = "(Brother's amount)";
                Dropdown.captionText.text = "one brother";
                items.Add("zero");
                items.Add("one brother");
                items.Add("two brothers");
                items.Add("three brothers");
                items.Add("four brothers");
                items.Add("five brothers");
                break;
            case 10:
                HintText.text = "(father's Job)";
                Dropdown.captionText.text = "teacher";
                items.Add("teacher");
                items.Add("dentist");
                items.Add("clerk");
                items.Add("cook");
                items.Add("waiter");
                items.Add("waitress");
                items.Add("worker");
                items.Add("writer");
                items.Add("officer");
                items.Add("player");
                items.Add("reporter");
                items.Add("salesman");
                items.Add("secretary");
                items.Add("shopkeeper");
                items.Add("singer");                
                items.Add("soldier");
                items.Add("star");
                items.Add("actor");
                items.Add("actress");
                items.Add("bussiness");
                items.Add("engineer");
                items.Add("fisherman");
                items.Add("Lawyer");
                items.Add("其他");
                break;
            case 11:
                HintText.text = "(mother's Job)";
                Dropdown.captionText.text = "teacher";
                items.Add("teacher");
                items.Add("dentist");
                items.Add("clerk");
                items.Add("cook");
                items.Add("waiter");
                items.Add("waitress");
                items.Add("worker");
                items.Add("writer");
                items.Add("officer");
                items.Add("player");
                items.Add("reporter");
                items.Add("salesman");
                items.Add("secretary");
                items.Add("shopkeeper");
                items.Add("singer");                
                items.Add("soldier");
                items.Add("star");
                items.Add("actor");
                items.Add("actress");
                items.Add("bussiness");
                items.Add("engineer");
                items.Add("fisherman");
                items.Add("Lawyer");
                items.Add("其他");
                break;
            case 12:
                HintText.text = "(Hobby)";
                Dropdown.captionText.text = "read books";
                items.Add("read books");
                items.Add("play computer");
                items.Add("其他");
                break;
            case 13:
                HintText.text = "(the thing you want to do)";
                Dropdown.captionText.text = "read books";
                items.Add("read books");
                items.Add("play computer");
                items.Add("其他");
                break;
            case 14:
                HintText.text = "(Clothes)";
                Dropdown.captionText.text = "jeans";
                items.Add("jeans");
                items.Add("a cap");
                items.Add("a shirt");
                items.Add("其他");
                break;
            case 15:
                HintText.text = "(Shoes)";
                Dropdown.captionText.text = "sneakers";
                items.Add("sneakers");
                items.Add("其他");
                break;
            case 16:
                HintText.text = "(Sport)";
                Dropdown.captionText.text = "badminton";
                items.Add("badminton");
                items.Add("basketball");
                items.Add("swimming");
                items.Add("ping pong");
                items.Add("camping");
                items.Add("fishing");
                items.Add("hiking");
                items.Add("soccer");
                items.Add("dodgeball");
                items.Add("tennis");
                items.Add("volleyball");
                items.Add("其他");
                break;
            case 17:
                HintText.text = "(Color)";
                Dropdown.captionText.text = "blue";
                items.Add("blue");
                items.Add("black");
                items.Add("brown");
                items.Add("gray");
                items.Add("green");
                items.Add("red");
                items.Add("orange");
                items.Add("yellow");
                items.Add("white");
                items.Add("pink");
                items.Add("其他");
                break;
            case 18:
                HintText.text = "(Food)";
                Dropdown.captionText.text = "steak";
                items.Add("apple");
                items.Add("banana");
                items.Add("barbecue");
                items.Add("breef");
                items.Add("bread");
                items.Add("burger");
                items.Add("cake");
                items.Add("candy");
                items.Add("chicken");
                items.Add("steak");
                items.Add("coffee");
                items.Add("chocolate");
                items.Add("dumplings");
                items.Add("egg");
                items.Add("fruit");
                items.Add("pizza");
                items.Add("steak");
                items.Add("其他");
                break;
            case 19:
                HintText.text = "(drink)";
                Dropdown.captionText.text = "juice";
                items.Add("juice");
                items.Add("tea");
                items.Add("water");
                items.Add("其他");
                break;
            case 20:
                HintText.text = "(Subject)";
                Dropdown.captionText.text = "English";
                items.Add("English");
                items.Add("math");
                items.Add("Chinese");
                items.Add("music");
                items.Add("art");
                items.Add("science");
                items.Add("其他");
                break;
            case 21:
                HintText.text = "(Job)";
                Dropdown.captionText.text = "teacher";
                items.Add("teacher");
                items.Add("dentist");
                items.Add("clerk");
                items.Add("cook");
                items.Add("waiter");
                items.Add("waitress");
                items.Add("worker");
                items.Add("writer");
                items.Add("officer");
                items.Add("player");
                items.Add("reporter");
                items.Add("salesman");
                items.Add("secretary");
                items.Add("shopkeeper");
                items.Add("singer");                
                items.Add("soldier");
                items.Add("star");
                items.Add("actor");
                items.Add("actress");
                items.Add("bussiness");
                items.Add("engineer");
                items.Add("fisherman");
                items.Add("Lawyer");
                items.Add("其他");
                break;
            case 22:
                HintText.text = "(Country)";
                Dropdown.captionText.text = "Taiwan";
                items.Add("Taiwan");
                items.Add("America");
                items.Add("Japan");
                items.Add("Korea");
                items.Add("Canada");
                items.Add("Thailand");
                items.Add("Australia");
                items.Add("其他");
                break;
        }

        foreach (var item in items)
        {
            Dropdown.options.Add(new Dropdown.OptionData() { text = item });
        }  
    }

    public void DropdowmClear()
    {
        Dropdown.options.Clear();
        InputAns.text = "";
        InputAns.gameObject.SetActive(false);
    }

    public void SetAnswer()
    {   
        keyword = "____";
        if(Dropdown.captionText.text == "其他")
        {
            string answer = InputAns.text;
            Azure.message = textList[Intro.index-1].Replace(keyword,answer);
            ParaData.ParaStorage[Intro.index-1] = answer;
        }
        else
        {
            if(Intro.index == 5)
            {
                string answer = Dropdown.captionText.text + " " + Dropdown1.Dropdown.captionText.text + ", " + Dropdown2.Dropdown.captionText.text;
                Azure.message = textList[Intro.index-1].Replace(keyword,answer);
                ParaData.ParaStorage[Intro.index-1] = answer;
            }
            else
            {
                string answer = Dropdown.captionText.text;
                Azure.message = textList[Intro.index-1].Replace(keyword,answer);
                ParaData.ParaStorage[Intro.index-1] = answer;
            }
        }
        TextSpeech.text = Azure.message;
    }

    void GetTextFromFile(TextAsset file)
    {
        textList.Clear();

        var lineData = file.text.Split('\n');
        foreach (var line in lineData)
        {
            textList.Add(line);
            count = count + 1;
        }
    }
}