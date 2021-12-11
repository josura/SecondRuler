using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable<T> {

    bool Damage(T damage);
}
