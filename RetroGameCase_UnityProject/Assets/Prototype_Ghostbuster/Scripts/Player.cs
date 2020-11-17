using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Prototype_Ghostbuster.Scripts
{
    public class Player : MonoBehaviour
    {
        [Header("input strings")]
        [SerializeField] private string h;
        [SerializeField] private string v;
        [SerializeField] private string dash;
    
        private                    float      maxStamina;
        private                    float      currentStamina;
        private                    Transform  spawnPos;
        private                    GameObject isHolding;
        [ SerializeField ] private float      timePassed = 0f;
        private                    bool       isDashing;
        [ SerializeField ] private float      sanityTimer       = 1f;
        [ SerializeField ] private float      sanityTimerPassed = 0f;
    
        [ SerializeField ] private int   amountCaught;
        [ SerializeField ] private float currentSanity = 100;
        [ SerializeField ] private float speed;
        [ SerializeField ] private float dashSpeed;
        [ SerializeField ] private Text  sanity;
        [ SerializeField ] private float maxSanity;
    
        [ SerializeField ] private GameObject colLeft;
        [ SerializeField ] private GameObject colRight;
        [ SerializeField ] private GameObject colTop;
        [ SerializeField ] private GameObject colBottom;
    
        private IEnumerator coroutineAddSanity;
        private IEnumerator coroutineDepleteSanity;

        public Player( float maxSanity , float maxStamina , Transform spawnPos ){
            this.maxSanity = maxSanity;
            this.maxStamina = maxStamina;
            this.spawnPos = spawnPos;
        }
    
        public void Start(){
            currentSanity = 100;
            sanity.text = currentSanity.ToString();
            coroutineAddSanity = AddSanity();
            coroutineDepleteSanity = DepleteSanity();
            StartCoroutine( coroutineDepleteSanity );
        }

        public void Update(){
            sanity.text = currentSanity.ToString();
        }
    
        public void FixedUpdate(){
            Move(h, v , this.gameObject);
            Dash( dash );
        }

        public float CurrentSanity{
            get => currentSanity;
            set => currentSanity = value;
        }
        public int AmountCaught{
            get => amountCaught;
            set => amountCaught = value;
        }

        private void Move(string hInput, string vInput, GameObject p){
            if( isDashing )
                return;
         
            float h = Input.GetAxis( hInput );
            float v = Input.GetAxis( vInput );
            //Debug.Log(string.Format("{0};{1}", h,v));
            Vector2 inputVector = new Vector2(h,v);
            GetComponent<Rigidbody2D>().velocity = new Vector2( h,v ) * (speed * Time.fixedDeltaTime);
        
            switch( inputVector ){
                case Vector2 vec when vec.x > 0 :
                    colLeft.SetActive(false);
                    colRight.SetActive(true);
                    colTop.SetActive(false);
                    colBottom.SetActive(false);
                    break;
                case Vector2 vec when vec.x < 0:
                    colLeft.SetActive(true);
                    colRight.SetActive(false);
                    colTop.SetActive(false);
                    colBottom.SetActive(false);
                    break;
                case Vector2 vec when vec.y > 0:
                    colLeft.SetActive(false);
                    colRight.SetActive(false);
                    colTop.SetActive(true);
                    colBottom.SetActive(false);
                    break;
                case Vector2 vec when vec.y < 0:
                    colLeft.SetActive(false);
                    colRight.SetActive(false);
                    colTop.SetActive(false);
                    colBottom.SetActive(true);
                    break;
                default:
                    colLeft.SetActive(false);
                    colRight.SetActive(false);
                    colTop.SetActive(false);
                    colBottom.SetActive(false);
                    break;
            }
        }

        private void Dash( string dashInput ){
            if(!Input.GetButtonDown(dashInput))
                return;
            //Debug.Log("DASH");
            StartCoroutine( DashCoroutine( .2f ) );
        }

        private IEnumerator DashCoroutine( float duration){
            isDashing = true;
            Vector2 localDir = GetComponent< Rigidbody2D >().velocity;
            GetComponent<Rigidbody2D>().AddForce(localDir.normalized * dashSpeed, ForceMode2D.Impulse);
            yield return new WaitForSeconds(duration);
            isDashing = false;
        }

        private IEnumerator DepleteSanity(){
            while( true ){
                currentSanity -= 1;
                //Debug.Log("Deplete Sanity");
                if( currentSanity > maxSanity )
                    currentSanity = maxSanity;
                yield return new WaitForSeconds(1f);
            }
        }

        private IEnumerator AddSanity(){
            while( true ){
                currentSanity += 2;
                //Debug.Log("Add Sanity");
                if( currentSanity > maxSanity )
                    currentSanity = maxSanity;
                yield return new WaitForSeconds(1f);
            }
        }
    
        public void OnTriggerEnter2D( Collider2D other ){
            //if( !other.CompareTag( "Light" ) ) ; 
            //return;
            if( other.CompareTag( "Light" ) ){ 
            
                StopCoroutine( coroutineDepleteSanity);
                coroutineDepleteSanity = null;
            
                if( coroutineAddSanity == null)
                    coroutineAddSanity = AddSanity();
            
                StartCoroutine( coroutineAddSanity );
            }

            //if( other.CompareTag( "Ghost" ) ){
            //    CurrentSanity -= 10;
            //}
        }
        public void OnTriggerExit2D( Collider2D other ){
            //if ( !other.CompareTag( "Light" ) ) 
            //return;
            if( other.CompareTag( "Light" ) ){
            
                StopCoroutine( coroutineAddSanity );
                coroutineAddSanity = null;
            
                if( coroutineDepleteSanity == null )
                    coroutineDepleteSanity = DepleteSanity();
            
                StartCoroutine( coroutineDepleteSanity );
            }
        }
    
    

    
    }
}
