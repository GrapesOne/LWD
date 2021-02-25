
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class NotPlayableEntity : Entity
{  
    public abstract void FillObject(GameObject ob);
    public Entities EntityType;
    public SettingsEnum.Difficulties difficult;
    public SettingsEnum.Environment environment;
    protected SpriteRenderer Renderer;
    protected ParticleSystem Particle;
    public VolumedAudioClip audio;
   
    
    public enum Entities
    {
        Ground = 1,
        Sphere,
        Crystal,
        Clock,
        Enemy
    }
}
