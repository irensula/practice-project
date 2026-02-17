using UnityEngine;

[CreateAssetMenu(fileName = "UITheme", 
menuName = "UI/Theme")]
public class UITheme : ScriptableObject
{
    public Color primary;
    public Color text;
    public Color secondary;
    public Color accent;
    public Color royalPlum;
    public Color lavanderIndigo;


    public Color success;
    public Color error;
    public Color warning;
}

public interface IThemeable
{
    void ApplyTheme(UITheme theme);
}


//     dark:
//     --primary: #1F0235 rgb(31, 2, 53);
//     --text: #DEE2FF rgb(222, 226, 255);
//     light:
//     --primary: #DEE2FF rgb(222, 226, 255);
//     --text: #29282E rgb(41, 40, 46);

//     --secondary: #7F3792 rgb(127, 55, 146);
//     --accent: #FF8C00 rgb(255, 140, 0);
//     --royal-plum: #8D32D4 rgb(141, 50, 212);
//     --lavander-indigo: #836AEA rgb(131, 106, 234);

//     --success: #1cd62c rgb(28, 214, 44);
//     --error: #DC143C rgb(220, 20, 60);
//     --warning: #FFA500 rgb(255, 165, 0);