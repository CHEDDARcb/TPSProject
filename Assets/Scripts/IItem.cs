using UnityEngine;

public interface IItem
{
    //入力としてアイテムを適用するゲームオブジェクトを受け入れる
    void Use(GameObject target);
}