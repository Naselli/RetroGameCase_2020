using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class PlayerOne : Player
    {
        [SerializeField]   private string h;
        [SerializeField]   private string v;
        [SerializeField]   private string dash;
      
        
        public PlayerOne(float maxSanity, float maxStamina, Transform spawnPos) : base( maxSanity,  maxStamina,  spawnPos){
            
        }
        

        private void FixedUpdate(){
            Move(h, v , this.gameObject);
            Dash( dash );
        }
        public override void Start()                                              => base.Start();
        public override void Move( string hInput , string vInput , GameObject p ) => base.Move( hInput , vInput , p );
        public override void Dash( string dashInput )                             => base.Dash( dashInput );
        public override void OnTriggerEnter2D( Collider2D other )                 => base.OnTriggerEnter2D( other );
        public override void OnTriggerExit2D( Collider2D other )                  => base.OnTriggerExit2D( other );
    }
}