﻿
class ExplosionDestroyer : Destroyer
{
    public float lifeTime = 1f;

    void Awake()
    {
        Destroy(gameObject, lifeTime);
    }

}

