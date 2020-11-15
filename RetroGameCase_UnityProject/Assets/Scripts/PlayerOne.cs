using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class PlayerOne : Player
    {
        [SerializeField] private string h;
        [SerializeField] private string v;
        [SerializeField] private string dash;
        
        public PlayerOne(float maxSanity, float maxStamina, Transform spawnPos) : base( maxSanity,  maxStamina,  spawnPos){
            
        }

        private void Start(){
            
        }

        private void Update(){
            Move(h, v , this.gameObject);
            Dash( dash );
        }

        public override void Move( string hInput , string vInput , GameObject p ){
            base.Move( hInput , vInput , p );
        }

        public override void Dash( string dashInput ){
            base.Dash( dashInput );
        }
    }
}