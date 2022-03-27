using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject FireShotPrefab;

    [SerializeField] SpriteRenderer FireShotGFX;

    [SerializeField] Slider FlameInHandPowerSlider;

    [SerializeField] Transform FlameInHand;

    [Range(0, 10)]

    [SerializeField] float FlameInHandPower;

    [Range(0, 3)]

    [SerializeField] float MaxFlameInHandCharge;

    float FlameInHandCharge;

    bool CanFire = true;

    private void Start()
    {
        FlameInHandPowerSlider.value = 0f;
        FlameInHandPowerSlider.maxValue = MaxFlameInHandCharge;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && CanFire)
        {
            ChargeFlameInHand();
        } else if (Input.GetMouseButtonUp(0) && CanFire)
        {
            FireFireInHand();
        } else
        {
            if (FlameInHandCharge > 0f)
            {
                FlameInHandCharge -= 1f * Time.deltaTime;
            } else
            {
                FlameInHandCharge = 0f;
                CanFire = true;
            }

            FlameInHandPowerSlider.value = FlameInHandCharge;
        }
    }

    void ChargeFlameInHand()
    {
        FireShotGFX.enabled = true;

        FlameInHandCharge += Time.deltaTime;

        FlameInHandPowerSlider.value = FlameInHandCharge;

        if (FlameInHandCharge > MaxFlameInHandCharge)
        {
            FlameInHandPowerSlider.value = MaxFlameInHandCharge;
        }
    }

    void FireFireInHand()
    {
        if (FlameInHandCharge > MaxFlameInHandCharge) FlameInHandCharge = MaxFlameInHandCharge;

        float FlameInHandSpeed = FlameInHandCharge + FlameInHandPower;
        float FireDamage = FlameInHandCharge * FlameInHandPower;

        float angle = Utility.AngleTowardsMouse(FlameInHand.position);
        Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));

        FireShot FireShot = Instantiate(FireShotPrefab, FlameInHand.position, rot).GetComponent<FireShot>();
        FireShot.FireVelocity = FlameInHandSpeed;
        FireShot.FireDamage = FireDamage;

        CanFire = false;
        FireShotGFX.enabled = false;
    }
}