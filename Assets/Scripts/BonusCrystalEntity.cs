using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCrystalEntity : NotPlayableEntity
{
        
        public override void FillObject(GameObject ob)
        {
                var components = ob.GetComponent<GameObjectInfo>().Components;
                if (!components.Contains("CrystalBonus"))
                { 
                        components.Add("CrystalBonus");
                        ob.AddComponent<CrystalBonus>();
                }
                if (!components.Contains("AudioSource"))
                {
                    components.Add("AudioSource");
                    var audio = ob.AddComponent<AudioSource>();
                    audio.volume = base.audio.volume;
                    audio.clip = base.audio.clip;
                }

                if (!components.Contains("PeltingBotton"))
                {
                        components.Add("PeltingBotton");
                        ob.AddComponent<PeltingBotton>();
                }
                if (!components.Contains("RotatableButton"))
                {
                        components.Add("RotatableButton");
                        ob.AddComponent<RotatableButton>();
                }
                if (!components.Contains("BoxCollider2D"))
                {
                        components.Add("BoxCollider2D");
                        var box = ob.AddComponent<BoxCollider2D>();
                        box.isTrigger = true; 
                        box.size = Vector2.one*0.7f;
                }
                if (!components.Contains("SpriteRenderer"))
                {
                        components.Add("SpriteRenderer");
                        ob.GetComponent<SpriteRenderer>().sprite = sprite;
                }

                
               
                //ob.AddComponent<ParticleSystem>().playOnAwake=false;
                
                
        }
}
