using System.Collections;
using System.Collections.Generic;

public interface IHaveHealth
{ 
    float Health { get; set; }

    void TakeDamage(float damageAmount);

    void HealthBelowZero();
}
