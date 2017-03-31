using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;
using Urho.Gui;
using Urho.Resources;

namespace CC_X
{
    class Program : IObserver
    {
        UIElement uiRoot;
        ResourceCache cache;

        Window menu;
        Window hallOfFame;
        Window help;
        Window about;
        Window setLoc;
        Window setName;
        Window chooseChar;
        Window selectDiff;
        Window giveCharName;

        Button mainMenu;
        Button helpBtn;
        Button aboutBtn;
        Button hallOfFameBtn;
        Button developerBtn;
        Button backBtn;
        Button exitBtn;
        Button startBtn;
        Button charOptn1;
        Button charOptn2;
        Button charOptn3;
        Button easy;
        Button medium;
        Button hard;
        Button submitCharName;

        Text menuBtnText;
        Text helpBtnText;
        Text aboutBtnText;
        Text hallOfFameBtnText;
        Text developerBtnText;
        Text backBtnText;
        Text exitBtnText;
        Text welcomeMsg;
        Text aboutMsg;
        Text startBtnMsg;
        Text charOptn1Text;
        Text charOptn2Text;
        Text charOptn3Text;
        Text easyText;
        Text mediumText;
        Text hardText;
        Text submitCharNameText;

        LineEdit xSet;
        LineEdit ySet;
        LineEdit zSet;
        LineEdit charName;

        static void Main(string[] args)
        {
        }
    }
}
