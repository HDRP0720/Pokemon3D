# Pokemon 3D

This is a 3D pokemon game clone with unity URP template (2021.3.24f1)

## Formula of Throw under physics 
* Parabolic throwing need some mathmatics
```math
\displaylines{
  velocityY = \sqrt{(-2gh)} \\ 
  velocityX = {d_x \over \sqrt{(-2h/g)} + \sqrt{(2(d_y - h)/g)}}
}
```

## Used Asset
* Unity Asset: Supercyan's [Environment Pack: Free Forest Sample](https://assetstore.unity.com/packages/3d/vegetation/environment-pack-free-forest-sample-168396)
* Sprite: fluxord's circle-03 in [20 Crosshairs for RE](https://opengameart.org/content/20-crosshairs-for-re)
* The asset location should be in `/Assets/Art/Asset`

## Mixamo Character & Animations
* Character - AJ
* Animations
  * Idle
  * Walking
  * Running
  * Jump Over
  * Throw Object

## Reference
* [Kinematic Equations](https://youtu.be/v1V3T5BPd7E)