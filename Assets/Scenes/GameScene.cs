using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GameScene : MonoBehaviour
{
    public GameObject CellPrefabs;
    public GameObject Parent;
    int TotalCells=8;
    public Sprite[] ObjectSprite;
    public List<Sprite> PickedSprite = new List<Sprite>();
    bool FirstClick=false;
    bool SecondClick=false;
    public string FirstMemorySpriteName,SecondMemorySpriteName;
    public Sprite CellBg;
    int FirstCellPosVal,SecondCellPosVal;
    int WinCellCount=0;
    public List<int> RandomCellValue=new List<int>();
    public AudioSource sound;
    public AudioSource soundapple;
    public AudioSource soundhouse;
    public AudioSource soundburger;
    public AudioSource soundball;
   


   

   
   
    // Start is called before the first frame update
    void Start()
    {
        for (int i=0;i<TotalCells;i++){
            GameObject CellInstance=Instantiate(CellPrefabs);
            CellInstance.transform.SetParent(Parent.transform);
           CellInstance.gameObject.name=i.ToString();
           ButtonListener(CellInstance);
           RandomCellValue.Add(i);
       
        }
        CollectingSpriteToCells();
    }
   //sound

    //to add sprite assigning object
    public void CollectingSpriteToCells(){
        int index=0;
        for(int i=0;i<TotalCells;i++){
            if(i== TotalCells / 2){
                index=0;
            }
            PickedSprite.Add(ObjectSprite[index]);
            index++;
           
        }
        RandomNumberGenerator();
    }
     //random number generator 
     public void RandomNumberGenerator(){
        RandomCellValue=RandomCellValue.OrderBy(Outval=>System.Guid.NewGuid()).ToList();
     }
     //sound
     public void playsound(){
        sound.Play();
     }
     //soundmatch
   
    //button listener
     void ButtonListener(GameObject CellInstance){
           CellInstance.GetComponent<Button>().onClick.AddListener(DetectClick);
           CellInstance.GetComponent<Button>().onClick.AddListener(playsound);

           
    }
    public void DetectClick(){
    //    Parent.transform.GetChild(ClickedValue).GetComponent<Image>().sprite=PickedSprite[ClickedValue];
       if(!FirstClick){
       FirstCellPosVal=int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
        FirstClick=true;
        Parent.transform.GetChild(FirstCellPosVal).GetComponent<Image>().sprite=PickedSprite[RandomCellValue[FirstCellPosVal]];
        Debug.Log("the click has been done: "+ UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
        FirstMemorySpriteName= Parent.transform.GetChild(FirstCellPosVal).GetComponent<Image>().sprite.name;
        Parent.transform.GetChild(FirstCellPosVal).GetComponent<Button>().enabled=false;
       }
       else if(!SecondClick){
       SecondCellPosVal=int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
        SecondClick=true;
        Parent.transform.GetChild(SecondCellPosVal).GetComponent<Image>().sprite=PickedSprite[RandomCellValue[SecondCellPosVal]];
        Debug.Log("the click has been done: "+ UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
        SecondMemorySpriteName= Parent.transform.GetChild(SecondCellPosVal).GetComponent<Image>().sprite.name;
        Parent.transform.GetChild(SecondCellPosVal).GetComponent<Button>().enabled=false;
        Invoke("Detect", 0.40f );


       }
    }
  void matching(){
     if(FirstMemorySpriteName == "character4"){
        soundapple.Play();
     }
     else if (FirstMemorySpriteName == "character2"){
        soundhouse.Play();
     }
     else if(FirstMemorySpriteName == "character3"){
          soundburger.Play();
     }
     else{
        soundball.Play();
     }
  }
   void Detect(){
   
    if(FirstMemorySpriteName == SecondMemorySpriteName){
        
         FirstClick=false;
         SecondClick=false;
         Debug.Log("matched");     
         WinCellCount++;
         matching();
         if(WinCellCount == TotalCells/ 2){
            Debug.Log("You won the game!");
         }
    }
    else{
         FirstClick=false;
         SecondClick=false;
         Debug.Log("not matched");
        Parent.transform.GetChild(FirstCellPosVal).GetComponent<Image>().sprite=CellBg;
        Parent.transform.GetChild(SecondCellPosVal).GetComponent<Image>().sprite=CellBg;
        Parent.transform.GetChild(FirstCellPosVal).GetComponent<Button>().enabled=true;
        Parent.transform.GetChild(SecondCellPosVal).GetComponent<Button>().enabled=true;




         
    }
   }
    // Update is called once per frame
    void Update()
    {
        
    }
}
