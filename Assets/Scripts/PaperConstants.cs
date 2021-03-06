using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PaperConstants
{
    //===== DEBUG LOG =====
    public const string SCREEN_MSG = "Error de variable";
    
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
    public const string LAYER_ENEMY = "Enemy";
    
    
    //===== PLAYER =====
    public const string ANIM_PLAYER_F_MOVING = "isMoving";
    public const string ANIM_TRIG_PLAYER_DEATH = "Die";
    
    //===== PLAYER PREFS =====
    public const string PLAYER_PREFS_TOTAL_COINS = "Coins";
    public const string PLAYER_PREFS_ENEMIES_KILLED = "EnemiesKilled";
    public const string PLAYER_PREFS_RESUME_GAME = "Resume";
    
    //Animation
    public const string ANIM_Q = "asdhfjsdf";
    public const string ANIM_W = "jlasdfjasdf";
    
    //COOLDOWNS PLAYER
    public const float COOLDOWN_Q = 2f;
    public const float COOLDOWN_W = 5f;
    public const float COOLDOWN_E = 7f;
    public const float COOLDOWN_R = 10f;

    public const float COOLDOWN_D = 20f;
    public const float COOLDOWN_F = 300f;
    
    //Projectils
    public const int PHYSIC_DAMAGE = 0;
    public const int MAGIC_DAMAGE = 1;
    public const int TRUE_DAMAGE = 2;

    public const float LIFE_TIME_PROJECTILS = 5f;
    public const float TARGET_UPDATE = 1f;


    //Colors NPGs HPsBars
    public const int HP_BAR_NEUTRAL = 0;
    public const int HP_BAR_COMBAT = 1;
    public const int HP_BAR_FRIENDLY = 2;
    
    
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
