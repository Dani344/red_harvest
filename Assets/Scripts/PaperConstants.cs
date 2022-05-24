using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperConstants
{
    //===== SCENES =====
    public const string SCENE_MAIN_MENU = "MainMenu";
    public const string SCENE_PLAYING = "PlayingScene";
    public const string SCENE_RESUME = "GameResume";

    //===== TAGS =====
    public const string TAG_GROUND = "Ground";
    public const string TAG_GAME_MANAGER = "GameManager";
    public const string TAG_ENEMY = "Enemy";
    public const string TAG_RECOLECTABLE = "Recolectable";
    public const string TAG_DEBUFF = "Debuff";
    public const string TAG_POWER_UP = "PowerUp";
    public const string TAG_MISSIL = "Missil";
    public const string TAG_ABILITIES_ICONS = "AbilitiesIcons";
    public const string TAG_PROJECTIL = "Projectil";
    public const string TAG_WALLS = "Walls";
    public const string TAG_SPAWN_PLAYER = "SpawnPlayer";
    public const string TAG_SPAWN_ENEMY = "SpawnEnemy";
    public const string TAG_SHIELD = "Shield";
    public const string TAG_MAIN_HPBAR = "MainHpBar";
    public const string TAG_CAST_BAR = "CastBar";
    
    public const string TAG_PLAYER = "Player";
    //public const string TAG_MAIN_CAMERA = "MainCamera";
    
    //===== LAYERS =====
    public const string LAYER_GROUND = "Ground";
    public const string LAYER_PLAYER = "Player";
    public const string LAYER_ENEMY = "Enemey";
    
    
    //===== PLAYER =====
    public const string ANIM_PLAYER_F_MOVING = "isMoving";
    public const string ANIM_TRIG_PLAYER_DEATH = "Die";
    
    
    //Animation
    public const string ANIM_Q = "asdhfjsdf";
    
    //===== CAMERA =====
    public const float HIGH_SCREEN_PERCENTAGE = 0.1f;
    public const float VERTICAL_SCREEN_PERCENTAGE = 0.1f;

    public const float ZOOM_CAMERA_SENSIVITY = 10f;
    public const float ZOOM_CAMERA_MIN_DISTANCE = 5f;
    public const float ZOOM_CAMERA_MAX_DISTANCE = 20f;



    //FALTA LAS CONSTANTES DE LAS BARRAS DE SALUD
    //FALTA LAS CONSTANTES DE ANIMATIONS
    
    //FALTA REVISAR SCRITPS: CHARACTER/ ENEMYFRONTAL / ENEMY
}
