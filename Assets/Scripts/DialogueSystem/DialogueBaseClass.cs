using UnityEngine;
using System.Collections;
using UnityEngine.UI;



// allows you to maintain structure and minimize naming conflicts 
namespace DialogueSystem
{

    public class DialogueBaseClass : MonoBehaviour
    {
        // IEnumerator is used to fetch the current element from a collection.
        // input will take some text " " in the textHolder and use a loop to take each letter
        // from the input String and put it in textholder, wait current amt of time and move on to
        // the next letter. Process is continued until we've iterated through all the letters from the input str

        // protected to make it accessible from child class (dialogue line)
        // using Text from the UI components 
        protected IEnumerator WriteText(string input, Text textHolder)
        {

            for(int i=0; i<input.Length; i++)
            {
                textHolder.text += input[i];
                // wait for seconds build in function 
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
        

}
