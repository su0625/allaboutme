using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class radarchart : MonoBehaviour {

public  int [] ability = new int [4];
public  int [] d = new int [5];
private  int [] c = new int [5];
private  int [] b = new int [5];
private  int [] a = new int [5];
private float m_fullSize ;
private float n_fullSize ;

private int length;
private float range;
private float weighted;

public Text Extraversion;
public Text Agreeableness;
public Text Conscientiousness;
public Text EmotionalStability;
public Text OpennesstoExperience;
public Text Listen;
public Text Speak;
public Text Read;
public Text Write;

string zodiacSign;
public GameObject chart;
public GameObject back;
[SerializeField] ParaData ParaData;
[SerializeField] PlayerData PlayerData;
[SerializeField] personalityscore personalityscore;
// [SerializeField] TaskData TaskData;

void Start(){
    
        add();
        for(int i=0;i<5;i++){
            OnValidate();
            OnValidate2();
        }
        score();
        SetPlayerData();
        Setpersonalityscore();
}

    public void constellation()
    {
        string birth = ParaData.ParaStorage[4];
        string[] birthArray = birth.Split(' ');

        string[] dateArray = birthArray[1].Split('s');
        if(dateArray.Length == 1){
        dateArray = birthArray[1].Split('n');

            if(dateArray.Length == 1){
            dateArray = birthArray[1].Split('r');

                if(dateArray.Length == 1){
                dateArray = birthArray[1].Split('t');
        }
        }
        }
        
        int date = int.Parse(dateArray[0]);

        switch (birthArray[0]) { 
            case "January": 
            if(date < 21){          //摩羯座
                a[0]=0;             //12/21~1/20
                a[1]=2;
                a[2]=4;
                a[3]=2;
                a[4]=1;
                zodiacSign = "Capricorn";
            }
            else if(date >= 21){    //水瓶座
                a[0]=2;             //1/21~2/19
                a[1]=4;
                a[2]=0;
                a[3]=0;
                a[4]=7;
                zodiacSign = "Aquarius ";
            }    
                break;       
            case "February": 
            if(date < 20){          //水瓶座
                a[0]=2;             //1/21~2/19
                a[1]=4;
                a[2]=0;
                a[3]=0;
                a[4]=7;
                zodiacSign = "Aquarius";
            }
            else if(date >= 20){    //雙魚座
                a[0]=0;             //2/20~3/20
                a[1]=3;
                a[2]=2;
                a[3]=4;
                a[4]=1;
                zodiacSign = "Pisces";
            }    
                break; 
            case "March": 
            if(date < 21){          //雙魚座
                a[0]=0;             //2/20~3/20
                a[1]=3;
                a[2]=2;
                a[3]=4;
                a[4]=1;
                zodiacSign = "Pisces";
            }
            else if(date >= 21){    //牡羊座
                a[0]=5;             //3/21~4/19
                a[1]=0;
                a[2]=1;
                a[3]=2;
                a[4]=6;
                zodiacSign = "Aries";
            }    
                break; 
            case "April": 
            if(date < 20){          //牡羊座
                a[0]=5;             //3/21~4/19
                a[1]=0;
                a[2]=1;
                a[3]=2;
                a[4]=6;
                zodiacSign = "Aries";
            }
            else if(date >= 20){    //金牛座
                a[0]=0;             //4/20~5/20
                a[1]=2;
                a[2]=5;
                a[3]=3;
                a[4]=1;
                zodiacSign = "Taurus";
            }    
                break;
            case "May": 
            if(date < 21){          //金牛座
                a[0]=0;             //4/20~5/20
                a[1]=2;
                a[2]=5;
                a[3]=3;
                a[4]=1;
                zodiacSign = "Taurus";
            }
            else if(date >= 21){    //雙子座
                a[0]=2;             //5/21~6/21
                a[1]=0;
                a[2]=1;
                a[3]=2;
                a[4]=5;
                zodiacSign = "Gemini";
            }    
                break; 
            case "June": 
            if(date < 22){          //雙子座
                a[0]=2;             //5/21~6/21
                a[1]=0;
                a[2]=1;
                a[3]=2;
                a[4]=5;
                zodiacSign = "Gemini";
            }
            else if(date >= 22){    //巨蟹座
                a[0]=0;             //6/22~7/22
                a[1]=3;
                a[2]=2;
                a[3]=2;
                a[4]=1;
                zodiacSign = "Cancer";
            }    
                break;
            case "July": 
            if(date < 23){          //巨蟹座
                a[0]=0;             //6/22~7/22
                a[1]=3;
                a[2]=2;
                a[3]=2;
                a[4]=1;
                zodiacSign = "Cancer";
            }
            else if(date >= 23){    //獅子座
                a[0]=5;             //7/23~8/22
                a[1]=3;
                a[2]=2;
                a[3]=3;
                a[4]=1;
                zodiacSign = "Leo";
            }    
                break;  
            case "August": 
            if(date < 23){          //獅子座
                a[0]=5;             //7/23~8/22
                a[1]=3;
                a[2]=2;
                a[3]=3;
                a[4]=1;
                zodiacSign = "Leo";
            }
            else if(date >= 23){    //處女座
                a[0]=1;             //8/23~9/22
                a[1]=1;
                a[2]=3;
                a[3]=5;
                a[4]=3;
                zodiacSign = "Virgo";
            }    
                break; 
            case "September": 
            if(date < 23){          //處女座
                a[0]=1;             //8/23~9/22
                a[1]=1;
                a[2]=3;
                a[3]=5;
                a[4]=3;
                zodiacSign = "Virgo";
            }
            else if(date >= 23){    //天秤座
                a[0]=1;             //9/23~10/23
                a[1]=4;
                a[2]=1;
                a[3]=7;
                a[4]=2;
                zodiacSign = "Libra";
            }    
                break;
            case "October": 
            if(date < 24){          //天秤座
                a[0]=1;             //9/23~10/23
                a[1]=4;
                a[2]=1;
                a[3]=7;
                a[4]=2;
                zodiacSign = "Libra";
            }
            else if(date >= 24){    //天蠍座
                a[0]=3;             //10/24~11/21
                a[1]=0;
                a[2]=4;
                a[3]=2;
                a[4]=0;
                zodiacSign = "Scorpio";
            }    
                break; 
            case "November": 
            if(date < 22){          //天蠍座
                a[0]=3;             //10/24~11/21
                a[1]=0;
                a[2]=4;
                a[3]=2;
                a[4]=0;
                zodiacSign = "Scorpio";
            }
            else if(date >= 22){    //射手座
                a[0]=3;             //11/22~12/20
                a[1]=3;
                a[2]=2;
                a[3]=3;
                a[4]=3;
                zodiacSign = "Sagittarius";
            }    
                break; 
            case "December": 
            if(date < 21){          //射手座
                a[0]=3;             //11/22~12/20
                a[1]=3;
                a[2]=2;
                a[3]=3;
                a[4]=3;
                zodiacSign = "Sagittarius";
            }
            else if(date >= 21){    //魔羯座
                a[0]=0;             //12/21~1/20
                a[1]=2;
                a[2]=4;
                a[3]=2;
                a[4]=1;
                zodiacSign = "Capricorn";
            }    
                break;
            default: 
                a[0]=0;             
                a[1]=0;
                a[2]=0;
                a[3]=0;
                a[4]=0;
                print("constellation No score"); 
                break;      
        }
        
    }

    public void job()
    {
        string job = ParaData.ParaStorage[20];
        switch (job) { 
            case "a cook": 
                b[0]=3;
                b[1]=1;
                b[2]=2;
                b[3]=1;
                b[4]=4;
                break;       
            case "a farmer": 
                b[0]=0;
                b[1]=4;
                b[2]=2;
                b[3]=6;
                b[4]=0;
                break; 
            case "a doctor": 
                b[0]=3;
                b[1]=9;
                b[2]=5;
                b[3]=1;
                b[4]=2;
                break; 
            case "a police": 
                b[0]=8;
                b[1]=4;
                b[2]=6;
                b[3]=3;
                b[4]=6;
                break;
            case "a nurse": 
                b[0]=4;
                b[1]=7;
                b[2]=4;
                b[3]=3;
                b[4]=2; 
                break; 
            case "a firefighter": 
                b[0]=2;
                b[1]=4;
                b[2]=6;
                b[3]=4;
                b[4]=7; 
                break;
            case "a teacher": 
                b[0]=4;
                b[1]=7;
                b[2]=2;
                b[3]=1;
                b[4]=4; 
                break;
            case "a Pilot": 
                b[0]=3;
                b[1]=2;
                b[2]=5;
                b[3]=2;
                b[4]=5; 
                break;
            case "a housewife": 
                b[0]=0;
                b[1]=3;
                b[2]=4;
                b[3]=4;
                b[4]=1; 
                break;
            case "a scientist": 
                b[0]=0;
                b[1]=1;
                b[2]=2;
                b[3]=3;
                b[4]=9; 
                break;
            case "a office worker": 
                b[0]=3;
                b[1]=6;
                b[2]=0;
                b[3]=1;
                b[4]=0; 
                break;
            case "a player": 
                b[0]=2;
                b[1]=3;
                b[2]=1;
                b[3]=2;
                b[4]=2; 
                break;
            case "a reporter": 
                b[0]=2;
                b[1]=1;
                b[2]=2;
                b[3]=1;
                b[4]=1; 
                break;
            case "a salesman": 
                b[0]=2;
                b[1]=3;
                b[2]=1;
                b[3]=2;
                b[4]=3; 
                break;
            case "a secretary": 
                b[0]=3;
                b[1]=1;
                b[2]=3;
                b[3]=2;
                b[4]=3; 
                break;
            case "a shopkeeper": 
                b[0]=2;
                b[1]=2;
                b[2]=1;
                b[3]=1;
                b[4]=3; 
                break;   
            case "a singer": 
                b[0]=3;
                b[1]=2;
                b[2]=3;
                b[3]=2;
                b[4]=2; 
                break;
            case "a soldier": 
                b[0]=1;
                b[1]=2;
                b[2]=4;
                b[3]=2;
                b[4]=2; 
                break;
            case "a star": 
                b[0]=3;
                b[1]=2;
                b[2]=3;
                b[3]=3;
                b[4]=2; 
                break;
            case "a waiter": 
                b[0]=3;
                b[1]=2;
                b[2]=1;
                b[3]=1;
                b[4]=0; 
                break;
            case "a waitress": 
                b[0]=3;
                b[1]=2;
                b[2]=1;
                b[3]=1;
                b[4]=0; 
                break;
            case "a worker": 
                b[0]=3;
                b[1]=2;
                b[2]=2;
                b[3]=2;
                b[4]=3; 
                break;
            case "a writer": 
                b[0]=1;
                b[1]=3;
                b[2]=2;
                b[3]=1;
                b[4]=5; 
                break;
            case "an actor": 
                b[0]=4;
                b[1]=2;
                b[2]=3;
                b[3]=2;
                b[4]=1; 
                break;
            case "an actress": 
                b[0]=4;
                b[1]=2;
                b[2]=3;
                b[3]=3;
                b[4]=1;
                break;
            case "a businessman": 
                b[0]=3;
                b[1]=1;
                b[2]=3;
                b[3]=2;
                b[4]=4; 
                break;
            case "a clerk": 
                b[0]=3;
                b[1]=2;
                b[2]=1;
                b[3]=1;
                b[4]=0; 
                break;
            case "a dentist": 
                b[0]=2;
                b[1]=2;
                b[2]=3;
                b[3]=2;
                b[4]=2; 
                break;   
            case "an engineer": 
                b[0]=1;
                b[1]=3;
                b[2]=5;
                b[3]=2;
                b[4]=5; 
                break;
            case "a fisherman": 
                b[0]=1;
                b[1]=3;
                b[2]=2;
                b[3]=2;
                b[4]=1; 
                break;
            case "a dancer": 
                b[0]=2;
                b[1]=4;
                b[2]=4;
                b[3]=2;
                b[4]=2; 
                break;
            case "a lawyer": 
                b[0]=2;
                b[1]=2;
                b[2]=6;
                b[3]=2;
                b[4]=5; 
                break;     
            default: 
                b[0]=0;
                b[1]=0;
                b[2]=0;
                b[3]=0;
                b[4]=0;
                print("job No score"); 
                break; 
        }
    }
    
    public void interest()
    {
        string interest = ParaData.ParaStorage[11];
        switch (interest) { 
            case "cook": case "cooking": 
                c[0]=2;
                c[1]=6;
                c[2]=4;
                c[3]=1;
                c[4]=3;
                break;       
            case "eat": case "eating":
                c[0]=5;
                c[1]=5;
                c[2]=2;
                c[3]=0;
                c[4]=1;
                break; 
            case "sing": case "singing": 
                c[0]=1;
                c[1]=0;
                c[2]=0;
                c[3]=0;
                c[4]=3;
                break;       
            case "study": case "studying": case "learning":
                c[0]=1;
                c[1]=0;
                c[2]=3;
                c[3]=2;
                c[4]=2;
                break; 
            case "shop": case "shopping": 
                c[0]=1;
                c[1]=2;
                c[2]=2;
                c[3]=1;
                c[4]=1;
                break;       
            case "dance":
                c[0]=1;
                c[1]=0;
                c[2]=4;
                c[3]=3;
                c[4]=2;
                break; 
            case "read":
                c[0]=0;
                c[1]=4;
                c[2]=0;
                c[3]=4;
                c[4]=1;
                break;
            case "draw":
                c[0]=3;
                c[1]=5;
                c[2]=3;
                c[3]=4;
                c[4]=6;
                break; 
            case "Swim":
                c[0]=2;
                c[1]=4;
                c[2]=3;
                c[3]=1;
                c[4]=0;
                break; 
            case "ride a bike":
                c[0]=3;
                c[1]=2;
                c[2]=2;
                c[3]=2;
                c[4]=1;
                break; 
            case "play the piano":
                c[0]=2;
                c[1]=2;
                c[2]=4;
                c[3]=1;
                c[4]=2;
                break;
            case "bake":
                c[0]=2;
                c[1]=2;
                c[2]=2;
                c[3]=2;
                c[4]=0;
                break; 
            case "playing chess":
                c[0]=2;
                c[1]=3;
                c[2]=3;
                c[3]=2;
                c[4]=3;
                break; 
            case "playing the guitar":
                c[0]=2;
                c[1]=3;
                c[2]=1;
                c[3]=2;
                c[4]=2;
                break; 
            case "flying kite":
                c[0]=4;
                c[1]=3;
                c[2]=1;
                c[3]=2;
                c[4]=2;
                break;
            default: 
                c[0]=0;
                c[1]=0;
                c[2]=0;
                c[3]=0;
                c[4]=0;
                print("interest No score"); 
                break;
        }
    }

    public void score(){
        Extraversion.text ="<color=white><size=18>"+d[0]+"</size></color>"+"\n"+"Extraversion"+"\n"+"(外向型)";
        Agreeableness.text ="<color=white><size=18>"+d[1]+"</size></color>"+"\n"+"Agreeableness"+"\n"+"(親和型)";
        Conscientiousness.text ="<color=white><size=18>"+d[2]+"</size></color>"+"\n"+"Conscientiousness"+"\n"+"(盡責型)";
        EmotionalStability.text ="<color=white><size=18>"+d[3]+"</size></color>"+"\n"+"Emotional Stability"+"\n"+"(沉穩型)";
        OpennesstoExperience.text ="<color=white><size=18>"+d[4]+"</size></color>"+"\n"+"Openness to Experience"+"\n"+"(開放型)";
        Listen.text ="<color=white><size=18>"+ability[0]+"</size></color>"+"\n"+"Listen";
        Speak.text ="<color=white><size=18>"+ability[1]+"</size></color>"+"\n"+"Speak";
        Read.text ="<color=white><size=18>"+ability[2]+"</size></color>"+"\n"+"Read";
        Write.text ="<color=white><size=18>"+ability[3]+"</size></color>"+"\n"+"Write";
    }

    public void add(){
        constellation();
        job();
        interest();

        for(int i=0;i<5;i++){
           d[i]= a[i]+b[i]+c[i];
        }
    }

    public void dynamic(){   //動態
        int max=0;
        int i=0;
        int j=1;
        range=10;
        weighted=0;

        if(length==5){
        for(i=0;i<5;i++){
           if(max<d[i]){
                max=d[i];
            }         
        }}
        
        if(length==4){
        for(i=0;i<4;i++){
           if(max<ability[i]){
                max=ability[i];
            }         
        }}

        while(max>range){
            j++;
            range=10*j;
            }
        weighted=100/range;
    }


    [SerializeField]
    private Image[] m_panels;
    
    public void OnValidate() {       
        length=d.Length;

        dynamic ();
        
        for (int i = 0; i < 5; i++) {
            SetValue (i, d[i]);       
        }
    }

    public void SetValue (int index, int value) {
        d[index] = value ;
        m_fullSize=weighted;
        
        Vector2 size = m_panels[index].rectTransform.sizeDelta;
        size.x = m_fullSize * value;
        m_panels[index].rectTransform.sizeDelta = size;

        int pre = (index + m_panels.Length - 1) % m_panels.Length;
        size = m_panels[pre].rectTransform.sizeDelta;
        size.y = m_fullSize * value;
        m_panels[pre].rectTransform.sizeDelta = size;
    }


    [SerializeField]
    private Image[] n_panels;
    
    public void OnValidate2() {
        ability[1]=PlayerData.score/10;
        length=ability.Length;

        dynamic ();
        
        for (int i = 0; i < 4; i++) {
            SetValue2 (i, ability[i]);
        }
    }

    public void SetValue2 (int index, int val) {
        ability[index] = val ;
        n_fullSize=weighted;

        Vector2 size = n_panels[index].rectTransform.sizeDelta;
        size.x = n_fullSize * val  ;
        n_panels[index].rectTransform.sizeDelta = size;

        int pre2 = (index + n_panels.Length - 1) % n_panels.Length;
        size = n_panels[pre2].rectTransform.sizeDelta;
        size.y = n_fullSize * val ;
        n_panels[pre2].rectTransform.sizeDelta = size;
        }

    public void click(){
        GameObject.Find("Canvas").GetComponent<radarToSQL>().Upload();    

        GameObject.Find("Canvas").SendMessage("LoadLevel", "GameLobby"); 
    }

    void Setpersonalityscore()
    {
        /*for(int i=0;i<5;i++){
            personalityscore.radarscore[i]=d[i];
        }
        for(int i=0;i<4;i++){
            personalityscore.radarscore[i+5]=ability[i];
        }*/
        personalityscore.Extraversion=d[0];
        personalityscore.Agreeableness=d[1];
        personalityscore.Conscientiousness=d[2];
        personalityscore.EmotionalStability=d[3];
        personalityscore.OpennesstoExperience=d[4];
        personalityscore.Listen=ability[0];
        personalityscore.Speak=ability[1];
        personalityscore.Read=ability[2];
        personalityscore.Write=ability[3];
    }

    void SetPlayerData()
    {
        PlayerData.zodiacSign = zodiacSign;
    }
}


