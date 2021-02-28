using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ContesterTypeSO : ScriptableObject
{
    //Bu bir Scriptable Object
    //Scriptable object: Bir data setidir. Bu sayede yapıları daha rahat kurabiliriz.
    //Mesala her Player için ayrı bir renge ihtiyacımız vardı
    //ve bu yapı sayesinde bir Script aracılığı ile birçok player tarzına ulaşabiliriz.

    [SerializeField]
    string contesterName;
    [SerializeField]
    Color changingColor;

    public Color ChangingColor => changingColor; // sadece get
    public string ContesterName => contesterName;
}